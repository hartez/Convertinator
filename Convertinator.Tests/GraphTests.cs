using System;
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
            _graph = new ConversionGraph();

            var meter = SI.Length.Meter;

            meter
                .CanBeAbbreviated("mtr")
                .IsAlsoCalled("Metres");

            Conversion c = Conversions.One(meter).In(US.Length.Foot).Is(3.28084M);

            _graph.AddConversion(c);
            
            Conversion x = Conversions.One(SI.Length.Kilometer).In(meter).Is(1000M);

            _graph.AddConversion(x);

            _graph
                .RoundUsing(MidpointRounding.AwayFromZero)
                .RoundToDecimalPlaces(4);
        }

        #endregion

        private ConversionGraph _graph;

        [Test]
        public void FindVertexForFeetShouldSucceed()
        {
            Unit vertex = _graph.FindVertex(US.Length.Foot);

            vertex.Should().NotBeNull();
        }

        [Test]
        public void FindVertexForLiterShouldFail()
        {
            Unit start = _graph.FindVertex(new Measurement(SI.Volume.Liter, 42M));

            start.Should().BeNull();
        }

        [Test]
        public void FindVertexForMeterShouldSucceed()
        {
            Unit start = _graph.FindVertex(new Measurement(SI.Length.Meter, 42M));

            start.Should().NotBeNull();
        }

        [Test]
        public void FindVertexForMeterStringShouldSucceed()
        {
            Unit start = _graph.FindVertex("Metres");

            start.Should().NotBeNull();
        }

        [Test]
        public void FindVertexForMeterAliasShouldSucceed()
        {
            Unit start = _graph.FindVertex("Metres");

            start.Should().NotBeNull();

            start = _graph.FindVertex("mtr");

            start.Should().NotBeNull();
        }

        [Test]
        public void FindVertexForMeasurementFromMeterAliasShouldSucceed()
        {
            Unit start = _graph.FindVertex(new Measurement("Metres", 10));

            start.Should().NotBeNull();

            start = _graph.FindVertex(new Measurement("mtr", 10));

            start.Should().NotBeNull();
        }

        [Test]
        public void FindVertexForMeasurementFromMeterNameShouldSucceed()
        {
            Unit start = _graph.FindVertex(new Measurement("Metres", 10));

            start.Should().NotBeNull();
        }


        [Test]
        public void GraphShouldHaveThreeUnitsAndTwoConversions()
        {
            _graph.VertexCount.Should().Be(3);
            _graph.EdgeCount.Should().Be(4);
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