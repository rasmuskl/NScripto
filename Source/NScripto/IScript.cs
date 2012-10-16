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
}