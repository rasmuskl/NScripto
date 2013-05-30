using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using Should;

namespace NScripto.Tests.ReadMe
{
    [TestFixture]
    public class WrappedSample
    {
        [Test]
        public void Wrapped()
        {
            var scriptApi = new ScriptApi();
            var wrappedScript = scriptApi.CompileWrappedScript<HappyScript>("Mood(\"Happy!\")");

            string result = wrappedScript.Run();

            result.ShouldEqual("Happy!");
        }

        public class HappyScript
        {
            private readonly IScript<HappyEnvironment> _script;

            public HappyScript(IScript<HappyEnvironment> script)
            {
                _script = script;
            }

            public string Run()
            {
                var happyEnvironment = new HappyEnvironment("Moody");
                _script.Run(happyEnvironment);
                return happyEnvironment.State;
            }
        }

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