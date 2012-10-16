using NScripto.Raw;

namespace NScripto.Wrappers
{
    public class GenericScriptWrapper<T> : IScript<T>
    {
        private readonly IRawScript _script;

        public GenericScriptWrapper(IRawScript script)
        {
            _script = script;
        }

        public void Run(T t)
        {
            _script.Run(t);
        }
    }

    public class GenericScriptWrapper<T1, T2> : IScript<T1, T2>
    {
        private readonly IRawScript _script;

        public GenericScriptWrapper(IRawScript script)
        {
            _script = script;
        }

        public void Run(T1 t1, T2 t2)
        {
            _script.Run(t1, t2);
        }
    }
}