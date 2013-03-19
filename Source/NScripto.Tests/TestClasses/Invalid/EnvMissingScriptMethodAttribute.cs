using NScripto.Documentation.Attributes;

namespace NScripto.Tests.TestClasses.Invalid
{
    [ScriptEnvironment("test", "test desc")]
    public class EnvMissingScriptMethodAttribute
    {
        public void Run()
        {
            
        }
    }
}