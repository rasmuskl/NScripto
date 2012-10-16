using System.Linq;
using System.Collections.Generic;
using System;
using NScripto.Documentation;
using NUnit.Framework;

namespace NScripto.Tests.Documentation
{
    public class when_extracting_documentation_from_a_valid_script_environment : behaves_as_a_documentation_extraction
    {
        protected override Type GetExtractedType()
        {
            return typeof (TestDocumentationEnvironment);
        }

        [Test]
        public void the_documentation_should_contain_exactly_one_environment()
        {
            Assert.That(_documentation.Environments.Count(), Is.EqualTo(1));
        }

        [Test]
        public void the_documentation_should_contain_information_about_the_environment_name()
        {
            Assert.That(_documentation.Environments.Any(x => x.Name == "name"));
        }

        [Test]
        public void the_documentation_should_contain_the_description_of_the_environment()
        {
            Assert.That(_documentation.Environments.Any(x => x.Description == "environment description"));
        }

        [Test]
        public void the_documentation_should_contain_method_names_of_script_methods()
        {
            Assert.That(_documentation.Environments.First().Methods.Any(x => x.Name == "Method"));
            Assert.That(_documentation.Environments.First().Methods.Any(x => x.Name == "Method2"));
        }

        [Test]
        public void the_documentataion_should_contain_descriptions_of_script_methods()
        {
            Assert.That(_documentation.Environments.First().Methods.Any(x => x.Name == "Method" && x.Description == "method description"));
            Assert.That(_documentation.Environments.First().Methods.Any(x => x.Name == "Method2" && x.Description == "method2 description"));
        }

        [Test]
        public void the_documentation_should_contain_parameters()
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

    [ScriptEnvironment("name", "environment description")]
    public class TestDocumentationEnvironment
    {
        [ScriptMethod("method description")]
        public void Method()
        {
            
        }

        [ScriptMethod("method2 description")]
        [ScriptParameter("a", "This is a.")]
        [ScriptParameter("c", "This is c.")]
        [ScriptParameter("b", "This is b.")]
        public void Method2(string a, string b, string c)
        {
            
        }
    }
}