using System.Linq;
using System.Collections.Generic;
using System;

namespace NScripto.Documentation.Model
{
    public class EnvironmentDocumentation
    {
        private readonly string _name;
        private readonly string _description;
        private readonly List<ScriptMethodDocumentation> _methods = new List<ScriptMethodDocumentation>();

        public EnvironmentDocumentation(Type environmentType, string name, string description)
        {
            _name = name;
            _description = description;
            EnvironmentType = environmentType;
        }

        public string Name
        {
            get { return _name; }
        }

        public string Description
        {
            get { return _description; }
        }

        public void AddMethod(string methodName, string description)
        {
            _methods.Add(new ScriptMethodDocumentation(methodName, description));
        }

        public IEnumerable<ScriptMethodDocumentation> Methods
        {
            get { return _methods.AsReadOnly(); }
        }

        public Type EnvironmentType { get; private set; }
    }
}