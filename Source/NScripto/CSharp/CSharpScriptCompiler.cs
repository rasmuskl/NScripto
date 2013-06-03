using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.CSharp;
using NScripto.CodeDom;
using NScripto.Extensions;
using NScripto.Raw;

namespace NScripto.CSharp
{
    public class CSharpScriptCompiler
    {
        private readonly MethodDelegater _methodDelegater = new MethodDelegater();

        public IRawScript CompileScript(string script, params Type[] delegatedTypes)
        {
            if(script.IsNullOrWhiteSpace())
            {
                return new EmptyScript();
            }

            var compileUnit = new CodeCompileUnit();
            var codeProvider = new CSharpCodeProvider();
            var results = GetCompilerResults(script, delegatedTypes, compileUnit, codeProvider);

            if (results.Errors.HasErrors)
            {
                var errors = results.Errors.OfType<CompilerError>().ToArray();

                var code = BuildCodeString(codeProvider, compileUnit);
                throw new ScriptCompilationException("Compilation failed: " + string.Join(Environment.NewLine, errors.Select(x => x.ErrorText)), errors, code);
            }

            return new RawScriptWrapper(results.CompiledAssembly.GetTypes().First(), delegatedTypes.Length);
        }

        private CompilerResults GetCompilerResults(string script, Type[] delegatedTypes, CodeCompileUnit compileUnit, CSharpCodeProvider codeProvider)
        {
            var parameters = new CompilerParameters();

            var codeNamespace = AddNamespace(compileUnit);
            var typeDeclaration = AddClass(codeNamespace);

            DelegateMethods(typeDeclaration, delegatedTypes);

            AddRunMethod(typeDeclaration, script);

            parameters.GenerateInMemory = true;
            parameters.GenerateExecutable = false;
            parameters.ReferencedAssemblies.Add(typeof (DefaultParameterValueAttribute).Assembly.Location);
            parameters.ReferencedAssemblies.Add(typeof (IScriptRunnable).Assembly.Location);

            foreach (var delegatedType in delegatedTypes)
            {
                parameters.ReferencedAssemblies.Add(delegatedType.Assembly.Location);
            }

            return codeProvider.CompileAssemblyFromDom(parameters, compileUnit);
        }

        public string ShowGeneratedCode(string script, params Type[] delegatedTypes)
        {
            var compileUnit = new CodeCompileUnit();
            var codeProvider = new CSharpCodeProvider();
            
            GetCompilerResults(script, delegatedTypes, compileUnit, codeProvider);

            return BuildCodeString(codeProvider, compileUnit);
        }

        private void DelegateMethods(CodeTypeDeclaration typeDeclaration, Type[] delegatedTypes)
        {
            var initializeMethod = new CodeMemberMethod();
            initializeMethod.Name = "Initialize";
            initializeMethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            var codeParameter = new CodeParameterDeclarationExpression(typeof (object[]), "objs");
            initializeMethod.Parameters.Add(codeParameter);

            for (var i = 0; i < delegatedTypes.Length;i++ )
            {
                var delegatedType = delegatedTypes[i];
                
                string instanceName = GenerateInstanceName(delegatedType);

                var fieldDeclaration = new CodeMemberField(delegatedType, instanceName);
                typeDeclaration.Members.Add(fieldDeclaration);

                foreach (var methodInfo in delegatedType.GetMethods(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (methodInfo.DeclaringType == typeof(object) || methodInfo.Attributes.HasFlag(MethodAttributes.SpecialName))
                    {
                        continue;
                    }

                    var delegateMethod = _methodDelegater.BuildDelegateMethod(methodInfo, instanceName);

                    if (delegateMethod != null)
                    {
                        typeDeclaration.Members.Add(delegateMethod);
                    }
                }

                var argumentReference = new CodeArgumentReferenceExpression("objs");
                var codeArrayIndexerExpression = new CodeArrayIndexerExpression(argumentReference, new CodeSnippetExpression(i.ToString()));

                var castExpression = new CodeCastExpression(delegatedType, codeArrayIndexerExpression);
                var assignmentStatement = new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), instanceName), castExpression);

                initializeMethod.Statements.Add(assignmentStatement);
            }

            typeDeclaration.Members.Add(initializeMethod);
        }

        private static string GenerateInstanceName(Type delegatedType)
        {
            string typeName = delegatedType.Name;

            if (typeName.IsNullOrWhiteSpace())
            {
                return string.Empty;
            }

            return string.Format("{0}{1}", typeName.Substring(0, 1).ToLowerInvariant(), typeName.Substring(1));
        }

        private static CodeTypeDeclaration AddClass(CodeNamespace codeNamespace)
        {
            var typeDeclaration = new CodeTypeDeclaration("Script");
            codeNamespace.Types.Add(typeDeclaration);

            typeDeclaration.BaseTypes.Add(typeof (IScriptRunnable));
            return typeDeclaration;
        }

        private static CodeNamespace AddNamespace(CodeCompileUnit compileUnit)
        {
            var codeNamespace = new CodeNamespace("Scripts");

            var systemImport = new CodeNamespaceImport("System");
            codeNamespace.Imports.Add(systemImport);

            compileUnit.Namespaces.Add(codeNamespace);
            return codeNamespace;
        }

        private static void AddRunMethod(CodeTypeDeclaration typeDeclaration, string script)
        {
            var runMethod = new CodeMemberMethod();
            typeDeclaration.Members.Add(runMethod);

            runMethod.Name = "Run";
            runMethod.Attributes = MemberAttributes.Public;

            if (!script.IsNullOrWhiteSpace())
            {
                runMethod.Statements.Add(new CodeSnippetStatement(script + ";"));
            }
        }

        private static string BuildCodeString(CodeDomProvider codeProvider, CodeCompileUnit compileUnit)
        {
            using (var stringWriter = new StringWriter())
            {
                using (var indentedTextWriter = new IndentedTextWriter(stringWriter))
                {
                    var options = new CodeGeneratorOptions();

                    options.BracingStyle = "C";
                    codeProvider.GenerateCodeFromCompileUnit(compileUnit, indentedTextWriter, options);
                }

                var builder = stringWriter.GetStringBuilder();
                return builder.ToString();
            }
        }
    }
}