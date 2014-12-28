using System;
using Convertinator.Systems;
using FluentAssertions;
using NUnit.Framework;

namespace Convertinator.Tests
{
    [TestFixture]
    public class TemperatureTests
    {
        [SetUp]
        public void Setup()
        {
            // TODO See if there's any way to change Conversions to Conversion here so it reads better
            // Or maybe change it to "WhenConverting"?

            _graph = ConversionGraph.Build(
                WhenConverting.From(US.Temperature.Fahrenheit).To(SI.Temperature.Celcius).Subtract(32).MultiplyBy(5M / 9M),
                WhenConverting.From(SI.Temperature.Celcius).To(new Unit("Kelvin")).Add(273.15M))
                .RoundUsing(MidpointRounding.AwayFromZero).RoundToDecimalPlaces(4);

            SI.Temperature.Celcius
                .CanBeAbbreviated("C", "°C")
                .IsAlsoCalled("centigrade", "celcius", "Centigrade");
        }

        private ConversionGraph<decimal> _graph;

        [Test]
        public void BoilingCelciusToFahrenheit()
        {
            var degrees = new Measurement(new Unit("Celcius"), 100);

            var result = _graph.Convert(degrees, new Unit("Fahrenheit"));

            result.Should().Be(212M);
        }

        [Test]
        public void BoilingFahrenheitToCelcius()
        {
            var degrees = new Measurement("Fahrenheit", 212M);

            var result = _graph.Convert(degrees, "Celcius");

            result.Should().Be(100M);
        }

        [Test]
        public void HundredDegreesFahrenheitInKelvin()
        {
            var degrees = new Measurement(new Unit("Fahrenheit"), 100M);

            var result = _graph.Convert(degrees, new Unit("Kelvin"));

            result.Should().Be(310.9278M);
        }

        [Test]
        public void ThirtyTwoDegreesFahrenheitInCelcius()
        {
            var degrees = new Measurement("Fahrenheit", 32M);

            var result = _graph.Convert(degrees, "Celcius");

            result.Should().Be(0M);
        }

        [Test]
        public void ZeroDegreesCelciusInFahrenheit()
        {
            var degrees = new Measurement(new Unit("Celcius"), 0M);

            var result = _graph.Convert(degrees, "Fahrenheit");

            result.Should().Be(32M);
        }

        [Test]
        public void ZeroDegreesCelciusInKelvin()
        {
            var degrees = new Measurement(new Unit("Celcius"), 0M);

            var result = _graph.Convert(degrees, "Kelvin");

            result.Should().Be(273.15M);
        }
    }
}