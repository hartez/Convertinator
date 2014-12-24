using System;
using Convertinator.Systems;
using FluentAssertions;
using NUnit.Framework;

namespace Convertinator.Tests.DefaultConfigurations
{
    [TestFixture]
    public class LengthTests
    {
        private ConversionGraph<decimal> _length;

        [SetUp]
        public void SetUp()
        {
            _length = Convertinator.DefaultConfigurations.Length();
        }

        [Test]
        public void OneMeterInYoctometers()
        {
            var oneMeter = new Measurement<decimal>(SI.Length.Meter, 1M);

            var result = _length.Convert(oneMeter, SI.Length.Yoctometer);

            var target = Convert.ToDecimal(Math.Pow(10, 24));

            result.Should().Be(target);
        }

        [Test]
        public void OneYoctometerInMeters()
        {
            _length.RoundToDecimalPlaces(25);

            var oneYocto = new Measurement<decimal>(SI.Length.Yoctometer, 1M);

            var result = _length.Convert(oneYocto, SI.Length.Meter);

            var target = Convert.ToDecimal(Math.Pow(10, -24));

            result.Should().Be(target);
        }

        [Test]
        public void ConvertOneFootToMeters()
        {
            var oneFoot = new Measurement<decimal>(US.Length.Foot, 1M);

            decimal meters = _length.Convert(oneFoot, SI.Length.Meter);

            meters.Should().Be(0.3048M);
        }

        [Test]
        public void ConvertOneMeterToFeet()
        {
            var oneMeter = new Measurement<decimal>(SI.Length.Meter, 1M);

            decimal feet = _length.Convert(oneMeter, US.Length.Foot);

            feet.Should().Be(3.2808M);
        }

        [Test]
        public void ConvertOneKilometerToFeet()
        {
            var oneKilometer = new Measurement<decimal>(SI.Length.Kilometer, 1M);

            decimal feet = _length.Convert(oneKilometer, US.Length.Foot);

            feet.Should().Be(3280.84M);
        }

        [Test]
        public void ConvertOneMeterToMeters()
        {
            var oneMeter = new Measurement<decimal>(SI.Length.Meter, 1M);

            decimal feet = _length.Convert(oneMeter, SI.Length.Meter);

            feet.Should().Be(1M);
        }
    }
}