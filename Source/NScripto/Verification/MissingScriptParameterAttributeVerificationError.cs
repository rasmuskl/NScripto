using System.Linq;
using System.Collections.Generic;
using System;

namespace NScripto.Verification
{
    public class MissingScriptParameterAttributeVerificationError : IVerificationError
    {
        public MissingScriptParameterAttributeVerificationError(Type type)
        {
            Type = type;
        }

        public Type Type { get; private set; }
    }
}