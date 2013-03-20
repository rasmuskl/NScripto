using System.Linq;
using System.Collections.Generic;
using System;
using NScripto.Documentation;

namespace NScripto.Tests.TestClasses.Invalid
{
    [ScriptEnvironment("test", "test desc")]
    public class EnvWrongScriptParameterAttributeName
    {
        [ScriptMethod("Runs the world.")]
        [ScriptParameter("x", "desc")]
        [ScriptParameter("y", "desc")]
        public void Run(int x)
        {
            
        }
    }
}