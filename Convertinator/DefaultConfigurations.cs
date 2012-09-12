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

            graph.AddConversion(
                Conversions.From(meters).To(kilometers).DivideBy(1000M),
                Conversions.From(meters).To(feet).MultiplyBy(3.28084M),
                Conversions.From(feet).To(miles).DivideBy(0.000189394M)
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