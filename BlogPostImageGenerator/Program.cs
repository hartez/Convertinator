using System.Diagnostics;
using Convertinator;

namespace BlogPostImageGenerator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Quick and dirty example of generating graph images for debugging
            // This assumes you have Graphviz 2.28 installed

            var meter_to_feet = ConversionGraph.Build();

            var meter = new Unit("meter");
            var feet = new Unit("foot");

            meter_to_feet.AddConversion(WhenConverting.One<decimal>(meter).In(feet).Is(3.28084M));

            meter_to_feet.ToDotFile("meter_feet.dot", VisualizationOptions.None);

            Process.Start(@"C:\Program Files (x86)\Graphviz 2.28\bin\dot.exe", "-Tpng meter_feet.dot -o meter_feet.png");

            var more_complex = ConversionGraph.Build();

            var kilometer = new Unit("kilometer");
            var inches = new Unit("inch");

            more_complex.AddConversion(
                WhenConverting.From<decimal>(kilometer).To(meter).MultiplyBy(1000M),
                WhenConverting.From<decimal>(meter).To(feet).MultiplyBy(3.28084M),
                WhenConverting.From<decimal>(feet).To(inches).MultiplyBy(12M)
                );

            more_complex.ToDotFile("more_complex.dot", VisualizationOptions.NumberEdges);

            Process.Start(@"C:\Program Files (x86)\Graphviz 2.28\bin\dot.exe",
                "-Tpng more_complex.dot -o more_complex.png");

            DefaultConfigurations.Length()
                .ToDotFile("length.dot", VisualizationOptions.NumberEdges | VisualizationOptions.ShowSystem);

            Process.Start(@"C:\Program Files (x86)\Graphviz 2.28\bin\dot.exe",
                "-Tpng length.dot -o length.png");
        }
    }
}