using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Convertinator.Systems;

namespace Convertinator.Tests
{
    [TestFixture]
    public class GraphTests
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            var meter = SI.Length.Meter;

            meter
                .UsePluralFormat("{0}s")
                .CanBeAbbreviated("mtr")
                .IsAlsoCalled("Metres");

            var feet = US.Length.Foot;
            feet.PluralizeAs("feet");

            var c = WhenConverting.One(meter).In(feet).Is(3.28084M);
            var x = WhenConverting.One(SI.Length.Kilometer).In(meter).Is(1000M); 

            _graph = ConversionGraph.Build();

            _graph.AddConversion(c);
            _graph.AddConversion(x);

            _graph
                .RoundUsing(MidpointRounding.AwayFromZero)
                .RoundToDecimalPlaces(4);
        }

        #endregion

        private ConversionGraph<decimal> _graph;

        [Test]
        public void GraphShouldHaveThreeUnits()
        {
            _graph.ConfiguredUnits.Count().Should().Be(3);
        }

        [Test]
        public void NonexistentSourceUnitsShouldThrowException()
        {
            var oneFoo = new Measurement(new Unit("foo"), 1M);
            Assert.Throws(typeof(ConversionNotFoundException), () => _graph.Convert(oneFoo, US.Length.Foot));
        }

        [Test]
        public void NonexistentTargetUnitsShouldThrowException()
        {
            var oneMeter = new Measurement(SI.Length.Meter, 1M);
            Assert.Throws(typeof(ConversionNotFoundException), () => _graph.Convert(oneMeter, new Unit("foo")));
        }
    }
}