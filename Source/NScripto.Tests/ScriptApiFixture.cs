using System;
using NScripto.CSharp;
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
            _scriptApi = new ScriptApi(new CSharpScriptCompiler());
        }
                
        [Test]
        public void SupportsGenericArity1()
        {
            var script = _scriptApi.CompileScript<TestScriptArity1>("throw new Exception()");

            Assert.Throws<Exception>(script.Run);
        }        
        
        [Test]
        public void SupportsGenericArity2()
        {
            var script = _scriptApi.CompileScript<TestScriptArity2>("throw new Exception()");

            Assert.Throws<Exception>(script.Run);
        }        
        
        [Test]
        public void SupportsGenericArity3()
        {
            var script = _scriptApi.CompileScript<TestScriptArity3>("throw new Exception()");

            Assert.Throws<Exception>(script.Run);
        }

        [Test]
        public void SupportsGenericArity4()
        {
            var script = _scriptApi.CompileScript<TestScriptArity4>("throw new Exception()");

            Assert.Throws<Exception>(script.Run);
        }

        [Test]
        public void InvalidConstructor_MultipleScriptConstructors_ThrowsException()
        {
            var exception = Assert.Throws<MultipleScriptConstructorsException>(() => _scriptApi.CompileScript<ScriptWithMultipleScriptConstructors>(string.Empty));
            exception.ScriptType.ShouldEqual(typeof(ScriptWithMultipleScriptConstructors));
        }

        [Test]
        public void InvalidConstructor_NoScriptConstructors_ThrowsException()
        {
            var exception = Assert.Throws<NoScriptConstructorsException>(() => _scriptApi.CompileScript<ScriptWithNoScriptConstructors>(string.Empty));
            exception.ScriptType.ShouldEqual(typeof(ScriptWithNoScriptConstructors));
        }
    }
}