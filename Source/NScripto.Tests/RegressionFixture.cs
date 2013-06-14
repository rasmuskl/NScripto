using NScripto.Tests.TestClasses;
using NUnit.Framework;

namespace NScripto.Tests
{
    [TestFixture]
    public class RegressionFixture
    {
        [Test]
        public void CanCompileEmptyScriptTwice()
        {
            var scriptApi = new ScriptApi();

            scriptApi.CompileWrappedScript<TestScriptArity4>("");
            scriptApi.CompileWrappedScript<TestScriptArity4>("");
        }
    }
}