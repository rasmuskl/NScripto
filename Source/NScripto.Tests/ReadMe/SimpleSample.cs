using System;
using System.Linq;
using System.Collections.Generic;
using NScripto.CSharp;
using NUnit.Framework;
using Should;

namespace NScripto.Tests.ReadMe
{
    [TestFixture]
    public class SimpleSample
    {
        [Test]
        public void HelloWorld()
        {
            var scriptApi = new ScriptApi();
            var script = scriptApi.CompileScript<HelloWorldEnvironment>("DoIt()");

            var environment = new HelloWorldEnvironment();
            script.Run(environment);

            environment.Result.ShouldEqual("Hello World!");
        }

        public class HelloWorldEnvironment
        {
            public string Result { get; set; }

            public void DoIt()
            {
                Result = "Hello World!";
            }
        }

        [Test]
        public void ShowCode()
        {
            var scriptCompiler = new CSharpScriptCompiler();
            Console.WriteLine(scriptCompiler.ShowGeneratedCode("DoIt()", typeof(HelloWorldEnvironment)));
        }
    }
}