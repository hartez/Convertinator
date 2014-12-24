using Convertinator.Systems;

namespace Convertinator
{
    public class DefaultConfigurations
    {
        public static ConversionGraph<decimal> Length()
        {
            var graph = new ConversionGraph<decimal>();

            var meters = SI.Length.Meter;
            var kilometers = SI.Length.Kilometer;
            var feet = US.Length.Foot;
            var miles = US.Length.Mile;
            var inches = US.Length.Inch;
            var decimeters = SI.Length.Decimeter;
            var centimeters = SI.Length.Centimeter;
            var millimeters = SI.Length.Millimeter;
            var micrometers = SI.Length.Micrometer;
            var nanometers = SI.Length.Nanometer;
            var picometers = SI.Length.Picometer;
            var femtometers = SI.Length.Femtometer;
            var attometers = SI.Length.Attometer;
            var zeptometers = SI.Length.Zeptometer;
            var yoctometers = SI.Length.Yoctometer;

            graph.AddConversion(
                Conversions.From<decimal>(meters).To(decimeters).MultiplyBy(10M),
                Conversions.From<decimal>(decimeters).To(centimeters).MultiplyBy(10M),
                Conversions.From<decimal>(centimeters).To(millimeters).MultiplyBy(10M),
                Conversions.From<decimal>(millimeters).To(micrometers).MultiplyBy(1000M),
                Conversions.From<decimal>(micrometers).To(nanometers).MultiplyBy(1000M),
                Conversions.From<decimal>(nanometers).To(picometers).MultiplyBy(1000M),
                Conversions.From<decimal>(picometers).To(femtometers).MultiplyBy(1000M),
                Conversions.From<decimal>(femtometers).To(attometers).MultiplyBy(1000M),
                Conversions.From<decimal>(attometers).To(zeptometers).MultiplyBy(1000M),
                Conversions.From<decimal>(zeptometers).To(yoctometers).MultiplyBy(1000M),

                Conversions.From<decimal>(meters).To(kilometers).DivideBy(1000M),
                Conversions.From<decimal>(meters).To(feet).MultiplyBy(3.28084M),
                Conversions.From<decimal>(feet).To(miles).DivideBy(0.000189394M),
                Conversions.From<decimal>(feet).To(inches).MultiplyBy(12M)
                );

            return graph;
        }

        public static ConversionGraph<decimal> Volume()
        {
            var graph = new ConversionGraph<decimal>();

            var gallons = US.Volume.Gallon;
            var liter = SI.Volume.Liter;

            graph.AddConversion(
                Conversions.From<decimal>(gallons).To(liter).MultiplyBy(3.78541M));

            return graph;
        }

        public static ConversionGraph<decimal> Time()
        {
            var graph = new ConversionGraph<decimal>();

            var seconds = SI.Time.Second;
            var minutes = SI.Time.Minute;
            var hours = SI.Time.Hour;

            graph.AddConversion(
                Conversions.From<decimal>(hours).To(minutes).MultiplyBy(60),
                Conversions.From<decimal>(minutes).To(seconds).MultiplyBy(60)
                );

            return graph;
        }
    }
}