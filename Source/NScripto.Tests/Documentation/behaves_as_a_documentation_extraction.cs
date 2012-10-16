using System.Linq;
using System.Collections.Generic;
using System;
using NScripto.Documentation;

namespace NScripto.Tests.Documentation
{
    public abstract class behaves_as_a_documentation_extraction : SpecBase
    {
        private ScriptDocumentationExtractor _extractor;
        protected ScriptDocumentation _documentation;

        protected override void Arrange()
        {
            _extractor = new ScriptDocumentationExtractor();
        }

        protected override void Act()
        {
            _documentation = _extractor.Extract(GetExtractedType());
        }

        protected abstract Type GetExtractedType();
    }
}