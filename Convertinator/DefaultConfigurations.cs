using Convertinator.Systems;

namespace Convertinator
{
    public class DefaultConfigurations
    {
        public static ConversionGraph Length()
        {
            var graph = new ConversionGraph();

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
                Conversions.From(meters).To(decimeters).MultiplyBy(10M),
                Conversions.From(decimeters).To(centimeters).MultiplyBy(10M),
                Conversions.From(centimeters).To(millimeters).MultiplyBy(10M),
                Conversions.From(millimeters).To(micrometers).MultiplyBy(1000M),
                Conversions.From(micrometers).To(nanometers).MultiplyBy(1000M),
                Conversions.From(nanometers).To(picometers).MultiplyBy(1000M),
                Conversions.From(picometers).To(femtometers).MultiplyBy(1000M),
                Conversions.From(femtometers).To(attometers).MultiplyBy(1000M),
                Conversions.From(attometers).To(zeptometers).MultiplyBy(1000M),
                Conversions.From(zeptometers).To(yoctometers).MultiplyBy(1000M),

                Conversions.From(meters).To(kilometers).DivideBy(1000M),
                Conversions.From(meters).To(feet).MultiplyBy(3.28084M),
                Conversions.From(feet).To(miles).DivideBy(0.000189394M),
                Conversions.From(feet).To(inches).MultiplyBy(12M)
                );

            return graph;
        }

        public static ConversionGraph Volume()
        {
            var graph = new ConversionGraph();

            var gallons = US.Volume.Gallon;
            var liter = SI.Volume.Liter;

            graph.AddConversion(
                Conversions.From(gallons).To(liter).MultiplyBy(3.78541M));

            return graph;
        }

        public static ConversionGraph Time()
        {
            var graph = new ConversionGraph();

            var seconds = SI.Time.Second;
            var minutes = SI.Time.Minute;
            var hours = SI.Time.Hour;

            graph.AddConversion(
                Conversions.From(hours).To(minutes).MultiplyBy(60),
                Conversions.From(minutes).To(seconds).MultiplyBy(60)
                );

            return graph;
        }
    }
}