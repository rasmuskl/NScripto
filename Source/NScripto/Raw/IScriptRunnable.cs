using System.Linq;
using System.Collections.Generic;
using System;

namespace NScripto.Raw
{
    public interface IScriptRunnable
    {
        void Run();
        void Initialize(object[] objs);
    }
}