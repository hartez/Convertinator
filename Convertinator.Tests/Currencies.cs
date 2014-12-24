using NUnit.Framework;

namespace Convertinator.Tests
{
    [TestFixture]
    public class Currencies
    {
        [Test]
        public void DollarsToYen()
        {
            var dollar = new Unit("dollar")
                .UsePluralFormat("{0}s");

            var yen = new Unit("yen");

            var system = new ConversionGraph<decimal>().RoundToDecimalPlaces(2);

            system.AddConversion(Conversions.One<decimal>(dollar).In(yen).Is(78.5300M));

            var dollarAmount = new Measurement<decimal>(dollar, 10);
            var yenAmount = new Measurement<decimal>(yen, 10000);

            Assert.That(system.Convert(dollarAmount, yen) == 785.30M);
            Assert.That(system.Convert(yenAmount, dollar) == 127.34M);
        }
    }
}