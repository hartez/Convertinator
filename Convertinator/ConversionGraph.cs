using System;
using System.Collections.Generic;
using System.Linq;
using QuickGraph;
using QuickGraph.Algorithms;

namespace Convertinator
{
    public class ConversionGraph : BidirectionalGraph<Unit, Conversion>
    {
        private int _decimalPlaces = 4;
        private MidpointRounding _roundingMode;

        public ConversionGraph RoundToDecimalPlaces(int decimalPlaces)
        {
            _decimalPlaces = decimalPlaces;
            return this;
        }

        public ConversionGraph RoundUsing(MidpointRounding roundingMode)
        {
            _roundingMode = roundingMode;
            return this;
        }

        public void AddConversion(Conversion conversion)
        {
            AddVerticesAndEdge(conversion);
            AddVerticesAndEdge(conversion.Reverse());
        }

        public Unit FindVertex(Measurement measurement)
        {
            return FindVertex(measurement.Unit);
        }

        public Unit FindVertex(Unit unit)
        {
            return FindVertex(unit.Name);
        }

        public Unit FindVertex(string unit)
        {
            return Vertices.FirstOrDefault(u => u.Matches(unit));
        }

        public decimal Convert(Measurement source, Unit target)
        {
            // Find the vertex that matches the source
            var start = FindVertex(source);

            if (start == null)
            {
                throw new ConversionNotFoundException(
                    string.Format("Source units {0} are not configured", source.Unit));
            }

            // Find the vertex that matches the target
            var end = FindVertex(target);

            if(end == null)
            {
                throw new ConversionNotFoundException(
                   string.Format("Target units {0} are not configured", target));
            }

            if(start == end)
            {
                return source.Value;
            }

            // Find a path from the source to the target
            Func<Conversion, double> edgeCost = e => 1;

            TryFunc<Unit, IEnumerable<Conversion>> tryGetPaths = this.ShortestPathsDijkstra(edgeCost, start);
            
            IEnumerable<Conversion> path;

            if (tryGetPaths(end, out path))
            {
                // Aggregate all of the steps in each conversion
                var steps = new List<IConversionStep>();
                foreach (var conversion in path)
                {
                    steps.AddRange(conversion.Steps);
                }

                var result = steps.Aggregate(source.Value, (current, step) => step.Apply(current));

                return Math.Round(result, _decimalPlaces, _roundingMode);
            }

            throw new ConversionNotFoundException(string.Format("No conversion between {0} and {1} has been configured", source.Unit, target));
        }
    }
}