using System;
using System.Diagnostics;

namespace Convertinator
{
    [DebuggerDisplay("Multiply value by {_scalar}")]
    public class ScalingConversion<T> : IConversionStep<T>
    {
        private readonly T _scalar = Numeric<T>.One();
        private readonly Func<T, T> _scale;

        public ScalingConversion(T scalar)
        {
            _scalar = scalar;
            _scale = input => Numeric<T>.Multiply(input, _scalar);
        }


        public T Apply(T input)
        {
            return _scale(input);
        }

        public IConversionStep<T> Reverse()
        {
            return new ScalingConversion<T>(Numeric<T>.Invert(_scalar));
        }

        public override string ToString()
        {
            return string.Format("Multiply by {0}", _scalar);
        }
    }
}