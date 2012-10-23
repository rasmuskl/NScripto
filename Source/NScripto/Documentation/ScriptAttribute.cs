using System;

namespace NScripto.Documentation
{
    public class ScriptAttribute : Attribute
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public ScriptAttribute(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}