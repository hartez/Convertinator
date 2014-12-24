using System;
using Convertinator.Systems;
using FluentAssertions;
using NUnit.Framework;

namespace Convertinator.Tests
{
    [TestFixture]
    public class TemperatureTests
    {
        private ConversionGraph<decimal> _graph;

        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _graph = new ConversionGraph<decimal>()
                .RoundUsing(MidpointRounding.AwayFromZero)
                .RoundToDecimalPlaces(4);

            var fahrenheit = US.Temperature.Fahrenheit;
            var celcius = SI.Temperature.Celcius;

            _graph.AddConversion(
                Conversions.From<decimal>(fahrenheit).To(celcius).Subtract(32).MultiplyBy(5M / 9M));

            _graph.AddConversion(Conversions.From<decimal>(celcius).To(new Unit("Kelvin")).Add(273.15M));

            SI.Temperature.Celcius
                .CanBeAbbreviated("C", "°C")
                .IsAlsoCalled("centigrade", "celcius", "Centigrade");
        }

        #endregion

        [Test]
        public void ThirtyTwoDegreesFahrenheitInCelcius()
        {
            var degrees = new Measurement<decimal>("Fahrenheit", 32M);

            var result = _graph.Convert(degrees, "Celcius");

            result.Should().Be(0M);
        }

        [Test]
        public void ZeroDegreesCelciusInFahrenheit()
        {
            var degrees = new Measurement<decimal>(new Unit("Celcius"), 0M);

            var result = _graph.Convert(degrees, "Fahrenheit");

            result.Should().Be(32M);
        }

        [Test]
        public void ZeroDegreesCelciusInKelvin()
        {
            var degrees = new Measurement<decimal>(new Unit("Celcius"), 0M);

            var result = _graph.Convert(degrees, "Kelvin");

            result.Should().Be(273.15M);
        }

        [Test]
        public void BoilingCelciusToFahrenheit()
        {
            var degrees = new Measurement<decimal>(new Unit("Celcius"), 100);

            var result = _graph.Convert(degrees, new Unit("Fahrenheit"));

            result.Should().Be(212M);
        }

        [Test]
        public void BoilingFahrenheitToCelcius()
        {
            var degrees = new Measurement<decimal>("Fahrenheit", 212M);

            var result = _graph.Convert(degrees, "Celcius");

            result.Should().Be(100M);
        }

        [Test]
        public void HundredDegreesFahrenheitInKelvin()
        {
            var degrees = new Measurement<decimal>(new Unit("Fahrenheit"), 100M);

            var result = _graph.Convert(degrees, new Unit("Kelvin"));

            result.Should().Be(310.9278M);
        }
    }
}