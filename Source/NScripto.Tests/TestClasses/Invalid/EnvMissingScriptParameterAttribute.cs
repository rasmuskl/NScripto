﻿using System.Linq;
using System.Collections.Generic;
using System;
using NScripto.Documentation;

namespace NScripto.Tests.TestClasses.Invalid
{
    [ScriptEnvironment("test", "test desc")]
    public class EnvMissingScriptParameterAttribute
    {
        [ScriptMethod("Runs the world.")]
        public void Run(int x)
        {
            
        }
    }
}