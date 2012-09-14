using FluentAssertions;
using NUnit.Framework;

namespace Convertinator.Tests
{
    [TestFixture]
    public class MeasurementOutputTests
    {
        [Test]
        public void OneUnitDefaultName()
        {
            var meter = new Unit("meter");
            var oneMeter = new Measurement(meter, 1M);

            oneMeter.ToString().Should().Be("1 meter");
        }

        [Test]
        public void MultipleUnitsPlural()
        {
            var meter = new Unit("meter")
                .UsePluralFormat("{0}s");

            var twoMeters = new Measurement(meter, 2M);

            twoMeters.ToString().Should().Be("2 meters");
        }

        [Test]
        public void AdjectiveForm()
        {
            var unit = new Unit("foot")
                .PluralizeAs("feet");

            var tenfoot = new Measurement(unit, 10M);

            var phrase = string.Format("I wouldn't touch that with a {0} pole.",
                                       tenfoot.ToAdjectiveString());

            phrase.Should().Be("I wouldn't touch that with a 10-foot pole.");
        }

        [Test]
        public void OneUnitExplicitDisplayName()
        {
            var meter = new Unit("meter")
                .DisplayWithName("metre");
            var oneMeter = new Measurement(meter, 1M);

            oneMeter.ToString().Should().Be("1 metre");
        }

        [Test]
        public void MultipleUnitsExplicitDisplayNameAndPluralFormat()
        {
            var meter = new Unit("meter")
                .DisplayWithName("metre")
                .UsePluralFormat("{0}s");

            var measure = new Measurement(meter, 5M);

            measure.ToString().Should().Be("5 metres");
        }

        [Test]
        public void OneUnitImplicitAbbreviation()
        {
            var meter = new Unit("meter")
                .CanBeAbbreviated("m");

            var oneMeter = new Measurement(meter, 1M);

            oneMeter.ToAbbreviatedString().Should().Be("1 m");
        }

        [Test]
        public void OneUnitExplicitAbbreviation()
        {
            var meter = new Unit("meter")
                .CanBeAbbreviated("m")
                .DisplayWithAbbreviation("mtr");

            var oneMeter = new Measurement(meter, 1M);

            oneMeter.ToAbbreviatedString().Should().Be("1 mtr");
        }
    }
}