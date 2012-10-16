using System.Linq;
using NScripto.Documentation;
using NScripto.Tests.TestClasses;
using NUnit.Framework;

namespace NScripto.Tests.Documentation
{
    [TestFixture]
    public class ScriptDocumentationExtractorFixture
    {
        private ScriptDocumentationExtractor _extractor;

        [SetUp]
        public void BeforeEachTest()
        {
            _extractor = new ScriptDocumentationExtractor();
        }

        [Test]
        public void WontAddClassesWithoutAttributeToEnvironments()
        {
            var documentation = _extractor.Extract(typeof(TestNotMarkedAsEnvironment));
            Assert.That(documentation.Environments.Count(), Is.EqualTo(0));
        }

        [Test]
        public void TheDocumentationShouldContainExactlyOneEnvironment()
        {
            var documentation = _extractor.Extract(typeof(TestDocumentationEnvironment));
            Assert.That(documentation.Environments.Count(), Is.EqualTo(1));
        }

        [Test]
        public void TheDocumentationShouldContainInformationAboutTheEnvironmentName()
        {
            var documentation = _extractor.Extract(typeof(TestDocumentationEnvironment));
            Assert.That(documentation.Environments.Any(x => x.Name == "name"));
        }

        [Test]
        public void TheDocumentationShouldContainTheDescriptionOfTheEnvironment()
        {
            var documentation = _extractor.Extract(typeof(TestDocumentationEnvironment));
            Assert.That(documentation.Environments.Any(x => x.Description == "environment description"));
        }

        [Test]
        public void TheDocumentationShouldContainMethodNamesOfScriptMethods()
        {
            var documentation = _extractor.Extract(typeof(TestDocumentationEnvironment));
            Assert.That(documentation.Environments.First().Methods.Any(x => x.Name == "Method"));
            Assert.That(documentation.Environments.First().Methods.Any(x => x.Name == "Method2"));
        }

        [Test]
        public void TheDocumentataionShouldContainDescriptionsOfScriptMethods()
        {
            var documentation = _extractor.Extract(typeof(TestDocumentationEnvironment));
            Assert.That(documentation.Environments.First().Methods.Any(x => x.Name == "Method" && x.Description == "method description"));
            Assert.That(documentation.Environments.First().Methods.Any(x => x.Name == "Method2" && x.Description == "method2 description"));
        }

        [Test]
        public void TheDocumentationShouldContainParameters()
        {
            var documentation = _extractor.Extract(typeof(TestDocumentationEnvironment));
            var methodDoc = documentation.Environments.First().Methods.First(x => x.Name == "Method2");

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