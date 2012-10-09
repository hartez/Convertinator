using System;
using System.Diagnostics;

namespace Convertinator
{
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

        public override string ToString()
        {
            return string.Format("Add {0}", _offset);
        }
    }
}