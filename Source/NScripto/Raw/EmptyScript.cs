using System.Linq;
using System.Collections.Generic;
using System;

namespace NScripto.Raw
{
    public class EmptyScript : IRawScript
    {
        public void Run(params object[] objs)
        {
        }
    }
}