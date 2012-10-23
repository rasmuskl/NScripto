using System.Linq;
using System.Collections.Generic;
using System;

namespace NScripto.Documentation.Model
{
    public class ScriptMethodDocumentation
    {
        private readonly string _name;
        private readonly string _description;
        private readonly List<ScriptDocumentationParameter> _parameters = new List<ScriptDocumentationParameter>();

        public ScriptMethodDocumentation(string methodName, string description)
        {
            _name = methodName;
            _description = description;
        }

        public string Name
        {
            get { return _name; }
        }

        public string Description
        {
            get { return _description; }
        }

        public void AddParameter(string parameterName, string description)
        {
            _parameters.Add(new ScriptDocumentationParameter(parameterName, description));
        }

        public IEnumerable<ScriptDocumentationParameter> Parameters
        {
            get { return _parameters.AsReadOnly(); }
        }

        public override string ToString()
        {
            return Name + "("+ String.Join(", ", _parameters.Select(x => x.Name).ToArray()) +")";
        }

    }
}