namespace Convertinator
{
    public interface IConversionStep
    {
        decimal Apply(decimal input);
        IConversionStep Reverse();
    }
}