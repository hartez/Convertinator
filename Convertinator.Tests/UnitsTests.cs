using FluentAssertions;
using NUnit.Framework;

namespace Convertinator.Tests
{
    [TestFixture]
    public class UnitsTests
    {
        [SetUp]
        public void Setup()
        {
            SI.Celcius
                .CanBeAbbreviated("C", "°C")
                .IsAlsoCalled("centigrade", "celcius", "Centigrade", " C");
        }

        [Test]
        public void CelciusMatchesAlternates()
        {
            SI.Celcius.Matches("C").Should().BeTrue();
            SI.Celcius.Matches("°C").Should().BeTrue();
            SI.Celcius.Matches("celcius").Should().BeTrue();
            SI.Celcius.Matches("Centigrade").Should().BeTrue();
            SI.Celcius.Matches("centigrade").Should().BeTrue();
            SI.Celcius.Matches(" C").Should().BeTrue();

            SI.Celcius.Matches(" foo").Should().BeFalse();
        }
    }
}