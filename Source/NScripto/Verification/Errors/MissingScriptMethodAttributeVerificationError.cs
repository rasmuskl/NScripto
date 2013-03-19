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
    }
}