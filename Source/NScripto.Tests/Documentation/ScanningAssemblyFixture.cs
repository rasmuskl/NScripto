using System.Linq;
using System.Collections.Generic;
using System;
using System.Reflection;
using NScripto.Documentation;
using NScripto.Tests.TestClasses;
using NScripto.Tests.TestClasses.Nested;
using NUnit.Framework;
using Should;

namespace NScripto.Tests.Documentation
{
    public class ScanningAssemblyFixture : SpecBase
    {
        private ScriptApi _scriptApi;
        private ScriptDocumentation _result;

        protected override void Arrange()
        {
            _scriptApi = new ScriptApi();
        }

        protected override void Act()
        {
            _result = _scriptApi.ExtractDocumentationFromAssembly(Assembly.GetExecutingAssembly());
        }

        [Test]
        public void ItShouldFindTheSameAsASpecializedNamespaceSearch()
        {
            _result.Environments.Count(x => x.EnvironmentType == typeof (SampleScriptEnvironment)).ShouldEqual(1);
            _result.Environments.Count(x => x.EnvironmentType == typeof(NestedSampleScriptEnvironment)).ShouldEqual(1);
        }
    }
}