using System;
using System.Collections.Generic;
using NScripto.Tests.TestClasses.Invalid;
using NScripto.Verification;
using NScripto.Verification.Errors;
using NUnit.Framework;
using System.Linq;
using Should;

namespace NScripto.Tests.Verification
{
    [TestFixture]
    public class EnvironmentVerificationFixture
    {
        [Test]
        public void ReportsScriptEnvironmentsWithoutScriptMethodAttributes()
        {
            var verifier = new ScriptVerifier();

            var errors = verifier.Verify(new [] { typeof (EnvMissingScriptMethodAttribute) });

            var error = errors.ShouldContainExactlyOne<MissingScriptMethodAttributeVerificationError>();
            error.Type.ShouldEqual(typeof (EnvMissingScriptMethodAttribute));
            error.MethodInfo.Name.ShouldEqual("Run");
        }

        [Test]
        public void ReportsScriptEnvironmentsWithMissingScriptParameterAttributes()
        {
            var verifier = new ScriptVerifier();

            var errors = verifier.Verify(new [] { typeof (EnvMissingScriptParameterAttribute) });

            var error = errors.ShouldContainExactlyOne<MissingScriptParameterAttributeVerificationError>();
            error.Type.ShouldEqual(typeof(EnvMissingScriptParameterAttribute));
            error.MethodInfo.Name.ShouldEqual("Run");
            error.ParameterInfo.Name.ShouldEqual("x");
        }

        [Test]
        public void ReportsScriptEnvironmentsWithUnmatchedScriptParameterAttributes()
        {
            var verifier = new ScriptVerifier();

            var errors = verifier.Verify(new [] { typeof (EnvWrongScriptParameterAttributeName) });

            var error = errors.ShouldContainExactlyOne<UnmatchedScriptParameterAttributeVerificationError>();
            error.Type.ShouldEqual(typeof(EnvWrongScriptParameterAttributeName));
            error.MethodInfo.Name.ShouldEqual("Run");
            error.Attribute.Name.ShouldEqual("y");
        }
    }
}