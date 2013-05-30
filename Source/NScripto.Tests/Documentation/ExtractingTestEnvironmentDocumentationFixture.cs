using System.Linq;
using NScripto.Documentation;
using NScripto.Tests.TestClasses;
using NUnit.Framework;
using Should;

namespace NScripto.Tests.Documentation
{
    [TestFixture]
    public class ExtractingTestEnvironmentDocumentationFixture : SpecBase
    {
        private ScriptDocumentationExtractor _extractor;
        private ScriptDocumentation _documentation;

        protected override void Arrange()
        {
            _extractor = new ScriptDocumentationExtractor();
        }

        protected override void Act()
        {
            _documentation = _extractor.ExtractDocumentation(typeof(TestDocumentationEnvironment));
        }

        [Test]
        public void TheDocumentationShouldContainExactlyOneEnvironment()
        {
            _documentation.Environments.Count().ShouldEqual(1);
        }

        [Test]
        public void TheDocumentationShouldContainInformationAboutTheEnvironmentName()
        {
            _documentation.Environments.Any(x => x.Name == "name").ShouldBeTrue();
        }

        [Test]
        public void TheDocumentationShouldContainTheDescriptionOfTheEnvironment()
        {
            _documentation.Environments.Any(x => x.Description == "environment description").ShouldBeTrue();
        }

        [Test]
        public void TheDocumentationShouldContainMethodNamesOfScriptMethods()
        {
            _documentation.Environments.First().Methods.Any(x => x.Name == "Method").ShouldBeTrue();
            _documentation.Environments.First().Methods.Any(x => x.Name == "Method2").ShouldBeTrue();
        }

        [Test]
        public void TheDocumentataionShouldContainDescriptionsOfScriptMethods()
        {
            _documentation.Environments.First().Methods.Any(x => x.Name == "Method" && x.Description == "method description").ShouldBeTrue();
            _documentation.Environments.First().Methods.Any(x => x.Name == "Method2" && x.Description == "method2 description").ShouldBeTrue();
        }

        [Test]
        public void TheDocumentationShouldContainParameters()
        {
            var methodDoc = _documentation.Environments.First().Methods.First(x => x.Name == "Method2");

            Assert.That(methodDoc.Parameters.Count(), Is.EqualTo(3));

            var enumerator = methodDoc.Parameters.GetEnumerator();

            enumerator.MoveNext();
            enumerator.Current.Name.ShouldEqual("a");
            enumerator.MoveNext();
            enumerator.Current.Name.ShouldEqual("b");
            enumerator.MoveNext();
            enumerator.Current.Name.ShouldEqual("c");
        }
    }
}