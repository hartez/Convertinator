using FluentAssertions;
using NUnit.Framework;
using Convertinator.Systems;

namespace Convertinator.Tests
{
    [TestFixture]
    public class UnitsTests
    {
        private Unit _celcius;

        [SetUp]
        public void Setup()
        {
            _celcius = 
            SI.Temperature.Celcius
                .CanBeAbbreviated("C", "°C")
                .IsAlsoCalled("centigrade", "celcius", "Centigrade", " C");
        }

        [Test]
        public void CelciusMatchesAlternates()
        {
            _celcius.Matches("C").Should().BeTrue();
            _celcius.Matches("°C").Should().BeTrue();
            _celcius.Matches("celcius").Should().BeTrue();
            _celcius.Matches("Centigrade").Should().BeTrue();
            _celcius.Matches("centigrade").Should().BeTrue();
            _celcius.Matches(" C").Should().BeTrue();

            _celcius.Matches(" foo").Should().BeFalse();
        }
    }
}