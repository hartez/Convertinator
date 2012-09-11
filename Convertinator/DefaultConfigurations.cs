using Convertinator.Systems;

namespace Convertinator
{
    public class DefaultConfigurations
    {
        public static ConversionGraph Length()
        {
            var graph = new ConversionGraph();

            graph.AddConversion(
                Conversions.From(SI.Length.Meter).To(SI.Length.Kilometer).DivideBy(1000M),
                Conversions.From(SI.Length.Meter).To(US.Length.Foot).MultiplyBy(3.28084M),
                Conversions.From(US.Length.Foot).To(US.Length.Mile).DivideBy(0.000189394M)
                );

            return graph;
        }
    }
}