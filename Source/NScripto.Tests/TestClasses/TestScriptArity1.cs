namespace NScripto.Tests.TestClasses
{
    public class TestScriptArity1
    {
        private readonly IScript<object> _script;

        public TestScriptArity1(IScript<object> script)
        {
            _script = script;
        }

        public void Run()
        {
            _script.Run(new object());
        }
    }
}