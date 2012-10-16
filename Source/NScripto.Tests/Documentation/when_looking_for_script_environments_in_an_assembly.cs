using System.Linq;
using System.Collections.Generic;
using System;
using System.Reflection;
using NScripto.Documentation;
using NScripto.Tests.Documentation.Samples;
using NScripto.Tests.Documentation.Samples.Nested;
using NUnit.Framework;

namespace NScripto.Tests.Documentation
{
    public class when_looking_for_script_environments_in_an_assembly : SpecBase
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
        public void it_should_find_the_same_as_a_specialized_namespace_search()
        {
            Assert.That(_result.EnvironmentTypes.Contains(typeof(SampleScriptEnvironment)));
            Assert.That(_result.EnvironmentTypes.Contains(typeof(NestedSampleScriptEnvironment)));
        }
    }
}