using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using QuickGraph;
using QuickGraph.Algorithms;
using QuickGraph.Algorithms.Search;
using QuickGraph.Graphviz;
using QuickGraph.Graphviz.Dot;

namespace Convertinator
{
    public static class ConversionGraph 
    {
        public static ConversionGraph<T> Build<T>(params Conversion<T>[] conversions)
        {
            return ConversionGraph<T>.Build(conversions);
        }

        public static ConversionGraph<decimal> Build(params Conversion<decimal>[] conversions)
        {
            return Build<decimal>(conversions);
        }
    }

    // TODO Think about making the FindVertex methods private and removing those tests
    // TODO think about having the graph be internal (instead of deriving from it) so there aren't so many normally unused methods on ConversionGraph
    // TODO Think about a method for configuring the rounding method per type (basically a type -> method array we keep)

    public class ConversionGraph<T> : BidirectionalGraph<Unit, Conversion<T>>
    {
        private int _decimalPlaces = 4;
        private MidpointRounding _roundingMode;

        public static ConversionGraph<T> Build(params Conversion<T>[] conversions)
        {
            var graph = new ConversionGraph<T>();

            graph.AddConversions(conversions);

            return graph;
        }

        protected ConversionGraph()
        {
        }

        public ConversionGraph<T> RoundToDecimalPlaces(int decimalPlaces)
        {
            _decimalPlaces = decimalPlaces;
            return this;
        }

        public ConversionGraph<T> RoundUsing(MidpointRounding roundingMode)
        {
            _roundingMode = roundingMode;
            return this;
        }

        public void AddConversions(params Conversion<T>[] conversions)
        {
            foreach (var additionalConversion in conversions)
            {
                AddVerticesAndEdge(additionalConversion);
                AddVerticesAndEdge(additionalConversion.Reverse());
            }
        }

        public void AddConversion(Conversion<T> conversion, params Conversion<T>[] moreConversions)
        {
            AddVerticesAndEdge(conversion);
            AddVerticesAndEdge(conversion.Reverse());
            
            AddConversions(moreConversions);
        }

        public IEnumerable<Unit> ConfiguredUnits
        {
            get { return Vertices; }
        }

        public Unit FindVertex(Measurement<T> measurement)
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

        public T Convert(Measurement<T> source, string target)
        {
            return Convert(source, new Unit(target));
        }

        public T Convert(Measurement<T> source, Unit target)
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

            var path = ComputeShortestPath(start, end);

            if (path != null)
            {
                return WalkConversionPath(source, path);
            }

            throw new ConversionNotFoundException(string.Format("No conversion between {0} and {1} has been configured", source.Unit, target));
        }

        private T WalkConversionPath(Measurement<T> source, IEnumerable<Conversion<T>> path)
        {
            // Aggregate all of the steps in each conversion
            var steps = new List<IConversionStep<T>>();
            foreach (var conversion in path)
            {
                steps.AddRange(conversion.Steps);
            }

            var result = steps.Aggregate(source.Value, (current, step) => step.Apply(current));

            return Numeric<T>.Round(result, _decimalPlaces, _roundingMode);
        }

        private IEnumerable<Conversion<T>> ComputeShortestPath(Unit start, Unit end)
        {
            // Find a path from the source to the target
            Func<Conversion<T>, double> edgeCost = e => 1;

            TryFunc<Unit, IEnumerable<Conversion<T>>> tryGetPaths = this.ShortestPathsDijkstra(edgeCost, start);

            IEnumerable<Conversion<T>> path;

            return tryGetPaths(end, out path) ? path : null;
        }

        private IEnumerable<Conversion<T>> ComputeShortestPathFromCandidates(Unit start, IEnumerable<Unit> candidates)
        {
            IEnumerable<Conversion<T>> currentShortest = null;
            
            foreach(var candidate in candidates)
            {
                var candidateConversionPath = ComputeShortestPath(start, candidate);

                if(candidateConversionPath == null)
                {
                    continue;
                }

                if(currentShortest == null)
                {
                    currentShortest = candidateConversionPath;
                    continue;
                }

                if(candidateConversionPath.Count() < currentShortest.Count())
                {
                    currentShortest = candidateConversionPath;
                }
            }

            return currentShortest;
        }

