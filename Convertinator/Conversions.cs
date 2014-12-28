using System;

namespace Convertinator
{
    public static class WhenConverting
    {
        public static Measurement<T> From<T>(Unit unit)
        {
            return new Measurement<T>(unit, Numeric<T>.One());
        }

        public static Measurement From(Unit unit)
        {
            return new Measurement(unit, Numeric<decimal>.One());
        }
    
        public static Conversion<T> To<T>(this Measurement<T> source, Unit unit)
        {
            return new Conversion<T>(source.Unit, unit);
        }

        public static Measurement<T> One<T>(Unit unit)
        {
            return new Measurement<T>(unit, Numeric<T>.One());
        }

        public static Measurement One(Unit unit)
        {
            return new Measurement(unit, Numeric<decimal>.One());
        }

        public static Tuple<Measurement<T>, Measurement<T>> In<T>(this Measurement<T> source, Unit target)
        {
            return new Tuple<Measurement<T>, Measurement<T>>(source, One<T>(target));
        }

        public static Conversion<T> Is<T>(this Tuple<Measurement<T>, Measurement<T>> conversion, T absolute)
        {
            T scalar = Numeric<T>.Divide(absolute, conversion.Item1.Value);

            var result = new Conversion<T>(conversion.Item1.Unit, conversion.Item2.Unit);

            result.AddStep(new ScalingStep<T>(scalar));

            return result;
        }
    }
}