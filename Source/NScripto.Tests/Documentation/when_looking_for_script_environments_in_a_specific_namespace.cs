using System.Linq;
using System.Collections.Generic;
using System;
using System.Reflection;
using NScripto.Documentation;
using NScripto.Tests.TestClasses;
using NScripto.Tests.TestClasses.Nested;
using NUnit.Framework;

namespace NScripto.Tests.Documentation
{
    public class when_looking_for_script_environments_in_a_specific_namespace : SpecBase
    {
        private ScriptEnvironmentScanner _scanner;
        private ScriptEnvironmentTypeResult _result;

        protected override void Arrange()
        {
            _scanner = new ScriptEnvironmentScanner();
        }

        protected override void Act()
        {
            var environmentNamespace = typeof(SampleScriptEnvironment).Namespace;
            _result = _scanner.Scan(Assembly.GetExecutingAssembly(), environmentNamespace);
        }

        [Test]
        public void it_should_find_the_correct_number_of_environments()
        {
            Assert.That(_result.EnvironmentCount, Is.GreaterThanOrEqualTo(2));
        }

        [Test]
        public void it_should_find_environments_in_the_namespace()
        {
            Assert.That(_result.EnvironmentTypes.Contains(typeof(SampleScriptEnvironment)));            
        }

        [Test]
        public void it_should_find_environments_nested_from_the_namespace()
        {
            Assert.That(_result.EnvironmentTypes.Contains(typeof(NestedSampleScriptEnvironment)));
        }
    }
}