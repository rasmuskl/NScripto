using System;

namespace NScripto.Tests.TestClasses
{
    public class TestScriptArity4
    {
        private readonly IScript<object, EventArgs, IndexOutOfRangeException, Random> _script;

        public TestScriptArity4(IScript<object, EventArgs, IndexOutOfRangeException, Random> script)
        {
            _script = script;
        }

        public void Run()
        {
            _script.Run(new object(), new EventArgs(), new IndexOutOfRangeException(), new Random());
        }
    }
}