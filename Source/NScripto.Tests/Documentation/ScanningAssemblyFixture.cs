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
    public class ScanningAssemblyFixture : SpecBase
    {
        private ScriptEnvironmentScanner _scanner;
        private IEnumerable<Type> _result;

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
            _result.ShouldContain(typeof (SampleScriptEnvironment));
            _result.ShouldContain(typeof (NestedSampleScriptEnvironment));
        }
    }
}