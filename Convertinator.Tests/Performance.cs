using System;
using System.Diagnostics;
using Convertinator.Systems;
using NUnit.Framework;

namespace Convertinator.Tests
{
    [TestFixture, Explicit]
    public class Performance
    {
        private ConversionGraph _graph;

        [SetUp]
        public void Setup()
        {
            _graph = new ConversionGraph();

            var meter = SI.Length.Meter;
            var mile = US.Length.Mile;
            var feet = US.Length.Foot;
            feet.PluralizeAs("feet");

            var kilometer = SI.Length.Kilometer.HasCounterPart(mile);

            var nanofoot = new Unit("nanofoot").SystemIs("US");
            var nanometer = new Unit("nanometer").SystemIs("metric");
            var picometer = new Unit("picometer").SystemIs("metric");
            nanofoot.HasCounterPart(nanometer);

            _graph.AddConversion(Conversions.One(meter).In(feet).Is(3.28084M));
            _graph.AddConversion(Conversions.One(kilometer).In(meter).Is(1000M));
            _graph.AddConversion(Conversions.One(mile).In(feet).Is(5280M));
            _graph.AddConversion(Conversions.From(feet).To(nanofoot).MultiplyBy(0.000000001M));
            _graph.AddConversion(Conversions.From(picometer).To(nanometer).MultiplyBy(0.001M));
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
                _graph.ConvertSystem(new Measurement("meter", 1M), "metric");
                _graph.ConvertSystem(new Measurement("meter", 1M), "US");
                _graph.ConvertSystem(new Measurement("kilometer", 1M), "US");
                _graph.ConvertSystem(new Measurement("mile", 0.6214M), "metric");
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