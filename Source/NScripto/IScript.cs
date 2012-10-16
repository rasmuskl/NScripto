namespace NScripto
{
    public interface IScript<T>
    {
        void Run(T t);
    }

    public interface IScript<T, T2>
    {
        void Run(T t, T2 t2);
    }

    public interface IScript<T, T2, T3>
    {
        void Run(T t, T2 t2, T3 t3);
    }

    public interface IScript<T, T2, T3, T4>
    {
        void Run(T t, T2 t2, T3 t3, T4 t4);
    }
}