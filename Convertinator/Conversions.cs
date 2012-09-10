using System;

namespace Convertinator
{
    public static class Conversions
    {
        public static Measurement One(Unit unit)
        {
            return new Measurement(unit, 1M);
        }

        public static Measurement From(Unit unit)
        {
            return new Measurement(unit, 1M);
        }

        public static Conversion To(this Measurement source, Unit unit)
        {
            return new Conversion(source.Unit, unit);
        }

        public static Tuple<Measurement, Measurement> In(this Measurement source, Unit target)
        {
            return new Tuple<Measurement, Measurement>(source, One(target));
        }

        public static Conversion Is(this Tuple<Measurement, Measurement> conversion, decimal absolute)
        {
            decimal scalar = absolute / conversion.Item1.Value;

            var result = new Conversion(conversion.Item1.Unit, conversion.Item2.Unit);

            result.AddStep(new ScalingConversion(scalar));

            return result;
        }

        public static Conversion Add(this Conversion conversion, decimal value)
        {
            conversion.AddStep(new OffsetConversion(value));
            return conversion;
        }

        public static Conversion Subtract(this Conversion conversion, decimal value)
        {
            return conversion.Add(-value);
        }

        public static Conversion MultiplyBy(this Conversion conversion, decimal value)
        {
            conversion.AddStep(new ScalingConversion(value));
            return conversion;
        }

        public static Conversion DivideBy(this Conversion conversion, decimal value)
        {
            return conversion.MultiplyBy(1 / value);
        }
    }
}