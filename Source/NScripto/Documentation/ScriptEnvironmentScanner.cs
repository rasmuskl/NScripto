using System.Linq;
using System.Collections.Generic;
using System;
using System.Reflection;
using NScripto.Documentation.Attributes;
using NScripto.Documentation.Model;

namespace NScripto.Documentation
{
    public class ScriptEnvironmentScanner
    {
        public ScriptEnvironmentTypeResult Scan(Assembly assembly)
        {
            return Scan(assembly, string.Empty);
        }

        public ScriptEnvironmentTypeResult Scan(Assembly assembly, string targetNamespace)
        {
            var q = from type in assembly.GetTypes()
                    where !String.IsNullOrEmpty(type.Namespace) 
                    && type.Namespace.StartsWith(targetNamespace)
                    && type.GetCustomAttributes(typeof(ScriptEnvironmentAttribute), false).Any()
                    select type;

            return new ScriptEnvironmentTypeResult(q.ToList());
        }
    }
}