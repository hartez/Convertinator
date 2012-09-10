using System;
using System.Diagnostics;

namespace Convertinator
{
    public interface IConversionStep
    {
        decimal Apply(decimal input);
        IConversionStep Reverse();
    }

    [DebuggerDisplay("Add {_offset} to value")]
    public class OffsetConversion : IConversionStep
    {
        private readonly decimal _offset;
        private readonly Func<decimal, decimal> _offsetFunction;

        public OffsetConversion(decimal offset)
        {
            _offset = offset;
            _offsetFunction = input => input + offset;
        }

        public decimal Apply(decimal input)
        {
            return _offsetFunction(input);
        }

        public IConversionStep Reverse()
        {
            return new OffsetConversion(-_offset);
        }
    }

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
    }
}