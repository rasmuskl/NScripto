using System.Linq;
using System.Collections.Generic;
using System;

namespace NScripto.Documentation
{
    public class ScriptTypeDocumentation
    {
        public ScriptTypeDocumentation(Type scriptType, EnvironmentDocumentation[] scriptEnvironments, string name, string description)
        {
            ScriptType = scriptType;
            Name = name;
            Description = description;
            Environments = scriptEnvironments ?? new EnvironmentDocumentation[0];
        }

        public Type ScriptType { get; private set; }
        public EnvironmentDocumentation[] Environments { get; private set; }

        public string Name { get; private set; }
        public string Description { get; private set; }
    }
}