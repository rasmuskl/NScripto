﻿using System.Linq;
using System.Collections.Generic;
using System;

namespace NScripto.Verification
{
    public class MissingScriptMethodAttributeVerificationError : IVerificationError
    {
        public MissingScriptMethodAttributeVerificationError(Type type)
        {
            Type = type;
        }

        public Type Type { get; private set; }
    }
}