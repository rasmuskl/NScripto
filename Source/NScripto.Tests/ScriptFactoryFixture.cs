using System;
using NScripto.CSharp;
using NScripto.Tests.TestClasses;
using NUnit.Framework;

namespace NScripto.Tests
{
    [TestFixture]
    public class ScriptFactoryFixture
    {
        private ScriptFactory _scriptFactory;

        [SetUp]
        public void BeforeEachTest()
        {
            _scriptFactory = new ScriptFactory(new CSharpScriptCompiler());
        }

        [Test]
        public void SupportsGenericArity1()
        {
            var script = _scriptFactory.CompileScript<TestScriptArity1>("throw new Exception()");

            Assert.Throws<Exception>(script.Run);
        }        
        
        [Test]
        public void SupportsGenericArity2()
        {
            var script = _scriptFactory.CompileScript<TestScriptArity2>("throw new Exception()");

            Assert.Throws<Exception>(script.Run);
        }        
        
        [Test]
        public void SupportsGenericArity3()
        {
            var script = _scriptFactory.CompileScript<TestScriptArity3>("throw new Exception()");

            Assert.Throws<Exception>(script.Run);
        }

        [Test]
        public void SupportsGenericArity4()
        {
            var script = _scriptFactory.CompileScript<TestScriptArity4>("throw new Exception()");

            Assert.Throws<Exception>(script.Run);
        }
    }
}