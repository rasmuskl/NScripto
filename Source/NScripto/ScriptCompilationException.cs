using System.CodeDom.Compiler;
using System.Linq;
using System.Collections.Generic;
using System;

namespace NScripto
{
    public class ScriptCompilationException : Exception
    {
        public CompilerError[] Errors { get; set; }
        public string Code { get; set; }

        public ScriptCompilationException(string message, CompilerError[] errors, string code) : base(message)
        {
            Errors = errors;
            Code = code;
        }
    }
}