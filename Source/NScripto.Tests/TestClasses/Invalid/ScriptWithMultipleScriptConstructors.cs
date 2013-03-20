namespace NScripto.Tests.TestClasses.Invalid
{
    public class ScriptWithMultipleScriptConstructors
    {
        public ScriptWithMultipleScriptConstructors(IScript<SampleScriptEnvironment> script)
        {
            
        } 

        public ScriptWithMultipleScriptConstructors(IScript<SampleScriptEnvironment, TestDocumentationEnvironment> script)
        {
            
        } 
    }
}