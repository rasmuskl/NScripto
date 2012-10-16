using System;
using NScripto.CSharp;
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
            var script = _scriptFactory.CompileScript<TestArity1>("throw new Exception()");

            Assert.Throws<Exception>(script.Run);
        }        
        
        [Test]
        public void SupportsGenericArity2()
        {
            var script = _scriptFactory.CompileScript<TestArity2>("throw new Exception()");

            Assert.Throws<Exception>(script.Run);
        }        
        
        [Test]
        public void SupportsGenericArity3()
        {
            var script = _scriptFactory.CompileScript<TestArity3>("throw new Exception()");

            Assert.Throws<Exception>(script.Run);
        }

        [Test]
        public void SupportsGenericArity4()
        {
            var script = _scriptFactory.CompileScript<TestArity4>("throw new Exception()");

            Assert.Throws<Exception>(script.Run);
        }
    }

    public class TestArity1
    {
        private readonly IScript<object> _script;

        public TestArity1(IScript<object> script)
        {
            _script = script;
        }

        public void Run()
        {
            _script.Run(new object());
        }
    }

    public class TestArity2
    {
        private readonly IScript<object, EventArgs> _script;

        public TestArity2(IScript<object, EventArgs> script)
        {
            _script = script;
        }

        public void Run()
        {
            _script.Run(new object(), new EventArgs());
        }
    }

    public class TestArity3
    {
        private readonly IScript<object, EventArgs, IndexOutOfRangeException> _script;

        public TestArity3(IScript<object, EventArgs, IndexOutOfRangeException> script)
        {
            _script = script;
        }

        public void Run()
        {
            _script.Run(new object(), new EventArgs(), new IndexOutOfRangeException());
        }
    }    
    
    public class TestArity4
    {
        private readonly IScript<object, EventArgs, IndexOutOfRangeException, Random> _script;

        public TestArity4(IScript<object, EventArgs, IndexOutOfRangeException, Random> script)
        {
            _script = script;
        }

        public void Run()
        {
            _script.Run(new object(), new EventArgs(), new IndexOutOfRangeException(), new Random());
        }
    }
}