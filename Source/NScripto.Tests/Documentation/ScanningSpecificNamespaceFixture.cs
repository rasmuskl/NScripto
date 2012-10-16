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
    public class ScanningSpecificNamespaceFixture : SpecBase
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
        public void ItShouldFindTheCorrectNumberOfEnvironments()
        {
            Assert.That(_result.EnvironmentCount, Is.GreaterThanOrEqualTo(2));
        }

        [Test]
        public void ItShouldFindEnvironmentsInTheNamespace()
        {
            Assert.That(_result.EnvironmentTypes.Contains(typeof(SampleScriptEnvironment)));            
        }

        [Test]
        public void ItShouldFindEnvironmentsNestedFromTheNamespace()
        {
            Assert.That(_result.EnvironmentTypes.Contains(typeof(NestedSampleScriptEnvironment)));
        }
    }
}