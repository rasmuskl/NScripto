using System.CodeDom;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using NScripto.Documentation;
using NScripto.Documentation.Attributes;

namespace NScripto.CodeDom
{
    public class MethodDelegater
    {
        public CodeMemberMethod BuildDelegateMethod(MethodInfo method, string instanceName)
        {
            if (method.GetCustomAttributes(typeof(NoScriptMethodAttribute), true).Any())
            {
                return null;
            }

            var delegatingMethod = new CodeMemberMethod();
            delegatingMethod.Name = method.Name;
            delegatingMethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;

            delegatingMethod.ReturnType = new CodeTypeReference(method.ReturnType);

            var invokeExpression = new CodeMethodInvokeExpression(new CodeFieldReferenceExpression(null, instanceName), method.Name);
            invokeExpression.Parameters.AddRange(BuildArguments(method));
            var bodyStatement = GetReturnStatement(method, invokeExpression);

            delegatingMethod.Statements.Add(bodyStatement);
            delegatingMethod.Parameters.AddRange(BuildParameters(method));

            return delegatingMethod;
        }

        private CodeStatement GetReturnStatement(MethodInfo method, CodeMethodInvokeExpression methodInvoke)
        {
            if (method.ReturnType == typeof(void))
            {
                return new CodeExpressionStatement(methodInvoke);
            }

            return new CodeMethodReturnStatement(methodInvoke);
        }

        private CodeExpression[] BuildArguments(MethodInfo method)
        {
            return method.GetParameters()
                .Select(x => new CodeArgumentReferenceExpression(x.Name))
                .OfType<CodeExpression>()
                .ToArray();
        }

        private CodeParameterDeclarationExpression[] BuildParameters(MethodInfo method)
        {
            return method.GetParameters()
                .Select(x => new CodeParameterDeclarationExpression(x.ParameterType, x.Name) { CustomAttributes = BuildOptionalParameterAttributes(x.RawDefaultValue)})
                .ToArray();
        }

        private static CodeAttributeDeclarationCollection BuildOptionalParameterAttributes(object defaultValue)
        {
            if (defaultValue == DBNull.Value)
            {
                return new CodeAttributeDeclarationCollection();
            }

            var optionalAttribute = new CodeAttributeDeclaration(new CodeTypeReference(typeof(OptionalAttribute)));
            var defaultParameterAttribute = new CodeAttributeDeclaration(new CodeTypeReference(typeof (DefaultParameterValueAttribute)), new CodeAttributeArgument(new CodePrimitiveExpression(defaultValue)));
            return new CodeAttributeDeclarationCollection(new [] { optionalAttribute, defaultParameterAttribute });
        }
    }
}