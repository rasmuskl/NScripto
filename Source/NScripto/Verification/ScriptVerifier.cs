using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using NScripto.Documentation.Attributes;
using System.Linq;
using NScripto.Verification.Errors;

namespace NScripto.Verification
{
    public class ScriptVerifier
    {
        public IVerificationError[] Verify(Type[] types)
        {
            var errors = new List<IVerificationError>();

            foreach (var type in types)
            {
                if (type.GetCustomAttributes(typeof (ScriptEnvironmentAttribute), true).Any())
                {
                    var methodInfos = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);

                    foreach (var methodInfo in methodInfos.Where(IsNotObjectMethodOrCompilerGenerated))
                    {
                        if (!methodInfo.GetCustomAttributes(typeof (ScriptMethodAttribute), true).Any())
                        {
                            errors.Add(new MissingScriptMethodAttributeVerificationError(type));
                        }

                        var parameterInfos = methodInfo.GetParameters();
                        var scriptParameterAttributes = methodInfo.GetCustomAttributes(typeof (ScriptParameterAttribute), true)
                            .OfType<ScriptParameterAttribute>()
                            .ToArray();

                        foreach (var parameterInfo in parameterInfos)
                        {
                            if (!scriptParameterAttributes.Any(x => x.Name == parameterInfo.Name))
                            {
                                errors.Add(new MissingScriptParameterAttributeVerificationError(type));
                            }
                        }
                    }

                }
            }


            return errors.ToArray();
        }

        private static bool IsNotObjectMethodOrCompilerGenerated(MethodInfo methodInfo)
        {
            return methodInfo.DeclaringType != typeof (object) && !methodInfo.GetCustomAttributes(typeof (CompilerGeneratedAttribute), true).Any();
        }
    }
}