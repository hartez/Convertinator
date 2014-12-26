using Convertinator.Systems;

namespace Convertinator
{
    public class DefaultConfigurations
    {
        public static ConversionGraph<decimal> Length()
        {
            var graph = new ConversionGraph<decimal>();

            graph.AddConversion(
                Conversions.From<decimal>(SI.Length.Meter).To(SI.Length.Decimeter).MultiplyBy(10M),
                Conversions.From<decimal>(SI.Length.Decimeter).To(SI.Length.Centimeter).MultiplyBy(10M),
                Conversions.From<decimal>(SI.Length.Centimeter).To(SI.Length.Millimeter).MultiplyBy(10M),
                Conversions.From<decimal>(SI.Length.Millimeter).To(SI.Length.Micrometer).MultiplyBy(1000M),
                Conversions.From<decimal>(SI.Length.Micrometer).To(SI.Length.Nanometer).MultiplyBy(1000M),
                Conversions.From<decimal>(SI.Length.Nanometer).To(SI.Length.Picometer).MultiplyBy(1000M),
                Conversions.From<decimal>(SI.Length.Picometer).To(SI.Length.Femtometer).MultiplyBy(1000M),
                Conversions.From<decimal>(SI.Length.Femtometer).To(SI.Length.Attometer).MultiplyBy(1000M),
                Conversions.From<decimal>(SI.Length.Attometer).To(SI.Length.Zeptometer).MultiplyBy(1000M),
                Conversions.From<decimal>(SI.Length.Zeptometer).To(SI.Length.Yoctometer).MultiplyBy(1000M),

                Conversions.From<decimal>(SI.Length.Meter).To(SI.Length.Kilometer).DivideBy(1000M),
                Conversions.From<decimal>(SI.Length.Meter).To(US.Length.Foot).MultiplyBy(3.28084M),
                Conversions.From<decimal>(US.Length.Foot).To(US.Length.Mile).DivideBy(0.000189394M),
                Conversions.From<decimal>(US.Length.Foot).To(US.Length.Inch).MultiplyBy(12M)
                );

            return graph;
        }

        public static ConversionGraph<decimal> Volume()
        {
            var graph = new ConversionGraph<decimal>();

            graph.AddConversion(
                Conversions.From<decimal>(US.Volume.Gallon).To(SI.Volume.Liter).MultiplyBy(3.78541M));

            return graph;
        }

        public static ConversionGraph<decimal> Time()
        {
            var graph = new ConversionGraph<decimal>();

            graph.AddConversion(
                Conversions.From<decimal>(SI.Time.Hour).To(SI.Time.Minute).MultiplyBy(60),
                Conversions.From<decimal>(SI.Time.Minute).To(SI.Time.Second).MultiplyBy(60)
                );

            return graph;
        }
    }
}