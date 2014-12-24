using System;
using System.Diagnostics;
using Convertinator.Systems;
using NUnit.Framework;

namespace Convertinator.Tests
{
    [TestFixture, Explicit]
    public class Performance
    {
        private ConversionGraph<double> _graph;

        [SetUp]
        public void Setup()
        {
            _graph = new ConversionGraph<double>();

            var meter = SI.Length.Meter;
            var mile = US.Length.Mile;
            var feet = US.Length.Foot;
            feet.PluralizeAs("feet");

            var kilometer = SI.Length.Kilometer.HasCounterPart(mile);

            var nanofoot = new Unit("nanofoot").SystemIs("US");
            var nanometer = new Unit("nanometer").SystemIs("metric");
            var picometer = new Unit("picometer").SystemIs("metric");
            nanofoot.HasCounterPart(nanometer);

            _graph.AddConversion(Conversions.One<double>(meter).In(feet).Is(3.28084d));
            _graph.AddConversion(Conversions.One<double>(kilometer).In(meter).Is(1000d));
            _graph.AddConversion(Conversions.One<double>(mile).In(feet).Is(5280d));
            _graph.AddConversion(Conversions.From<double>(feet).To(nanofoot).MultiplyBy(0.000000001d));
            _graph.AddConversion(Conversions.From<double>(picometer).To(nanometer).MultiplyBy(0.001d));
            _graph
                .RoundUsing(MidpointRounding.AwayFromZero)
                .RoundToDecimalPlaces(4);
        }

        [Test]
        public void LotsOfConversions()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            const int loops = 100000;

            for(var n = 0; n < loops; n++)
            {
                _graph.ConvertSystem(new Measurement<double>("meter", 1d), "metric");
                _graph.ConvertSystem(new Measurement<double>("meter", 1d), "US");
                _graph.ConvertSystem(new Measurement<double>("kilometer", 1d), "US");
                _graph.ConvertSystem(new Measurement<double>("mile", 0.6214d), "metric");
            }

            stopWatch.Stop();
            var ts = stopWatch.Elapsed;

            var elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);

            Console.WriteLine("Ran {1} loops in {0}", elapsedTime, loops);
        }
    }
}