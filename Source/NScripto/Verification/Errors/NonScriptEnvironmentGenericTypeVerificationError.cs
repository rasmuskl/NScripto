using System.Linq;
using System.Collections.Generic;
using System;

namespace NScripto.Verification.Errors
{
    public class NonScriptEnvironmentGenericTypeVerificationError : IVerificationError
    {
        public NonScriptEnvironmentGenericTypeVerificationError(Type type, Type nonScriptEnvironmentType)
        {
            Type = type;
            NonScriptEnvironmentType = nonScriptEnvironmentType;
        }

        public Type Type { get; private set; }
        public Type NonScriptEnvironmentType { get; private set; }

        public string Message { get { return "Script type in constructor contains a non-ScriptEnvironment type: " + Type.Name + ", method: " + NonScriptEnvironmentType.Name; } }
    }
}