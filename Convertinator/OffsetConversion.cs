using System;
using System.Diagnostics;

namespace Convertinator
{
    [DebuggerDisplay("Add {_offset} to value")]
    public class OffsetStep<T> : IConversionStep<T>
    {
        private readonly T _offset;
        private readonly Func<T, T> _offsetFunction;

        public OffsetStep(T offset)
        {
            _offset = offset;
            _offsetFunction = input => Numeric<T>.Add(input, offset);
        }

        public T Apply(T input)
        {
            return _offsetFunction(input);
        }

        public IConversionStep<T> Reverse()
        {
            return new OffsetStep<T>(Numeric<T>.Negate(_offset));
        }

        public override string ToString()
        {
            return string.Format("Add {0}", _offset);
        }
    }
}