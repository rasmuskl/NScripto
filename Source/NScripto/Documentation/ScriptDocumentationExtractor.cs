using System.Linq;
using System.Collections.Generic;
using System;
using System.Reflection;
using NScripto.Documentation.Attributes;

namespace NScripto.Documentation
{
    public class ScriptDocumentationExtractor
    {
        public ScriptDocumentation Extract(Type type)
        {
            return Extract(new [] { type });
        }

        public ScriptDocumentation Extract(IEnumerable<Type> environmentTypes)
        {
            var documentation = new ScriptDocumentation();

            foreach (var type in environmentTypes)
                AddSingleType(type, documentation);

            return documentation;
        }

        private void AddSingleType(Type type, ScriptDocumentation documentation)
        {
            var environmentAttributes = type.GetCustomAttributes(typeof(ScriptEnvironmentAttribute), false).Cast<ScriptEnvironmentAttribute>();
            var environmentAttribute = environmentAttributes.FirstOrDefault();

            if (environmentAttribute == null)
                return;

            documentation.Add(type, environmentAttribute.Name, environmentAttribute.Description);

            foreach (var methodInfo in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                var methodAttributes = methodInfo.GetCustomAttributes(typeof(ScriptMethodAttribute), false).Cast<ScriptMethodAttribute>();
                var methodAttribute = methodAttributes.FirstOrDefault();

                if (methodAttribute == null)
                    continue;

                var parameterAttributes = methodInfo.GetCustomAttributes(typeof(ScriptParameterAttribute), false).OfType<ScriptParameterAttribute>();

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

        public ScriptDocumentation ExtractScriptDocumentation(Type scriptType)
        {
            ParameterInfo parameterInfo = scriptType.GetConstructors().First().GetParameters().First();
            Type[] genericArguments = parameterInfo.ParameterType.GetGenericArguments();

            var scriptAttributes = scriptType.GetCustomAttributes(typeof(ScriptAttribute), false).Cast<ScriptAttribute>();
            var scriptAttribute = scriptAttributes.FirstOrDefault();

            var name = scriptAttribute.Name;
            var description = scriptAttribute.Description;

            var envDocs = Extract(genericArguments);
            var scriptDoc = new ScriptTypeDocumentation(scriptType, envDocs.Environments.ToArray(), name, description);

            return new ScriptDocumentation(scriptDoc);
        }
    }
}