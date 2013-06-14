using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NScripto.CSharp;
using NScripto.Cache;
using NScripto.Documentation;
using NScripto.Exceptions;
using NScripto.Raw;
using NScripto.Verification;
using NScripto.Wrappers;

namespace NScripto
{
    public class ScriptApi
    {
        private readonly CSharpScriptCompiler _scriptCompiler = new CSharpScriptCompiler();
        private readonly ScriptDocumentationExtractor _documentationExtractor = new ScriptDocumentationExtractor();

        public T CompileWrappedScript<T>(string scriptText)
        {
            var scriptType = typeof(T);

            var scriptConstructors = scriptType.GetConstructors().Where(IsScriptConstructor).ToArray();

            if (scriptConstructors.Length > 1)
            {
                throw new MultipleScriptConstructorsException(typeof(T));
            }

            if (scriptConstructors.Length == 0)
            {
                throw new NoScriptConstructorsException(typeof(T));
            }

            var scriptConstructor = scriptConstructors.Single();
            var scriptParameter = scriptConstructor.GetParameters().Single();
            var genericArguments = scriptParameter.ParameterType.GetGenericArguments();

            var genericScript = CompileGenericScript(scriptText, genericArguments);

            return (T)Activator.CreateInstance(typeof(T), new[] { genericScript }, new object[0]);
        }

        public IScript<T> CompileScript<T>(string scriptText)
        {
            var genericScript = CompileGenericScript(scriptText, new[] { typeof(T) });
            return (IScript<T>)genericScript;
        }

        public IScript<T, T2> CompileScript<T, T2>(string scriptText)
        {
            var genericScript = CompileGenericScript(scriptText, new[] { typeof(T), typeof(T2) });
            return (IScript<T, T2>)genericScript;
        }

        public IScript<T, T2, T3> CompileScript<T, T2, T3>(string scriptText)
        {
            var genericScript = CompileGenericScript(scriptText, new[] { typeof(T), typeof(T2), typeof(T3) });
            return (IScript<T, T2, T3>)genericScript;
        }

        public IScript<T, T2, T3, T4> CompileScript<T, T2, T3, T4>(string scriptText)
        {
            var genericScript = CompileGenericScript(scriptText, new[] { typeof(T), typeof(T2), typeof(T3), typeof(T4) });
            return (IScript<T, T2, T3, T4>)genericScript;
        }

        private object CompileGenericScript(string scriptText, Type[] genericArguments)
        {
            object cachedGenericScript;
            if (ScriptCache.TryGetCachedScript(scriptText, genericArguments, out cachedGenericScript))
            {
                return cachedGenericScript;
            }

            Type openGenericScriptType = GetGenericWrapperOfArity(genericArguments.Count());
            Type closedGenericScriptType = openGenericScriptType.MakeGenericType(genericArguments);

            IRawScript compiledScript = _scriptCompiler.CompileScript(scriptText, genericArguments);

            var genericScript = Activator.CreateInstance(closedGenericScriptType, compiledScript);
            ScriptCache.AddCachedScript(scriptText, genericArguments, genericScript);

            return genericScript;
        }

        public void VerifyTypes(IEnumerable<Type> types)
        {
            types = types ?? new Type[0];
            var verifier = new ScriptVerifier();
            var errors = verifier.AnalyzeTypes(types.ToArray());

            if (errors.Any())
            {
                throw new ScriptVerificationException(errors);
            }
        }

        public void VerifyTypesInAssembly(Assembly assembly)
        {
            VerifyTypes(assembly.GetTypes());
        }

        public void VerifyTypesInAssemblyOf<T>()
        {
            VerifyTypesInAssembly(typeof(T).Assembly);
        }

        public ScriptDocumentation ExtractDocumentationFromTypes(Type[] types)
        {
            return _documentationExtractor.ExtractDocumentation(types);
        }

        public ScriptDocumentation ExtractDocumentationFromAssembly(Assembly assembly)
        {
            return _documentationExtractor.ExtractDocumentation(assembly.GetTypes());
        }

        public ScriptDocumentation ExtractDocumentationFromAssemblyOf<T>()
        {
            return _documentationExtractor.ExtractDocumentation(typeof(T).Assembly.GetTypes());
        }

        private bool IsScriptConstructor(ConstructorInfo constructor)
        {
            var parameters = constructor.GetParameters();

            if (parameters.Length != 1)
            {
                return false;
            }

            var parameterInfo = parameters.First();
            return typeof(IScript).IsAssignableFrom(parameterInfo.ParameterType);
        }

        private Type GetGenericWrapperOfArity(int arity)
        {
            switch (arity)
            {
                case 1:
                    return typeof(GenericScriptWrapper<>);

                case 2:
                    return typeof(GenericScriptWrapper<,>);

                case 3:
                    return typeof(GenericScriptWrapper<,,>);

                case 4:
                    return typeof(GenericScriptWrapper<,,,>);
            }

            throw new Exception("Arity " + arity + " not supported.");
        }
    }
}