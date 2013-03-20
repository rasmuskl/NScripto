using System;
using System.Reflection;
using NScripto.Documentation;

namespace NScripto.Verification.Errors
{
    public class UnmatchedScriptParameterAttributeVerificationError : IVerificationError
    {
        public UnmatchedScriptParameterAttributeVerificationError(Type type, MethodInfo methodInfo, ScriptParameterAttribute attribute)
        {
            Type = type;
            MethodInfo = methodInfo;
            Attribute = attribute;
        }

        public Type Type { get; private set; }
        public MethodInfo MethodInfo { get; private set; }
        public ScriptParameterAttribute Attribute { get; private set; }

        public string Message { get { return "Unmatched script parameter attribute (" + Attribute.Name + ") in environment: " + Type.Name + ", method: " + MethodInfo.Name; } }
    }
}