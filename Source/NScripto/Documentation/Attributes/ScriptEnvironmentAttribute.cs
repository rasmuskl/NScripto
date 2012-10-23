using System.Linq;
using System.Collections.Generic;
using System;

namespace NScripto.Documentation.Attributes
{
    public class ScriptEnvironmentAttribute : Attribute
    {
        private readonly string _name;
        private readonly string _description;

        public ScriptEnvironmentAttribute(string name, string description)
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
    }
}