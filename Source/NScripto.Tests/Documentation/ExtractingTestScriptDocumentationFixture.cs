using NScripto.Documentation;
using NScripto.Documentation.Model;
using NScripto.Tests.TestClasses;
using NScripto.Tests.TestClasses.ScriptTypes;
using NUnit.Framework;
using System.Linq;
using Should;

namespace NScripto.Tests.Documentation
{
    [TestFixture]
    public class ExtractingTestScriptDocumentationFixture : SpecBase
    {
        private ScriptDocumentationExtractor _extractor;
        private ScriptDocumentation _documentation;

        protected override void Arrange()
        {
            _extractor = new ScriptDocumentationExtractor();
        }

        protected override void Act()
        {
            _documentation = _extractor.ExtractScriptDocumentation(typeof(DocumentedScript));
        }

        [Test]
        public void ShouldContainAScriptType()
        {
            _documentation.Scripts.Count().ShouldEqual(1);
        }

        [Test]
        public void ShouldGetNameFromAttribute()
        {
            _documentation.Scripts.Single().Name.ShouldEqual("Documented Script");
        }

        [Test]
        public void ShouldGetDescriptionFromAttribute()
        {
            _documentation.Scripts.Single().Description.ShouldEqual("It's like a script - but documented.");
        }

        [Test]
        public void ShouldContainCorrectScriptType()
        {
            _documentation.Scripts.Single().ScriptType.ShouldEqual(typeof (DocumentedScript));
        }

        [Test]
        public void ShouldContainScriptTypeEnvironment()
        {
            _documentation.Scripts.Single().Environments.Single().EnvironmentType.ShouldEqual(typeof(TestDocumentationEnvironment));
        }
    }
}