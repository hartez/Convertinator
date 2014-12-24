namespace Convertinator
{
    public interface IConversionStep<T>
    {
        T Apply(T input);
        IConversionStep<T> Reverse();
    }
}