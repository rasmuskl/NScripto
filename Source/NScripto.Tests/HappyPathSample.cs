using NScripto.CSharp;
using NUnit.Framework;
using Should;

namespace NScripto.Tests
{
    [TestFixture]
    public class HappyPathSample
    {
        private ScriptApi _scriptApi;

        [SetUp]
        public void BeforeEachTest()
        {
            _scriptApi = new ScriptApi(new CSharpScriptCompiler());
        }

        [Test]
        public void Meh()
        {
            var happyScript = _scriptApi.CompileWrappedScript<HappyScript>("");
            happyScript.Run().ShouldEqual("Moody");
        }

        [Test]
        public void Yay()
        {
            var happyScript = _scriptApi.CompileWrappedScript<HappyScript>("Happy()");
            happyScript.Run().ShouldEqual("Happy!");
        }

        [Test]
        public void Noo()
        {
            var happyScript = _scriptApi.CompileWrappedScript<HappyScript>("Unhappy()");
            happyScript.Run().ShouldEqual("Unhappy!");
        }

        public class HappyScript
        {
            private readonly IScript<HappyEnvironment> _script;

            public HappyScript(IScript<HappyEnvironment> script)
            {
                _script = script;
            }

            public string Run()
            {
                var happyEnvironment = new HappyEnvironment("Moody");
                _script.Run(happyEnvironment);
                return happyEnvironment.State;
            }
        }

        public class HappyEnvironment
        {
            public HappyEnvironment(string initialState)
            {
                State = initialState;
            }

            public string State { get; set; }

            public void Happy()
            {
                State = "Happy!";
            }

            public void Unhappy()
            {
                State = "Unhappy!";
            }
        }
    }
}