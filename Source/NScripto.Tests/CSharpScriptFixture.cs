using System.Linq;
using System.Collections.Generic;
using System;
using NScripto.CSharp;
using NUnit.Framework;
using Should;

namespace NScripto.Tests
{
    [TestFixture]
    public class CSharpScriptFixture
    {
        [Test]
        public void CanCompileASimpleScript()
        {
            var compiler = new CSharpScriptCompiler();

            var script = compiler.CompileScript("throw new Exception(\"Test\")");

            Assert.Throws<Exception>(() => script.Run(), "Test");
        }

        [Test]
        public void CanDelegatedAClassWithAMethod()
        {
            // Arrange
            var compiler = new CSharpScriptCompiler();
            var script = compiler.CompileScript(string.Empty, typeof(TestScriptMethodEnvironment));

            // Act
            script.Run(new TestScriptMethodEnvironment());
        }

        [Test]
        public void CanDelegatedAClassWithAProperty()
        {
            // Arrange
            var compiler = new CSharpScriptCompiler();
            var script = compiler.CompileScript(string.Empty, typeof(TestScriptPropertyEnvironment));

            // Act
            script.Run(new TestScriptPropertyEnvironment());
        }

        [Test]
        public void CanCallAMethodInADelegatedClass()
        {
            // Arrange
            var compiler = new CSharpScriptCompiler();
            var script = compiler.CompileScript("AbortIf(true)", typeof(TestScriptPropertyEnvironment));

            // Act
            var environment = new TestScriptPropertyEnvironment();
            script.Run(environment);

            // Assert
            environment.Abort.ShouldBeTrue();
        }

        [Test]
        public void CanCompileTypedScript()
        {
            var compiler = new CSharpScriptCompiler();
            var factory = new ScriptApi(compiler);

            var script = factory.CompileScript<TestScript>("throw new Exception()");

            Assert.Throws<Exception>(script.Run);
        }

        public class TestScriptMethodEnvironment
        {
            public void AbortIf(bool condition)
            {
            }
        }

        public class TestScriptPropertyEnvironment
        {
            public bool Abort { get; private set; }

            public void AbortIf(bool condition)
            {
                if (condition)
                {
                    Abort = true;
                }
            }
        }

        public class TestScript
        {
            private readonly IScript<TestScriptMethodEnvironment> _script;

            public TestScript(IScript<TestScriptMethodEnvironment> script)
            {
                _script = script;
            }

            public void Run()
            {
                _script.Run(new TestScriptMethodEnvironment());
            }
        }
    }
}