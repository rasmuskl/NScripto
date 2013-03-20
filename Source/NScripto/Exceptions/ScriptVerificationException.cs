using System;
using System.Collections.Generic;
using System.Text;
using NScripto.Verification;

namespace NScripto.Exceptions
{
    public class ScriptVerificationException : Exception
    {
        private readonly IVerificationError[] _errors;

        public ScriptVerificationException(IVerificationError[] errors) : base(BuildMessage(errors))
        {
            _errors = errors;
            
        }

        public IVerificationError[] Errors
        {
            get { return _errors; }
        }

        private static string BuildMessage(IEnumerable<IVerificationError> errors)
        {
            var messageBuilder = new StringBuilder();

            messageBuilder.AppendLine("Script verification failed:" + Environment.NewLine);

            foreach (var error in errors)
            {
                messageBuilder.AppendLine(string.Format(" - {0}", error.Message));
            }

            return messageBuilder.ToString();
        }
    }
}