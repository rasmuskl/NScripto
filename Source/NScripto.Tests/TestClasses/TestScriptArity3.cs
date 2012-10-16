using System;

namespace NScripto.Tests.TestClasses
{
    public class TestScriptArity3
    {
        private readonly IScript<object, EventArgs, IndexOutOfRangeException> _script;

        public TestScriptArity3(IScript<object, EventArgs, IndexOutOfRangeException> script)
        {
            _script = script;
        }

        public void Run()
        {
            _script.Run(new object(), new EventArgs(), new IndexOutOfRangeException());
        }
    }
}