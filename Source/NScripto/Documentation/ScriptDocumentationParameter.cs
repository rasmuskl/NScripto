using System.Linq;
using System.Collections.Generic;
using System;

namespace NScripto.Documentation
{
    public class ScriptDocumentationParameter
    {
        private readonly string _name;
        private readonly string _description;

        public ScriptDocumentationParameter(string name, string description)
        {
            _name = name;
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

        public override string ToString()
        {
            return Name;
        }
    }
}