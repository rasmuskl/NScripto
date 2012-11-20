using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.CSharp;
using NScripto.CSharp;
using NScripto.CodeDom;
using NScripto.Documentation;
using NScripto.Documentation.Attributes;
using NUnit.Framework;
using Should;

namespace NScripto.Tests
{
    [TestFixture]
    public class MethodDelegaterFixture
    {
        [Test]
        public void CanDelegateASimpleMethod()
        {
            var delegater = new MethodDelegater();

            var result = delegater.BuildDelegateMethod(typeof(TestDelegaterClass).GetMethod("Method"), "testClass");

            CompileToString(result).ShouldEqual(@"public void Method()
{
    testClass.Method();
}");
        }

        [Test]
        public void CanDelegateAMethodWithSimpleArgument()
        {
            var delegater = new MethodDelegater();

            var result = delegater.BuildDelegateMethod(typeof(TestDelegaterClass).GetMethod("MethodWithArgument"), "testClass");

            CompileToString(result).ShouldEqual(@"public void MethodWithArgument(string test)
{
    testClass.MethodWithArgument(test);
}");
        }

        [Test]
        public void CanDelegateAMethodWithReturnType()
        {
            var delegater = new MethodDelegater();

            var result = delegater.BuildDelegateMethod(typeof(TestDelegaterClass).GetMethod("MethodWithReturn"), "testClass");

            CompileToString(result).ShouldEqual(@"public string MethodWithReturn()
{
    return testClass.MethodWithReturn();
}");
        }

        [Test]
        public void CanDelegateAMethodWithOptionalParameter()
        {
            var delegater = new MethodDelegater();

            var result = delegater.BuildDelegateMethod(typeof(TestDelegaterClass).GetMethod("MethodWithOptional"), "testClass");

            CompileToString(result).ShouldEqual(@"public void MethodWithOptional([System.Runtime.InteropServices.OptionalAttribute()] [System.Runtime.InteropServices.DefaultParameterValueAttribute(42)] int num)
{
    testClass.MethodWithOptional(num);
}");
        }        
        
        [Test]
        public void CanDelegateAMethodWithOptionalStringParameter()
        {
            var delegater = new MethodDelegater();

            var result = delegater.BuildDelegateMethod(typeof(TestDelegaterClass).GetMethod("MethodWithOptionalString"), "testClass");

            CompileToString(result).ShouldEqual(@"public void MethodWithOptionalString([System.Runtime.InteropServices.OptionalAttribute()] [System.Runtime.InteropServices.DefaultParameterValueAttribute(""Test"")] string str)
{
    testClass.MethodWithOptionalString(str);
}");
        }

        [Test]
        public void CanDelegateAMethodWithParams()
        {
            var delegater = new MethodDelegater();

            var result = delegater.BuildDelegateMethod(typeof(TestDelegaterClass).GetMethod("MethodWithParams"), "testClass");

            CompileToString(result).ShouldEqual(@"public void MethodWithParams(params object[] objs)
{
    testClass.MethodWithParams(objs);
}");
        }

        [Test]
        public void WillNotDelegateANoScriptMethodMethod()
        {
            var delegater = new MethodDelegater();

            var result = delegater.BuildDelegateMethod(typeof(TestDelegaterClass).GetMethod("NoScriptMethod"), "testClass");

            result.ShouldBeNull();
        }

        private class TestDelegaterClass
        {
            public void Method()
            {

            }

            public void MethodWithArgument(string test)
            {
                Console.Out.WriteLine(test);
            }

            public string MethodWithReturn()
            {
                return string.Empty;
            }

            [NoScriptMethod]
            public string NoScriptMethod()
            {
                return string.Empty;
            }

            public void MethodWithOptional(int num = 42)
            {
                
            }            
            
            public void MethodWithOptionalString(string str = "Test")
            {
                
            }            
            
            public void MethodWithParams(params object[] objs)
            {
                
            }
        }

        private string CompileToString(CodeMemberMethod method)
        {
            var codeProvider = new CSharpCodeProvider();

            using (var stringWriter = new StringWriter())
            {
                using (var indentedTextWriter = new IndentedTextWriter(stringWriter))
                {
                    var options = new CodeGeneratorOptions();

                    options.BracingStyle = "C";
                    codeProvider.GenerateCodeFromMember(method, indentedTextWriter, options);
                }

                var builder = stringWriter.GetStringBuilder();
                return SanitizeCodeForComparison(builder.ToString());
            }

        }

        private string SanitizeCodeForComparison(string str)
        {
            return str.Trim('\n', '\r');
        }
    }
}