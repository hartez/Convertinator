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
            var meter = SI.Length.Meter;
            var feet = US.Length.Foot;
            var kilometer = SI.Length.Kilometer;

            var c = WhenConverting.One(meter).In(feet).Is(3.28084M);
            var x = WhenConverting.One(kilometer).In(meter).Is(1000M);

            _graph = ConversionGraph.Build(x, c)
                .RoundUsing(MidpointRounding.AwayFromZero)
                .RoundToDecimalPlaces(4);
        }

        #endregion

        [Test]
        public void ConvertOneFootToMeters()
        {
            var oneFoot = new Measurement(US.Length.Foot, 1M);

            var meters = _graph.Convert(oneFoot, SI.Length.Meter);

            meters.Should().Be(0.3048M);
        }

        [Test]
        public void ConvertOneMeterToFeet()
        {
            var oneMeter = new Measurement(SI.Length.Meter, 1M);

            var feet = _graph.Convert(oneMeter, US.Length.Foot);

            feet.Should().Be(3.2808M);
        }

        [Test]
        public void ConvertOneKilometerToFeet()
        {
            var oneKilometer = new Measurement(SI.Length.Kilometer, 1M);

            var feet = _graph.Convert(oneKilometer, US.Length.Foot);

            feet.Should().Be(3280.84M);
        }

        [Test]
        public void ConvertOneMeterToMeters()
        {
            var oneMeter = new Measurement(SI.Length.Meter, 1M);

            var feet = _graph.Convert(oneMeter, SI.Length.Meter);

            feet.Should().Be(1M);
        }
    }
}