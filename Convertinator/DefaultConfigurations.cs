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
    }
}