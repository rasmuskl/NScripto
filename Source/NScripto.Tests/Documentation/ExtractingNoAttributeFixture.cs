﻿using System.Linq;
using NScripto.Documentation;
using NScripto.Tests.TestClasses;
using NUnit.Framework;
using Should;

namespace NScripto.Tests.Documentation
{
    public class ExtractingNoAttributeFixture : SpecBase
    {
        private ScriptDocumentationExtractor _extractor;
        private ScriptDocumentation _documentation;

        protected override void Arrange()
        {
            _extractor = new ScriptDocumentationExtractor();
        }

        protected override void Act()
        {
            _documentation = _extractor.ExtractDocumentation(typeof (TestNotMarkedAsEnvironment));
        }

        [Test]
        public void WontAddClassesWithoutAttributeToEnvironments()
        {
            _documentation.Environments.Count().ShouldEqual(0);
        }
    }
}