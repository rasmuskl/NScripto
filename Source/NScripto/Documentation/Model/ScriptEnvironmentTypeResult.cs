using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;

namespace NScripto.Documentation.Model
{
    public class ScriptEnvironmentTypeResult : IEnumerable<Type>
    {
        private readonly List<Type> _environments;

        public ScriptEnvironmentTypeResult(IEnumerable<Type> environments)
        {
            _environments = new List<Type>(environments);
        }

        public int EnvironmentCount
        {
            get { return _environments.Count; }
        }

        public IEnumerable<Type> EnvironmentTypes
        {
            get { return _environments.AsReadOnly(); }
        }

        public IEnumerator<Type> GetEnumerator()
        {
            return _environments.AsReadOnly().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}