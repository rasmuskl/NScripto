using System;
using System.Linq;
using System.Collections.Generic;
using NScripto.CSharp;
using NUnit.Framework;
using Should;

namespace NScripto.Tests.ReadMe
{
    [TestFixture]
    public class ComposedSample
    {
        [Test]
        public void MultipleEnvironments()
        {
            var scriptApi = new ScriptApi();
            var script = scriptApi.CompileScript<HelloWorldEnvironment, GeneralPurposeEnvironment>("DoIt(GetRandom(42))");

            var helloEnvironment = new HelloWorldEnvironment();
            var generalEnvironment = new GeneralPurposeEnvironment();
        
            script.Run(helloEnvironment, generalEnvironment);

            helloEnvironment.Result.ShouldEqual("Hello 28!");
        }

        public class HelloWorldEnvironment
        {
            public string Result { get; set; }

            public void DoIt(int num)
            {
                Result = "Hello " + num + "!";
            }
        }

        public class GeneralPurposeEnvironment
        {
            public static Random Random = new Random(42);

            public int GetRandom(int max)
            {
                return Random.Next(max);
            }
        }

        [Test]
        public void ShowCode()
        {
            var scriptCompiler = new CSharpScriptCompiler();
            Console.WriteLine(scriptCompiler.ShowGeneratedCode("DoIt(GetRandom(42))", typeof(HelloWorldEnvironment), typeof(GeneralPurposeEnvironment)));
        }
    }
}