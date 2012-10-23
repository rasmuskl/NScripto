using System.Linq;
using System.Collections.Generic;
using System;

namespace NScripto.Documentation
{
    public class ScriptDocumentation
    {
        private readonly List<EnvironmentDocumentation> _environments = new List<EnvironmentDocumentation>();
        private readonly List<ScriptTypeDocumentation> _scripts = new List<ScriptTypeDocumentation>();

        public ScriptDocumentation(ScriptTypeDocumentation scriptTypeDocumentation)
        {
            _scripts.Add(scriptTypeDocumentation);
        }

        public ScriptDocumentation()
        {
        }

        public IEnumerable<EnvironmentDocumentation> Environments
        {
            get { return _environments.AsReadOnly(); }
        }

        public IEnumerable<ScriptTypeDocumentation> Scripts
        {
            get { return _scripts.AsReadOnly(); }
        }

        public void Add(Type environmentType, string name, string description)
        {
            _environments.Add(new EnvironmentDocumentation(environmentType, name, description));
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