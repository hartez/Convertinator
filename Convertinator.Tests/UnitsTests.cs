using FluentAssertions;
using NUnit.Framework;
using Convertinator.Systems;

namespace Convertinator.Tests
{
    [TestFixture]
    public class UnitsTests
    {
        [SetUp]
        public void Setup()
        {
            SI.Temperature.Celcius
                .CanBeAbbreviated("C", "°C")
                .IsAlsoCalled("centigrade", "celcius", "Centigrade", " C");
        }

        [Test]
        public void CelciusMatchesAlternates()
        {
            SI.Temperature.Celcius.Matches("C").Should().BeTrue();
            SI.Temperature.Celcius.Matches("°C").Should().BeTrue();
            SI.Temperature.Celcius.Matches("celcius").Should().BeTrue();
            SI.Temperature.Celcius.Matches("Centigrade").Should().BeTrue();
            SI.Temperature.Celcius.Matches("centigrade").Should().BeTrue();
            SI.Temperature.Celcius.Matches(" C").Should().BeTrue();

            SI.Temperature.Celcius.Matches(" foo").Should().BeFalse();
        }
    }
}