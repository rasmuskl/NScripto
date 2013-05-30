using System.Linq;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace NScripto.Documentation
{
    public class ScriptDocumentationExtractor
    {
        public ScriptDocumentation ExtractDocumentation(Type type)
        {
            return ExtractDocumentation(new[] { type });
        }

        public ScriptDocumentation ExtractDocumentation(IEnumerable<Type> types)
        {
            var documentation = new ScriptDocumentation();

            foreach (var type in types)
            {
                TryAddEnvironmentType(type, documentation);
                TryAddWrappedScript(type, documentation);
            }

            return documentation;
        }

        private void TryAddEnvironmentType(Type type, ScriptDocumentation documentation)
        {
            var environmentAttributes = type.GetCustomAttributes(typeof(ScriptEnvironmentAttribute), false).Cast<ScriptEnvironmentAttribute>();
            var environmentAttribute = environmentAttributes.FirstOrDefault();

            if (environmentAttribute == null)
                return;

            documentation.AddEnvironment(type, environmentAttribute.Name, environmentAttribute.Description);

            foreach (var methodInfo in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                var methodAttributes = methodInfo.GetCustomAttributes(typeof(ScriptMethodAttribute), false).Cast<ScriptMethodAttribute>();
                var methodAttribute = methodAttributes.FirstOrDefault();

                if (methodAttribute == null)
                    continue;

                var parameterAttributes = methodInfo.GetCustomAttributes(typeof(ScriptParameterAttribute), false).OfType<ScriptParameterAttribute>().ToArray();

                documentation.AddScriptMethod(environmentAttribute.Name, methodInfo.Name, methodAttribute.Description);

                foreach (var parameterInfo in methodInfo.GetParameters())
                {
                    var parameterAttribute = parameterAttributes.FirstOrDefault(x => x.Name == parameterInfo.Name);

                    if (parameterAttribute == null)
                        continue;

                    documentation.AddScriptParameter(environmentAttribute.Name, methodInfo.Name, parameterAttribute.Name, parameterAttribute.Description);
                }
            }
        }

        private void TryAddWrappedScript(Type scriptType, ScriptDocumentation documentation)
        {
            var scriptAttributes = scriptType.GetCustomAttributes(typeof(ScriptAttribute), false).Cast<ScriptAttribute>();
            var scriptAttribute = scriptAttributes.FirstOrDefault();

            if (scriptAttribute == null)
            {
                return;
            }

            ParameterInfo parameterInfo = scriptType.GetConstructors().First().GetParameters().First();
            Type[] genericArguments = parameterInfo.ParameterType.GetGenericArguments();

            var name = scriptAttribute.Name;
            var description = scriptAttribute.Description;

            var envDocs = ExtractDocumentation(genericArguments);
            var scriptDoc = new ScriptTypeDocumentation(scriptType, envDocs.Environments.ToArray(), name, description);

            documentation.AddWrappedScript(scriptDoc);

            foreach (var envDoc in envDocs.Environments)
            {
                documentation.AddEnvironment(envDoc.EnvironmentType, envDoc.Name, envDoc.Description);
            }
        }
    }
}