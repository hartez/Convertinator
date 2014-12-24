using System;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;

namespace Convertinator
{
    public static class Numeric<T>
    {
        public static T One()
        {
            dynamic v = 1;
            return (T)v;
        }

        public static T Multiply(dynamic v1, dynamic v2)
        {
            return (T)(v1 * v2);
        }

        public static T Divide(dynamic v1, dynamic v2)
        {
            return (T)(v1 / v2);
        }

        public static T Add(dynamic v1, dynamic v2)
        {
            return (T)(v1 + v2);
        }

        public static T Negate(dynamic value)
        {
            return (T)(0 - value);
        }

        public static T Invert(dynamic value)
        {
            return Numeric<T>.Divide(1, value);
        }

        public static T Round(dynamic value, int decimalPlaces, MidpointRounding roundingMode)
        {
            if(value is double)
            {
                return (T)(dynamic)Math.Round((double) value, decimalPlaces, roundingMode);
            }

            if (value is decimal)
            {
                return (T)(dynamic)Math.Round((decimal)value, decimalPlaces, roundingMode);
            }

            // TODO We need to just remove rounding from this altogether
            return (T)value;
        }
    }

    [DebuggerDisplay("Multiply value by {_scalar}")]
    public class ScalingConversion<T> : IConversionStep<T>
    {
        public ScalingConversion(T scalar)
        {
            _scalar = scalar;
            _scale = input => Numeric<T>.Multiply(input, _scalar);
        }

        private readonly T _scalar = Numeric<T>.One();
        private readonly Func<T, T> _scale;
        

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