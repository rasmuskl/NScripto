using System.Linq;
using System.Collections.Generic;
using System;

namespace NScripto.Raw
{
    public class RawScriptWrapper : IRawScript
    {
        private readonly Type _compiledScriptType;
        private readonly int _environmentTypeCount;

        public RawScriptWrapper(Type compiledScriptType, int environmentTypeCount)
        {
            _compiledScriptType = compiledScriptType;
            _environmentTypeCount = environmentTypeCount;
        }

        public void Run(params object[] objs)
        {
            if (objs == null)
            {
                throw new ArgumentNullException("objs");
            }

            if (objs.Length != _environmentTypeCount)
            {
                throw new ArgumentException("Script requires " + _environmentTypeCount + " arguments.", "objs");
            }

            if (objs.Any(x => x == null))
            {
                throw new ArgumentException("Argument objects cannot be null.");
            }

            var script = (IScriptRunnable)Activator.CreateInstance(_compiledScriptType);
            script.Initialize(objs);
            script.Run();
        }
    }
}