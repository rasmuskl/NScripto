using System;
using System.Linq;
using System.Collections.Generic;
using NScripto.Documentation;
using NUnit.Framework;

namespace NScripto.Tests.ReadMe
{
    [TestFixture]
    public class VerificationSample
    {
        [Test]
        [Ignore("The type is not valid. Only for demonstration.")]
        public void VerifyScripts()
        {
            var scriptApi = new ScriptApi();

            scriptApi.VerifyTypes(new [] { typeof(HappyEnvironment) });
        }

        [ScriptEnvironment("Happy env!", "Happy dappy.")]
        public class HappyEnvironment
        {
            public string State { get; set; }

            public HappyEnvironment(string initialState)
            {
                State = initialState;
            }

            public void Mood(string mood)
            {
                State = mood;
            }
        }
    }
}