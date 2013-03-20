using System;
using NScripto.Verification;

namespace NScripto.Exceptions
{
    public class ScriptVerificationException : Exception
    {
        private readonly IVerificationError[] _errors;

        public ScriptVerificationException(IVerificationError[] errors)
        {
            _errors = errors;
        }

        public IVerificationError[] Errors
        {
            get { return _errors; }
        }
    }
}