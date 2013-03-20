using System;

namespace NScripto.Exceptions
{
    public class MultipleScriptConstructorsException : Exception
    {
        public Type ScriptType { get; set; }

        public MultipleScriptConstructorsException(Type scriptType)
        {
            ScriptType = scriptType;
        }
    }
}