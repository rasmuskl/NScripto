using System.Linq;
using System.Collections.Generic;
using System;

namespace NScripto.Raw
{
    public class RawScriptWrapper : IRawScript
    {
        private readonly Type _compiledScriptType;

        public RawScriptWrapper(Type compiledScriptType)
        {
            _compiledScriptType = compiledScriptType;
        }

        public void Run(params object[] objs)
        {
            var script = (IScriptRunnable)Activator.CreateInstance(_compiledScriptType);

            foreach (var obj in objs)
            {
                script.Initialize(obj);
            }

            script.Run();
        }
    }
}