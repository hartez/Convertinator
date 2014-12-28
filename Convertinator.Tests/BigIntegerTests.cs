using System;
using System.Diagnostics;
using System.Numerics;
using FluentAssertions;
using NUnit.Framework;

namespace Convertinator.Tests
{
    [TestFixture]
    public class BigIntegerTests
    {
        [Test]
        public void ConvertOneLightYearToYoctometersWithDecimal()
        {
            var lightYear = new Unit("Light Year");
            
            var yoctometers = new Unit("Yoctometer");
            var meters = new Unit("Meter");

            var graph = ConversionGraph.Build(
                WhenConverting.One(lightYear).In(meters).Is((decimal)(9.4605284E+15)),
                WhenConverting.From(meters).To(yoctometers).MultiplyBy((decimal)1E+24)
                );

            var oneLightYear = new Measurement<decimal>(lightYear, 1M);
            Action action = () => graph.Convert(oneLightYear, yoctometers);

            // Throws an overflow exception because the decimal data type can't handle a value of this size
            action.ShouldThrow<OverflowException>();
        }

        [Test]
        public void ConvertOneLightYearToYoctometersWithBigInteger()
        {
            var lightYear = new Unit("Light Year");

            var yoctometers = new Unit("Yoctometer");
            var meters = new Unit("Meter");

            var graph = ConversionGraph<BigInteger>.Build(
                WhenConverting.One<BigInteger>(lightYear).In(meters).Is((BigInteger)(9.4605284E+15)),
                WhenConverting.From<BigInteger>(meters).To(yoctometers).MultiplyBy(BigInteger.Pow(10, 24))
                );

            var oneLightYear = new Measurement<BigInteger>(lightYear, new BigInteger(1));
            var result = graph.Convert(oneLightYear, yoctometers);
            result.Should().Be(BigInteger.Parse("9460528400000000000000000000000000000000"));
        }
    }
}