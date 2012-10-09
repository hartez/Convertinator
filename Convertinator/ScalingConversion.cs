using System;
using System.Diagnostics;

namespace Convertinator
{
    [DebuggerDisplay("Multiply value by {_scalar}")]
    public class ScalingConversion : IConversionStep
    {
        public ScalingConversion(decimal scalar)
        {
            _scalar = scalar;
            _scale = input => input * _scalar;
        }

        private readonly decimal _scalar = 1M;
        private readonly Func<decimal, decimal> _scale;
        

        public decimal Apply(decimal input)
        {
            return _scale(input);
        }

        public IConversionStep Reverse()
        {
            return new ScalingConversion(1/_scalar);
        }

        public override string ToString()
        {
            return string.Format("Multiply by {0}", _scalar);
        }
    }
}