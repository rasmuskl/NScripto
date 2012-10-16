using System.Linq;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace NScripto.Documentation
{
    public class ExtractingScriptDocumentationService : IScriptDocumentationService
    {
        public ScriptDocumentation GetDocumentation(Assembly assembly)
        {
            var scanner = new ScriptEnvironmentScanner();
            var environmentTypes = scanner.Scan(assembly);

            var extractor = new ScriptDocumentationExtractor();
            return extractor.Extract(environmentTypes);
        }
    }
}