using System.IO;
using QuickGraph.Graphviz;
using QuickGraph.Graphviz.Dot;

namespace Convertinator
{
    public sealed class FileDotEngine : IDotEngine
    {
        public string Run(GraphvizImageType imageType, string dot, string outputFileName)
        {
            string output = outputFileName;
            if(!output.EndsWith(".dot"))
            {
                output += ".dot";
            }

            File.WriteAllText(output, dot);
            return output;
        }
    }
}