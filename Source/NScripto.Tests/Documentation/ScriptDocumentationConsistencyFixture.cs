using System.Linq;
using System.Collections.Generic;
using System;
using NUnit.Framework;

namespace NScripto.Tests.Documentation
{
    [TestFixture]
    public class ScriptDocumentationConsistencyFixture
    {
        //[Test]
        //public void all_methods_in_script_environments_should_have_a_script_or_noscript_attribute()
        //{
        //    var assembly = Assembly.GetAssembly(typeof (GeneralScriptEnvironment));
        //    var scanner = new ScriptEnvironmentScanner();

        //    foreach (var type in scanner.Scan(assembly).EnvironmentTypes)
        //    {
        //        foreach (var methodInfo in type.GetMethods(BindingFlags.Public | BindingFlags.Instance))
        //        {
        //            if(methodInfo.DeclaringType == typeof(object) || methodInfo.GetCustomAttributes(typeof(CompilerGeneratedAttribute), true).Any())
        //                continue;

        //            var attributes = methodInfo.GetCustomAttributes(false);

        //            Assert.That(attributes.Any(x => x.GetType() == typeof(ScriptMethodAttribute) 
        //                || x.GetType() == typeof(NoScriptMethodAttribute)), type.Name + "." + methodInfo.Name + " did not have appropriate attributes.");
        //        }
        //    }
        //}

        //[Test]
        //public void documentation_extraction_should_not_fail_on_script_assembly()
        //{
        //    var scanner = new ScriptEnvironmentScanner();
        //    var extractor = new ScriptDocumentationExtractor();

        //    foreach (var environmentType in scanner.Scan(typeof(GeneralScriptEnvironment).Assembly))
        //    {
        //        extractor.Extract(environmentType);
        //    }
        //}

        //[Test]
        //public void all_methods_in_script_environments_with_a_script_attribute_should_have_parameter_attributes()
        //{
        //    var assembly = Assembly.GetAssembly(typeof(GeneralScriptEnvironment));
        //    var scanner = new ScriptEnvironmentScanner();

        //    foreach (var type in scanner.Scan(assembly))
        //    {
        //        foreach (var methodInfo in type.GetMethods(BindingFlags.Public | BindingFlags.Instance))
        //        {
        //            if (methodInfo.DeclaringType == typeof(object) || methodInfo.GetCustomAttributes(typeof(CompilerGeneratedAttribute), true).Any())
        //                continue;

        //            var attributes = methodInfo.GetCustomAttributes(false);

        //            if(attributes.All(x => x.GetType() != typeof(ScriptMethodAttribute)))
        //                continue;

        //            foreach (var parameterInfo in methodInfo.GetParameters())
        //            {
        //                Assert.That(attributes.Any(x => x.GetType() == typeof(ScriptParameterAttribute)
        //                    && ((ScriptParameterAttribute)x).Name == parameterInfo.Name),
        //                    type.Name+"."+methodInfo.Name+" parameter '"+parameterInfo.Name+"' has no description.");
        //            }

        //            Assert.That(attributes.Count(x => x.GetType() == typeof(ScriptParameterAttribute)), Is.EqualTo(methodInfo.GetParameters().Count()),
        //                type.Name+"."+methodInfo.Name+ " had too many parameter attributes.");
        //        }
        //    }
        //}
    }
}