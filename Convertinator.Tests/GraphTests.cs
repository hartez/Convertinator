using System;
using FluentAssertions;
using NUnit.Framework;

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

            Conversion c = Conversions.One(SI.Meter).In(US.Feet).Is(3.28084M);

            SI.Meter
                .CanBeAbbreviated("m")
                .IsAlsoCalled("Meters");

            _graph.AddConversion(c);

            Conversion x = Conversions.One(SI.Kilometer).In(SI.Meter).Is(1000M);

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
            Unit vertex = _graph.FindVertex(US.Feet);

            vertex.Should().NotBeNull();
        }

        [Test]
        public void FindVertexForLiterShouldFail()
        {
            Unit start = _graph.FindVertex(new Measurement(SI.Liter, 42M));

            start.Should().BeNull();
        }

        [Test]
        public void FindVertexForMeterShouldSucceed()
        {
            Unit start = _graph.FindVertex(new Measurement(SI.Meter, 42M));

            start.Should().NotBeNull();
        }

        [Test]
        public void FindVertexForMeterStringShouldSucceed()
        {
            Unit start = _graph.FindVertex("Meter");

            start.Should().NotBeNull();
        }

        [Test]
        public void FindVertexForMeterAliasShouldSucceed()
        {
            Unit start = _graph.FindVertex("Meters");

            start.Should().NotBeNull();

            start = _graph.FindVertex("m");

            start.Should().NotBeNull();
        }

        [Test]
        public void FindVertexForMeasurementFromMeterAliasShouldSucceed()
        {
            Unit start = _graph.FindVertex(new Measurement("Meters", 10));

            start.Should().NotBeNull();

            start = _graph.FindVertex(new Measurement("m", 10));

            start.Should().NotBeNull();
        }

        [Test]
        public void FindVertexForMeasurementFromMeterNameShouldSucceed()
        {
            Unit start = _graph.FindVertex(new Measurement("Meter", 10));

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
            Assert.Throws(typeof(ConversionNotFoundException), () => _graph.Convert(oneFoo, US.Feet));
        }

        [Test]
        public void NonexistentTargetUnitsShouldThrowException()
        {
            var oneMeter = new Measurement(SI.Meter, 1M);
            Assert.Throws(typeof(ConversionNotFoundException), () => _graph.Convert(oneMeter, new Unit("foo")));
        }
    }
}