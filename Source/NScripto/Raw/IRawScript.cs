using System.Linq;
using System.Collections.Generic;
using System;

namespace NScripto.Raw
{
    public interface IRawScript
    {
        void Run(params object[] objs);
    }
}