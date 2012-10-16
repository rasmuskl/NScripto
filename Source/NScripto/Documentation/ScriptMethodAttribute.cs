using System.Linq;
using System.Collections.Generic;
using System;

namespace NScripto.Documentation
{
    public class ScriptMethodAttribute : Attribute
    {
        private readonly string _description;

        public ScriptMethodAttribute(string description)
        {
            _description = description;
        }

        public string Description
        {
            get { return _description; }
        }
    }
}