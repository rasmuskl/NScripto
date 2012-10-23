using System.Linq;
using System.Collections.Generic;
using System;
using System.Reflection;
using NScripto.Documentation;
using NScripto.Documentation.Model;
using NScripto.Tests.TestClasses;
using NScripto.Tests.TestClasses.Nested;
using NUnit.Framework;

namespace NScripto.Tests.Documentation
{
    public class ScanningAssemblyFixture : SpecBase
    {
        private ScriptEnvironmentScanner _scanner;
        private ScriptEnvironmentTypeResult _result;

        protected override void Arrange()
        {
            _scanner = new ScriptEnvironmentScanner();
        }

        protected override void Act()
        {
            _result = _scanner.Scan(Assembly.GetExecutingAssembly());
        }

        [Test]
        public void ItShouldFindTheSameAsASpecializedNamespaceSearch()
        {
            Assert.That(_result.EnvironmentTypes.Contains(typeof(SampleScriptEnvironment)));
            Assert.That(_result.EnvironmentTypes.Contains(typeof(NestedSampleScriptEnvironment)));
        }
    }
}