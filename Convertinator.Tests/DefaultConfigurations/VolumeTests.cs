using Convertinator.Systems;
using FluentAssertions;
using NUnit.Framework;

namespace Convertinator.Tests.DefaultConfigurations
{
    [TestFixture]
    public class VolumeTests
    {
        private ConversionGraph _volume;

        [SetUp]
        public void SetUp()
        {
            _volume = Convertinator.DefaultConfigurations.Volume();
        }

        [Test]
        public void ConvertOneGallonToLiters()
        {
            var gallon = new Measurement(US.Volume.Gallon, 1M);

            decimal liters = _volume.Convert(gallon, SI.Volume.Liter);

            liters.Should().Be(3.7854M);
        }
    }
}