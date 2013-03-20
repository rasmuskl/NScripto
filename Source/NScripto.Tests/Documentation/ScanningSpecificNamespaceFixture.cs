using System.Linq;
using System.Collections.Generic;
using System;
using System.Reflection;
using NScripto.Documentation;
using NScripto.Documentation.Tools;
using NScripto.Tests.TestClasses;
using NScripto.Tests.TestClasses.Nested;
using NUnit.Framework;
using Should;

namespace NScripto.Tests.Documentation
{
    public class ScanningSpecificNamespaceFixture : SpecBase
    {
        private ScriptEnvironmentScanner _scanner;
        private IEnumerable<Type> _result;

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
            _result.Count().ShouldBeInRange(2, 1000);
        }

        [Test]
        public void ItShouldFindEnvironmentsInTheNamespace()
        {
            _result.ShouldContain(typeof(SampleScriptEnvironment));
        }

        [Test]
        public void ItShouldFindEnvironmentsNestedFromTheNamespace()
        {
            _result.ShouldContain(typeof(NestedSampleScriptEnvironment));
        }
    }
}