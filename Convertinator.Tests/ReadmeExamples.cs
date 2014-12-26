using NUnit.Framework;

namespace Convertinator.Tests
{
    [TestFixture]
    public class ReadmeExamples
    {
        [Test]
        public void IndirectConversion()
        {
            var system = new ConversionGraph<decimal>();

            var meter = new Unit("meter");
            var kilometer = new Unit("kilometer");
            var foot = new Unit("foot");
            
            system.AddConversion(
                Conversions.From<decimal>(kilometer).To(meter).MultiplyBy(1000M),
                Conversions.From<decimal>(meter).To(foot).MultiplyBy(3.28084M)
                );

            var measurement = new Measurement<decimal>(kilometer, 100M);

            Assert.That(system.Convert(measurement, foot) == 328084M);

            Assert.That(system.Convert(new Measurement<decimal>(foot, 328084M), kilometer) == 100M);
        }

        [Test]
        public void UnitNames()
        {
            var meter = new Unit("meter")
                .IsAlsoCalled("metre")
                .CanBeAbbreviated("m", "mtr")
                .UsePluralFormat("{0}s");

            var feet = new Unit("foot")
                .PluralizeAs("feet")
                .CanBeAbbreviated("ft");

            var system = new ConversionGraph<decimal>()
                .RoundToDecimalPlaces(5);

            system.AddConversion(Conversions.One<decimal>(meter).In(feet).Is(3.28084M));

            var meterMeasurement = new Measurement<decimal>(meter, 1);
            var feetMeasurement = new Measurement<decimal>(feet, 2);

            Assert.That(system.Convert(meterMeasurement, feet) == 3.28084M);
            Assert.That(system.Convert(meterMeasurement, "ft") == 3.28084M);
            Assert.That(system.Convert(feetMeasurement, "metre") == 0.6096M);
            Assert.That(system.Convert(feetMeasurement, "mtr") == 0.6096M);
            Assert.That(system.Convert(feetMeasurement, "m") == 0.6096M);

            Assert.That(meterMeasurement.ToAbbreviatedString() == "1 m");
            Assert.That(meterMeasurement.ToString() == "1 meter");

            Assert.That(feetMeasurement.ToAbbreviatedString() == "2 ft");
            Assert.That(feetMeasurement.ToString() == "2 feet");
        }
    }
}