using System;
using System.Linq;
using NScripto.CSharp;
using NScripto.Wrappers;

namespace NScripto
{
    public class ScriptFactory
    {
        private readonly CSharpScriptCompiler _scriptCompiler;

        public ScriptFactory(CSharpScriptCompiler scriptCompiler)
        {
            _scriptCompiler = scriptCompiler;
        }

        public T CompileScript<T>(string scriptText)
        {
            var scriptType = typeof(T);

            var parameterInfo = scriptType.GetConstructors().First().GetParameters().First();
            var genericArguments = parameterInfo.ParameterType.GetGenericArguments();

            var openGenericScriptType = GetGenericWrapperOfArity(genericArguments.Count());
            var closedGenericScriptType = openGenericScriptType.MakeGenericType(genericArguments);

            var script = _scriptCompiler.CompileScript(scriptText, genericArguments);
            var genericScript = Activator.CreateInstance(closedGenericScriptType, script);

            return (T)Activator.CreateInstance(typeof(T), new[] { genericScript }, new object[0]);
        }

        private Type GetGenericWrapperOfArity(int count)
        {
            switch (count)
            {
                case 1:
                    return typeof(GenericScriptWrapper<>);

                case 2:
                    return typeof(GenericScriptWrapper<,>);
            }

            throw new Exception("Arity " + count + " not supported.");
        }
    }
}