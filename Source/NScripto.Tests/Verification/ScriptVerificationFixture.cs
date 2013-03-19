using System;
using NScripto.Tests.TestClasses.Invalid;
using NScripto.Verification;
using NScripto.Verification.Errors;
using NUnit.Framework;
using Should;

namespace NScripto.Tests.Verification
{
    [TestFixture]
    public class ScriptVerificationFixture
    {
        [Test]
        public void ReportsScriptsWithNonScriptEnvironmentGenericArgumentsToTheirContructors()
        {
            var verifier = new ScriptVerifier();

            var errors = verifier.Verify(new[] {typeof (ScriptWithNonEnvDependency)});

            var error = errors.ShouldContainExactlyOne<NonScriptEnvironmentGenericTypeVerificationError>();
            error.Type.ShouldEqual(typeof(ScriptWithNonEnvDependency));
            error.NonScriptEnvironmentType.ShouldEqual(typeof(bool));
        }
    }
}