        private Unit FindCounterpartInSystem(Unit source, string system)
        {
            return source.Counterparts.FirstOrDefault(c => c.System == system);
        }

        public Measurement<T> ConvertSystem(Measurement<T> source, string system)
        {
            var sourceUnit = FindVertex(source);

            // If the source measurement is already in the specified system, just return it
            if (sourceUnit.System == system)
            {
                return source;
            }

            // If the source measurement has an explicitly designated counterpart
            // in the target system, convert to that counterpart
            var dest = FindCounterpartInSystem(sourceUnit, system);

            if(dest != null)
            {
                try
                {
                    return new Measurement<T>(dest, Convert(source, dest));
                }
                catch(ConversionNotFoundException exception)
                {
                    throw new ConversionNotFoundException(
                        string.Format("Source unit {0} has a counterpart {1} defined in the target system, but a couversion could not be made between the units", source.Unit, dest), exception);
                }
            }

            // See if we can find any units for the target system and, if so, whether there's a path 
            // to convert to any of them. If so, use the shortest one (implied counterpart)
            var candidates = new List<Unit>();
            var search = new DepthFirstSearchAlgorithm<Unit, Conversion<T>>(this);
            search.DiscoverVertex += vertex =>
                                            {
                                                if(vertex.System == system)
                                                {
                                                    candidates.Add(vertex);
                                                }
                                            };                                       
            search.Compute(sourceUnit);

            var path = ComputeShortestPathFromCandidates(sourceUnit, candidates);

            if (path != null)
            {
                return new Measurement<T>(path.Last().Target, WalkConversionPath(source, path));
            }

            throw new ConversionNotFoundException(string.Format("The specified measurement unit {0} doesn't have an explicit counterpart in the target system {1} and a path to the target system {1} could not be found.", source.Unit, system));
        }

        public void ToDotFile(string path, VisualizationOptions options)
        {
            var graphviz = new GraphvizAlgorithm<Unit, Conversion<T>>(this);

            graphviz.GraphFormat.RankDirection = GraphvizRankDirection.LR;

            graphviz.FormatVertex += (sender, args) =>
                                         {
                                             var label = args.Vertex.Name;

                                             if (options == (options | VisualizationOptions.ShowSystem))
                                             {
                                                 if(!String.IsNullOrEmpty(args.Vertex.System))
                                                 {
                                                     label += string.Format(" [{0}]", args.Vertex.System);
                                                 }
                                             }
                                             args.VertexFormatter.Label = label;
                                         };

            var edgeIndex = 0;

            graphviz.FormatEdge +=
                (sender, args) =>
                    {
                        string label = String.Empty;

                        if(options == (options | VisualizationOptions.NumberEdges))
                        {
                            edgeIndex += 1;
                            label = edgeIndex.ToString(CultureInfo.InvariantCulture);
                        }

                        if(options == (options | VisualizationOptions.ShowFullConversionDescriptions))
                        {
                            label += 
                                args.Edge.Steps.Aggregate(String.Empty,
                                                      (output, step) =>
                                                      (String.IsNullOrEmpty(output.Trim()) ? String.Empty : " ") + 
                                                      output + step.ToString() +
                                                      (String.IsNullOrEmpty(output.Trim()) ? String.Empty : ", "));
                        }
                        
                        args.EdgeFormatter.Label.Value = label;
                    };

            graphviz.Generate(new FileDotEngine(), path);
        }
    }

    [Flags]
    public enum VisualizationOptions
    {
        None,
        ShowFullConversionDescriptions = 2,
        NumberEdges = 4,
        ShowSystem = 8  
    }
}