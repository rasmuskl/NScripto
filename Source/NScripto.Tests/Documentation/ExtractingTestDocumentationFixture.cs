using System.Linq;
using NScripto.Documentation;
using NScripto.Tests.TestClasses;
using NUnit.Framework;

namespace NScripto.Tests.Documentation
{
    [TestFixture]
    public class ExtractingTestDocumentationFixture : SpecBase
    {
        private ScriptDocumentationExtractor _extractor;
        private ScriptDocumentation _documentation;

        protected override void Arrange()
        {
            _extractor = new ScriptDocumentationExtractor();
        }

        protected override void Act()
        {
            _documentation = _extractor.Extract(typeof(TestDocumentationEnvironment));
        }

        [Test]
        public void TheDocumentationShouldContainExactlyOneEnvironment()
        {
            Assert.That(_documentation.Environments.Count(), Is.EqualTo(1));
        }

        [Test]
        public void TheDocumentationShouldContainInformationAboutTheEnvironmentName()
        {
            Assert.That(_documentation.Environments.Any(x => x.Name == "name"));
        }

        [Test]
        public void TheDocumentationShouldContainTheDescriptionOfTheEnvironment()
        {
            Assert.That(_documentation.Environments.Any(x => x.Description == "environment description"));
        }

        [Test]
        public void TheDocumentationShouldContainMethodNamesOfScriptMethods()
        {
            Assert.That(_documentation.Environments.First().Methods.Any(x => x.Name == "Method"));
            Assert.That(_documentation.Environments.First().Methods.Any(x => x.Name == "Method2"));
        }

        [Test]
        public void TheDocumentataionShouldContainDescriptionsOfScriptMethods()
        {
            Assert.That(_documentation.Environments.First().Methods.Any(x => x.Name == "Method" && x.Description == "method description"));
            Assert.That(_documentation.Environments.First().Methods.Any(x => x.Name == "Method2" && x.Description == "method2 description"));
        }

        [Test]
        public void TheDocumentationShouldContainParameters()
        {
            var methodDoc = _documentation.Environments.First().Methods.First(x => x.Name == "Method2");

            Assert.That(methodDoc.Parameters.Count(), Is.EqualTo(3));

            var enumerator = methodDoc.Parameters.GetEnumerator();

            enumerator.MoveNext();
            Assert.That(enumerator.Current.Name, Is.EqualTo("a"));
            enumerator.MoveNext();
            Assert.That(enumerator.Current.Name, Is.EqualTo("b"));
            enumerator.MoveNext();
            Assert.That(enumerator.Current.Name, Is.EqualTo("c"));
        }
    }
}