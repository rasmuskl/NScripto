using NScripto.Documentation;
using NScripto.Documentation.Attributes;

namespace NScripto.Tests.TestClasses.ScriptTypes
{
    [Script("Documented Script", "It's like a script - but documented.")]
    public class DocumentedScript
    {
        public IScript<TestDocumentationEnvironment> Script { get; set; }

        public DocumentedScript(IScript<TestDocumentationEnvironment> script)
        {
            Script = script;
        }
    }
}