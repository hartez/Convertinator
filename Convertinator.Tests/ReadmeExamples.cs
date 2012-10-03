using NUnit.Framework;

namespace Convertinator.Tests
{
    [TestFixture]
    public class ReadmeExamples
    {
        [Test]
        public void IndirectConversion()
        {
            var system = new ConversionGraph();

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

            var system = new ConversionGraph()
                .RoundToDecimalPlaces(5);

            system.AddConversion(Conversions.One(meter).In(feet).Is(3.28084M));

            var meterMeasurement = new Measurement(meter, 1);
            var feetMeasurement = new Measurement(feet, 2);

            var x = system.Convert(meterMeasurement, feet);

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

        [Test]
        public void Currencies()
        {
            var dollar = new Unit("dollar")
                .UsePluralFormat("{0}s");

            var yen = new Unit("yen");

            var system = new ConversionGraph().RoundToDecimalPlaces(2);

            system.AddConversion(Conversions.One(dollar).In(yen).Is(78.5300M));

            var dollarAmount = new Measurement(dollar, 10);
            var yenAmount = new Measurement(yen, 10000);

            Assert.That(system.Convert(dollarAmount, yen) == 785.30M);
            Assert.That(system.Convert(yenAmount, dollar) == 127.34M);
        }
    }
}