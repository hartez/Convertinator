using System;
using FluentAssertions;
using NUnit.Framework;

namespace Convertinator.Tests
{
    [TestFixture]
    public class ConversionTests
    {
        private ConversionGraph _graph;
        
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _graph = new ConversionGraph();

            Conversion c = Conversions.One(SI.Meter).In(US.Feet).Is(3.28084M);

            _graph.AddConversion(c);

            Conversion x = Conversions.One(SI.Kilometer).In(SI.Meter).Is(1000M);

            _graph.AddConversion(x);

            _graph
                .RoundUsing(MidpointRounding.AwayFromZero)
                .RoundToDecimalPlaces(4);
        }

        #endregion

        [Test]
        public void ConvertOneFootToMeters()
        {
            var oneFoot = new Measurement(US.Feet, 1M);

            decimal feet = _graph.Convert(oneFoot, SI.Meter);

            feet.Should().Be(0.3048M);
        }

        [Test]
        public void ConvertOneMeterToFeet()
        {
            var oneMeter = new Measurement(SI.Meter, 1M);

            decimal feet = _graph.Convert(oneMeter, US.Feet);

            feet.Should().Be(3.2808M);
        }

        [Test]
        public void ConvertOneKilometerToFeet()
        {
            var oneKilometer = new Measurement(SI.Kilometer, 1M);

            decimal feet = _graph.Convert(oneKilometer, US.Feet);

            feet.Should().Be(3280.84M);
        }

        [Test]
        public void ConvertOneMeterToMeters()
        {
            var oneMeter = new Measurement(SI.Meter, 1M);

            decimal feet = _graph.Convert(oneMeter, SI.Meter);

            feet.Should().Be(1M);
        }
    }
}