using System.Linq;
using System.Collections.Generic;
using System;

namespace NScripto.Exceptions
{
    public class NoScriptConstructorsException : Exception
    {
        public Type ScriptType { get; set; }

        public NoScriptConstructorsException(Type scriptType)
        {
            ScriptType = scriptType;
        }
    }
}