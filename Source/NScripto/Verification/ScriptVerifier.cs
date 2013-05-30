using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Linq;
using NScripto.Documentation;
using NScripto.Verification.Errors;

namespace NScripto.Verification
{
    public class ScriptVerifier
    {
        public IVerificationError[] AnalyzeTypes(Type[] types)
        {
            var errors = new List<IVerificationError>();

            foreach (var type in types)
            {
                if (type.GetCustomAttributes(typeof(ScriptEnvironmentAttribute), true).Any())
                {
                    var methodInfos = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);

                    foreach (var methodInfo in methodInfos.Where(IsNotObjectMethodOrCompilerGenerated))
                    {
                        if (methodInfo.GetCustomAttributes(typeof (NoScriptMethodAttribute), true).Any())
                        {
                            continue;
                        }

                        if (!methodInfo.GetCustomAttributes(typeof(ScriptMethodAttribute), true).Any())
                        {
                            errors.Add(new MissingScriptMethodAttributeVerificationError(type, methodInfo));
                        }

                        var parameterInfos = methodInfo.GetParameters();
                        var scriptParameterAttributes = methodInfo.GetCustomAttributes(typeof(ScriptParameterAttribute), true)
                            .OfType<ScriptParameterAttribute>()
                            .ToArray();

                        foreach (var parameterInfo in parameterInfos)
                        {
                            if (scriptParameterAttributes.All(x => x.Name != parameterInfo.Name))
                            {
                                errors.Add(new MissingScriptParameterAttributeVerificationError(type, methodInfo, parameterInfo));
                            }
                        }

                        foreach (var scriptParameterAttribute in scriptParameterAttributes)
                        {
                            if (parameterInfos.All(x => x.Name != scriptParameterAttribute.Name))
                            {
                                errors.Add(new UnmatchedScriptParameterAttributeVerificationError(type, methodInfo, scriptParameterAttribute));
                            }
                        }
                    }
                }

                var constructorInfos = type.GetConstructors();

                foreach (var constructorInfo in constructorInfos)
                {
                    foreach (var parameterInfo in constructorInfo.GetParameters())
                    {
                        var parameterType = parameterInfo.ParameterType;

                        if (parameterType.IsGenericType && typeof(IScript).IsAssignableFrom(parameterType.GetGenericTypeDefinition()))
                        {
                            foreach (var genericEnvironmentType in parameterType.GetGenericArguments())
                            {
                                if (!genericEnvironmentType.GetCustomAttributes(typeof(ScriptEnvironmentAttribute), true).Any())
                                {
                                    errors.Add(new NonScriptEnvironmentGenericTypeVerificationError(type, genericEnvironmentType));
                                }
                            }
                        }
                    }
                }
            }

            return errors.ToArray();
        }

        private static bool IsNotObjectMethodOrCompilerGenerated(MethodInfo methodInfo)
        {
            return methodInfo.DeclaringType != typeof(object) && !methodInfo.GetCustomAttributes(typeof(CompilerGeneratedAttribute), true).Any();
        }
    }
}