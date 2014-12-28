using NUnit.Framework;

namespace Convertinator.Tests
{
    [TestFixture]
    public class ReadmeExamples
    {
        [Test]
        public void IndirectConversion()
        {
            var system = ConversionGraph.Build();

            var meter = new Unit("meter");
            var kilometer = new Unit("kilometer");
            var foot = new Unit("foot");
            
            system.AddConversion(
                Conversions.From(kilometer).To(meter).MultiplyBy(1000M),
                Conversions.From(meter).To(foot).MultiplyBy(3.28084M)
                );

            var measurement = new Measurement(kilometer, 100M);

            Assert.That(system.Convert(measurement, foot) == 328084M);

            Assert.That(system.Convert(new Measurement(foot, 328084M), kilometer) == 100M);
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

            var system = ConversionGraph.Build()
                .RoundToDecimalPlaces(5);

            system.AddConversion(Conversions.One(meter).In(feet).Is(3.28084M));

            var meterMeasurement = new Measurement(meter, 1);
            var feetMeasurement = new Measurement(feet, 2);

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