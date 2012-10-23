using System.Linq;
using NScripto.Documentation;
using NScripto.Documentation.Model;
using NScripto.Tests.TestClasses;
using NUnit.Framework;

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
            _documentation = _extractor.Extract(typeof (TestNotMarkedAsEnvironment));
        }

        [Test]
        public void WontAddClassesWithoutAttributeToEnvironments()
        {
            Assert.That(_documentation.Environments.Count(), Is.EqualTo(0));
        }
    }
}