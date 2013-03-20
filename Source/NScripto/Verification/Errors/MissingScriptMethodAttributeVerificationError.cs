using System.Linq;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace NScripto.Verification.Errors
{
    public class MissingScriptMethodAttributeVerificationError : IVerificationError
    {
        public MissingScriptMethodAttributeVerificationError(Type type, MethodInfo methodInfo)
        {
            Type = type;
            MethodInfo = methodInfo;
        }

        public Type Type { get; private set; }
        public MethodInfo MethodInfo { get; private set; }
        
        public string Message { get { return "Missing script method attribute in environment: " + Type.Name + ", method: " + MethodInfo.Name; } }
    }
}