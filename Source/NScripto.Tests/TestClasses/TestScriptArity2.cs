using System;

namespace NScripto.Tests.TestClasses
{
    public class TestScriptArity2
    {
        private readonly IScript<object, EventArgs> _script;

        public TestScriptArity2(IScript<object, EventArgs> script)
        {
            _script = script;
        }

        public void Run()
        {
            _script.Run(new object(), new EventArgs());
        }
    }
}