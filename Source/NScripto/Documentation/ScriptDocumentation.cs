using System.Linq;
using System.Collections.Generic;
using System;

namespace NScripto.Documentation
{
    public class ScriptDocumentation
    {
        private readonly List<EnvironmentDocumentation> _environments = new List<EnvironmentDocumentation>();

        public IEnumerable<EnvironmentDocumentation> Environments
        {
            get { return _environments.AsReadOnly(); }
        }

        public void Add(string name, string description)
        {
            _environments.Add(new EnvironmentDocumentation(name, description));
        }

        public void AddScriptMethod(string environmentName, string methodName, string description)
        {
            var environmentDocumentation = _environments.First(x => x.Name == environmentName);
            environmentDocumentation.AddMethod(methodName, description);
        }

        public void AddScriptParameter(string environmentName, string methodName, string parameterName, string parameterDescription)
        {
            var environmentDocumentation = _environments.First(x => x.Name == environmentName);
            var methodDocumentation = environmentDocumentation.Methods.First(x => x.Name == methodName);
            methodDocumentation.AddParameter(parameterName, parameterDescription);
        }
    }
}