using Convertinator.Systems;
using FluentAssertions;
using NUnit.Framework;

namespace Convertinator.Tests
{
    [TestFixture]
    public class UnitsTests
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _celcius =
                SI.Temperature.Celcius
                    .CanBeAbbreviated("C", "°C")
                    .IsAlsoCalled("centigrade", "celcius", "Centigrade", " C");
        }

        #endregion

        private Unit _celcius;

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

        [Test]
        public void PluralsMatch()
        {
            Unit meter = new Unit("meter")
                .UsePluralFormat("{0}s");

            Unit foot = new Unit("foot")
                .PluralizeAs("feet");

            meter.Matches("meters").Should().BeTrue();
            foot.Matches("feet").Should().BeTrue();
        }
    }
}