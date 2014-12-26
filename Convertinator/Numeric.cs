using System;

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
}