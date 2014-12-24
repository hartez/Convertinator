using System;
using Convertinator.Systems;
using FluentAssertions;
using NUnit.Framework;

namespace Convertinator.Tests
{
    [TestFixture]
    public class ConversionTests
    {
        private ConversionGraph<decimal> _graph;
        
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _graph = new ConversionGraph<decimal>();

            var meter = SI.Length.Meter;
            var feet = US.Length.Foot;
            var kilometer = SI.Length.Kilometer;

            Conversion<decimal> c = Conversions.One<decimal>(meter).In(feet).Is(3.28084M);

            _graph.AddConversion(c);

            Conversion<decimal> x = Conversions.One<decimal>(kilometer).In(meter).Is(1000M);

            _graph.AddConversion(x);

            _graph
                .RoundUsing(MidpointRounding.AwayFromZero)
                .RoundToDecimalPlaces(4);
        }

        #endregion

        [Test]
        public void ConvertOneFootToMeters()
        {
            var oneFoot = new Measurement<decimal>(US.Length.Foot, 1M);

            decimal meters = _graph.Convert(oneFoot, SI.Length.Meter);

            meters.Should().Be(0.3048M);
        }

        [Test]
        public void ConvertOneMeterToFeet()
        {
            var oneMeter = new Measurement<decimal>(SI.Length.Meter, 1M);

            decimal feet = _graph.Convert(oneMeter, US.Length.Foot);

            feet.Should().Be(3.2808M);
        }

        [Test]
        public void ConvertOneKilometerToFeet()
        {
            var oneKilometer = new Measurement<decimal>(SI.Length.Kilometer, 1M);

            decimal feet = _graph.Convert(oneKilometer, US.Length.Foot);

            feet.Should().Be(3280.84M);
        }

        [Test]
        public void ConvertOneMeterToMeters()
        {
            var oneMeter = new Measurement<decimal>(SI.Length.Meter, 1M);

            decimal feet = _graph.Convert(oneMeter, SI.Length.Meter);

            feet.Should().Be(1M);
        }
    }
}