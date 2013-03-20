using System.Linq;
using System.Collections.Generic;
using System;

namespace NScripto.Documentation
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ScriptParameterAttribute : Attribute
    {
        private readonly string _name;
        private readonly string _description;

        public ScriptParameterAttribute(string name, string description)
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