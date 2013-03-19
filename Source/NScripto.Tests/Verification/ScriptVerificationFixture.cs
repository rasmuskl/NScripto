using System;
using System.Collections.Generic;
using NScripto.Tests.TestClasses.Invalid;
using NScripto.Verification;
using NUnit.Framework;
using System.Linq;
using Should;

namespace NScripto.Tests.Verification
{
    [TestFixture]
    public class ScriptVerificationFixture
    {
        [Test]
        public void ReportsScriptEnvironmentsWithoutScriptMethodAttributes()
        {
            var rule = new ScriptVerifier();

            var errors = rule.Verify(new [] { typeof (ScriptEnvironmentWithMissingScriptMethodAttribute) });

            var error = errors.ShouldContainExactlyOne<MissingScriptMethodAttributeVerificationError>();
            error.Type.ShouldEqual(typeof (ScriptEnvironmentWithMissingScriptMethodAttribute));
        }

        [Test]
        public void ReportsScriptEnvironmentsWithMissingScriptParameterAttributes()
        {
            var rule = new ScriptVerifier();

            var errors = rule.Verify(new [] { typeof (ScriptEnvironmentWithMissingScriptParameterAttribute) });

            var error = errors.ShouldContainExactlyOne<MissingScriptParameterAttributeVerificationError>();
            error.Type.ShouldEqual(typeof(ScriptEnvironmentWithMissingScriptParameterAttribute));
        }
    }
}