using Convertinator.Systems;
using FluentAssertions;
using NUnit.Framework;

namespace Convertinator.Tests.DefaultConfigurations
{
    [TestFixture]
    public class LengthTests
    {
        private ConversionGraph _length;

        [SetUp]
        public void SetUp()
        {
            _length = Convertinator.DefaultConfigurations.Length();
        }

        [Test]
        public void ConvertOneFootToMeters()
        {
            var oneFoot = new Measurement(US.Length.Foot, 1M);

            decimal meters = _length.Convert(oneFoot, SI.Length.Meter);

            meters.Should().Be(0.3048M);
        }

        [Test]
        public void ConvertOneMeterToFeet()
        {
            var oneMeter = new Measurement(SI.Length.Meter, 1M);

            decimal feet = _length.Convert(oneMeter, US.Length.Foot);

            feet.Should().Be(3.2808M);
        }

        [Test]
        public void ConvertOneKilometerToFeet()
        {
            var oneKilometer = new Measurement(SI.Length.Kilometer, 1M);

            decimal feet = _length.Convert(oneKilometer, US.Length.Foot);

            feet.Should().Be(3280.84M);
        }

        [Test]
        public void ConvertOneMeterToMeters()
        {
            var oneMeter = new Measurement(SI.Length.Meter, 1M);

            decimal feet = _length.Convert(oneMeter, SI.Length.Meter);

            feet.Should().Be(1M);
        }
    }
}