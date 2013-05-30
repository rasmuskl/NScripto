using System;
using NScripto.Exceptions;
using NScripto.Tests.TestClasses;
using NScripto.Tests.TestClasses.Invalid;
using NUnit.Framework;
using Should;

namespace NScripto.Tests
{
    [TestFixture]
    public class ScriptApiFixture
    {
        private ScriptApi _scriptApi;

        [SetUp]
        public void BeforeEachTest()
        {
            _scriptApi = new ScriptApi();
        }
                
        [Test]
        public void SupportsGenericArity1()
        {
            var script = _scriptApi.CompileWrappedScript<TestScriptArity1>("throw new Exception()");

            Assert.Throws<Exception>(script.Run);
        }        
        
        [Test]
        public void SupportsGenericArity2()
        {
            var script = _scriptApi.CompileWrappedScript<TestScriptArity2>("throw new Exception()");

            Assert.Throws<Exception>(script.Run);
        }        
        
        [Test]
        public void SupportsGenericArity3()
        {
            var script = _scriptApi.CompileWrappedScript<TestScriptArity3>("throw new Exception()");

            Assert.Throws<Exception>(script.Run);
        }

        [Test]
        public void SupportsGenericArity4()
        {
            var script = _scriptApi.CompileWrappedScript<TestScriptArity4>("throw new Exception()");

            Assert.Throws<Exception>(script.Run);
        }

        [Test]
        public void InvalidConstructor_MultipleScriptConstructors_ThrowsException()
        {
            var exception = Assert.Throws<MultipleScriptConstructorsException>(() => _scriptApi.CompileWrappedScript<ScriptWithMultipleScriptConstructors>(string.Empty));
            exception.ScriptType.ShouldEqual(typeof(ScriptWithMultipleScriptConstructors));
        }

        [Test]
        public void InvalidConstructor_NoScriptConstructors_ThrowsException()
        {
            var exception = Assert.Throws<NoScriptConstructorsException>(() => _scriptApi.CompileWrappedScript<ScriptWithNoScriptConstructors>(string.Empty));
            exception.ScriptType.ShouldEqual(typeof(ScriptWithNoScriptConstructors));
        }
    }
}