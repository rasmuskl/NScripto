using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security;
using NScripto.CSharp;
using NScripto.Exceptions;
using NScripto.Verification;
using NScripto.Wrappers;

namespace NScripto
{
    public class ScriptApi
    {
        private readonly CSharpScriptCompiler _scriptCompiler;

        public ScriptApi(CSharpScriptCompiler scriptCompiler)
        {
            _scriptCompiler = scriptCompiler;
        }

        public T CompileScript<T>(string scriptText)
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

            var openGenericScriptType = GetGenericWrapperOfArity(genericArguments.Count());
            var closedGenericScriptType = openGenericScriptType.MakeGenericType(genericArguments);

            var script = _scriptCompiler.CompileScript(scriptText, genericArguments);
            var genericScript = Activator.CreateInstance(closedGenericScriptType, script);

            return (T)Activator.CreateInstance(typeof(T), new[] { genericScript }, new object[0]);
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