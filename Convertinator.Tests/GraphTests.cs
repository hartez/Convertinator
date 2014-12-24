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
            _graph = new ConversionGraph<decimal>();

            var meter = SI.Length.Meter;

            meter
                .UsePluralFormat("{0}s")
                .CanBeAbbreviated("mtr")
                .IsAlsoCalled("Metres");

            var feet = US.Length.Foot;
            feet.PluralizeAs("feet");

            Conversion<decimal> c = Conversions.One<decimal>(meter).In(feet).Is(3.28084M);

            _graph.AddConversion(c);

            Conversion<decimal> x = Conversions.One<decimal>(SI.Length.Kilometer).In(meter).Is(1000M);

            _graph.AddConversion(x);

            _graph
                .RoundUsing(MidpointRounding.AwayFromZero)
                .RoundToDecimalPlaces(4);
        }

        #endregion

        private ConversionGraph<decimal> _graph;

        [Test]
        public void FindVertexForFeetShouldSucceed()
        {
            Unit vertex = _graph.FindVertex(US.Length.Foot);

            vertex.Should().NotBeNull();
        }

        [Test]
        public void FindVertexForLiterShouldFail()
        {
            Unit start = _graph.FindVertex(new Measurement<decimal>(SI.Volume.Liter, 42M));

            start.Should().BeNull();
        }

        [Test]
        public void FindVertexForMeterShouldSucceed()
        {
            Unit start = _graph.FindVertex(new Measurement<decimal>(SI.Length.Meter, 42M));

            start.Should().NotBeNull();
        }

        [Test]
        public void FindVertexForPluralFormatShouldSucceed()
        {
            Unit start = _graph.FindVertex(new Measurement<decimal>("meters", 42M));

            start.Should().NotBeNull();
        }

        [Test]
        public void FindVertexForExplicitPluralShouldSucceed()
        {
            Unit start = _graph.FindVertex(new Measurement<decimal>("feet", 42M));

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
            Unit start = _graph.FindVertex(new Measurement<decimal>("Metres", 10));

            start.Should().NotBeNull();

            start = _graph.FindVertex(new Measurement<decimal>("mtr", 10));

            start.Should().NotBeNull();
        }

        [Test]
        public void FindVertexForMeasurementFromMeterNameShouldSucceed()
        {
            Unit start = _graph.FindVertex(new Measurement<decimal>("Metres", 10));

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
            var oneFoo = new Measurement<decimal>(new Unit("foo"), 1M);
            Assert.Throws(typeof(ConversionNotFoundException), () => _graph.Convert(oneFoo, US.Length.Foot));
        }

        [Test]
        public void NonexistentTargetUnitsShouldThrowException()
        {
            var oneMeter = new Measurement<decimal>(SI.Length.Meter, 1M);
            Assert.Throws(typeof(ConversionNotFoundException), () => _graph.Convert(oneMeter, new Unit("foo")));
        }
    }
}