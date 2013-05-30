using System.Linq;
using System.Collections.Generic;
using System;

namespace NScripto.Documentation
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

        #region Equality members

        protected bool Equals(EnvironmentDocumentation other)
        {
            return EnvironmentType == other.EnvironmentType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((EnvironmentDocumentation) obj);
        }

        public override int GetHashCode()
        {
            return (EnvironmentType != null ? EnvironmentType.GetHashCode() : 0);
        }

        public static bool operator ==(EnvironmentDocumentation left, EnvironmentDocumentation right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(EnvironmentDocumentation left, EnvironmentDocumentation right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}