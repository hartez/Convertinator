using Convertinator.Systems;

namespace Convertinator
{
    public class DefaultConfigurations
    {
        public static ConversionGraph<decimal> Length()
        {
            return ConversionGraph.Build(
                WhenConverting.From(SI.Length.Meter).To(SI.Length.Decimeter).MultiplyBy(10M),
                WhenConverting.From(SI.Length.Decimeter).To(SI.Length.Centimeter).MultiplyBy(10M),
                WhenConverting.From(SI.Length.Centimeter).To(SI.Length.Millimeter).MultiplyBy(10M),
                WhenConverting.From(SI.Length.Millimeter).To(SI.Length.Micrometer).MultiplyBy(1000M),
                WhenConverting.From(SI.Length.Micrometer).To(SI.Length.Nanometer).MultiplyBy(1000M),
                WhenConverting.From(SI.Length.Nanometer).To(SI.Length.Picometer).MultiplyBy(1000M),
                WhenConverting.From(SI.Length.Picometer).To(SI.Length.Femtometer).MultiplyBy(1000M),
                WhenConverting.From(SI.Length.Femtometer).To(SI.Length.Attometer).MultiplyBy(1000M),
                WhenConverting.From(SI.Length.Attometer).To(SI.Length.Zeptometer).MultiplyBy(1000M),
                WhenConverting.From(SI.Length.Zeptometer).To(SI.Length.Yoctometer).MultiplyBy(1000M),
                WhenConverting.From(SI.Length.Meter).To(SI.Length.Kilometer).DivideBy(1000M),
                WhenConverting.From(SI.Length.Meter).To(US.Length.Foot).MultiplyBy(3.28084M),
                WhenConverting.From(US.Length.Foot).To(US.Length.Mile).DivideBy(0.000189394M),
                WhenConverting.From(US.Length.Foot).To(US.Length.Inch).MultiplyBy(12M)
                );
        }

        public static ConversionGraph<decimal> Volume()
        {
            return ConversionGraph.Build(
                WhenConverting.From(US.Volume.Gallon).To(SI.Volume.Liter).MultiplyBy(3.78541M));
        }

        public static ConversionGraph<decimal> Time()
        {
            return ConversionGraph.Build(
                WhenConverting.From<decimal>(SI.Time.Hour).To(SI.Time.Minute).MultiplyBy(60),
                WhenConverting.From<decimal>(SI.Time.Minute).To(SI.Time.Second).MultiplyBy(60)
                );
        }
    }
}