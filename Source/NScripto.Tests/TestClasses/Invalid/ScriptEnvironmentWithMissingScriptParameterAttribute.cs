using System.Linq;
using System.Collections.Generic;
using System;
using NScripto.Documentation.Attributes;

namespace NScripto.Tests.TestClasses.Invalid
{
    [ScriptEnvironment("test", "test desc")]
    public class ScriptEnvironmentWithMissingScriptParameterAttribute
    {
        [ScriptMethod("Runs the world.")]
        public void Run(int x)
        {
            
        }
    }
}