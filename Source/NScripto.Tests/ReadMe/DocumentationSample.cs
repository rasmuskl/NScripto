using System;
using System.Linq;
using System.Collections.Generic;
using NScripto.Documentation;
using NUnit.Framework;

namespace NScripto.Tests.ReadMe
{
    [TestFixture]
    public class DocumentationSample
    {
        [Test]
        public void VerifyScripts()
        {
            var scriptApi = new ScriptApi();

            var documentation = scriptApi.ExtractDocumentationFromTypes(new[] { typeof(HappyEnvironment) });
        }

        [ScriptEnvironment("Happy env!", "Happy dappy.")]
        public class HappyEnvironment
        {
            public string State { get; set; }

            public HappyEnvironment(string initialState)
            {
                State = initialState;
            }

            [ScriptMethod("Sets the overall mood.")]
            [ScriptParameter("mood", "How you doing?")]
            public void Mood(string mood)
            {
                State = mood;
            }
        }
    }
}