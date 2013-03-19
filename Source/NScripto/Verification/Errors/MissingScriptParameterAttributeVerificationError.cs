using System.Linq;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace NScripto.Verification.Errors
{
    public class MissingScriptParameterAttributeVerificationError : IVerificationError
    {
        public MissingScriptParameterAttributeVerificationError(Type type, MethodInfo methodInfo, ParameterInfo parameterInfo)
        {
            Type = type;
            MethodInfo = methodInfo;
            ParameterInfo = parameterInfo;
        }

        public Type Type { get; private set; }
        public MethodInfo MethodInfo { get; private set; }
        public ParameterInfo ParameterInfo { get; private set; }
    }
}