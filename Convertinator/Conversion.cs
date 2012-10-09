using System.Collections.Generic;
using System.Linq;
using QuickGraph;

namespace Convertinator
{
    public class Conversion : Edge<Unit>, IConversion
    {
        private readonly List<IConversionStep> _steps;

        public IEnumerable<IConversionStep> Steps
        {
            get { return _steps.AsEnumerable(); }
        }

        public void AddStep(IConversionStep step)
        {
            _steps.Add(step);
        }

        public Conversion(Unit source, Unit target)
            : base(source, target)
        {
            _steps = new List<IConversionStep>();
        }

        public Conversion Reverse()
        {
            var reverse = new Conversion(Target, Source);

            foreach(var conversionStep in Steps.Reverse())
            {
                reverse.AddStep(conversionStep.Reverse());
            }

            return reverse;
        }
    }

    public interface IConversion
    {
        IEnumerable<IConversionStep> Steps { get; }
        void AddStep(IConversionStep step);
        Conversion Reverse();
    }
}