using System.Collections.Generic;
using System.Linq;
using QuickGraph;

namespace Convertinator
{
    public class Conversion<T> : Edge<Unit>, IConversion<T>
    {
        private readonly List<IConversionStep<T>> _steps;

        public IEnumerable<IConversionStep<T>> Steps
        {
            get { return _steps.AsEnumerable(); }
        }

        public void AddStep(IConversionStep<T> step)
        {
            _steps.Add(step);
        }

        public Conversion(Unit source, Unit target)
            : base(source, target)
        {
            _steps = new List<IConversionStep<T>>();
        }

        public Conversion<T> Reverse()
        {
            var reverse = new Conversion<T>(Target, Source);

            foreach(var conversionStep in Steps.Reverse())
            {
                reverse.AddStep(conversionStep.Reverse());
            }

            return reverse;
        }
    }

    public interface IConversion<T>
    {
        IEnumerable<IConversionStep<T>> Steps { get; }
        void AddStep(IConversionStep<T> step);
        Conversion<T>  Reverse();
    }
}