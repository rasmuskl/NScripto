using System.Linq;
using System.Collections.Generic;
using System;
using NUnit.Framework;

namespace NScripto.Tests.Documentation
{
    public class when_extracting_documentation_from_a_class_not_marked_as_an_environment : behaves_as_a_documentation_extraction
    {
        protected override Type GetExtractedType()
        {
            return typeof(TestNotMarkedAsEnvironment);
        }

        [Test]
        public void nothing_should_be_added_to_the_documentation()
        {
            Assert.That(_documentation.Environments.Count(), Is.EqualTo(0));
        }
    }

    public class TestNotMarkedAsEnvironment
    {
    }
}