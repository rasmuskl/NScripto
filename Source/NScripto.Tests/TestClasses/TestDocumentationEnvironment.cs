using NScripto.Documentation;

namespace NScripto.Tests.TestClasses
{
    [ScriptEnvironment("name", "environment description")]
    public class TestDocumentationEnvironment
    {
        [ScriptMethod("method description")]
        public void Method()
        {
            
        }

        [ScriptMethod("method2 description")]
        [ScriptParameter("a", "This is a.")]
        [ScriptParameter("c", "This is c.")]
        [ScriptParameter("b", "This is b.")]
        public void Method2(string a, string b, string c)
        {
            
        }
    }
}