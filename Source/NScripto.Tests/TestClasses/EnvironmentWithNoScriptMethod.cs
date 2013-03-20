using NScripto.Documentation;

namespace NScripto.Tests.TestClasses
{
    [ScriptEnvironment("test", "desc")]
    public class EnvironmentWithNoScriptMethod
    {
        [NoScriptMethod]
        public void Test()
        {
            
        }
    }
}