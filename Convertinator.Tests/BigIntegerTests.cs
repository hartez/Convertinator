using System;
using System.Numerics;
using Convertinator.Systems;
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
            var oneLightYear = new Measurement<decimal>(lightYear, 1M);

            var graph = ConversionGraph<decimal>.Build();

            graph.AddConversion(
                Conversions.One<decimal>(lightYear).In(SI.Length.Meter).Is((decimal)(9.4605284 * Math.Pow(10, 15))),
                Conversions.From<decimal>(SI.Length.Meter).To(SI.Length.Nanometer).MultiplyBy((decimal)Math.Pow(10, 9)),
                Conversions.From<decimal>(SI.Length.Nanometer).To(SI.Length.Yoctometer).MultiplyBy((decimal)Math.Pow(10, 15))
                );

            Action action = () => graph.Convert(oneLightYear, SI.Length.Yoctometer);

            // Throws an overflow exception because the decimal data type can't handle a value of this size
            action.ShouldThrow<OverflowException>();
        }

        [Test]
        public void ConvertOneLightYearToYoctometersWithBigInteger()
        {
            var lightYear = new Unit("Light Year");
            var oneLightYear = new Measurement<BigInteger>(lightYear, new BigInteger(1));

            var graph = ConversionGraph<BigInteger>.Build();

            graph.AddConversion(
                Conversions.One<BigInteger>(lightYear).In(SI.Length.Meter).Is((BigInteger)(9.4605284 * Math.Pow(10, 15))),
                Conversions.From<BigInteger>(SI.Length.Meter).To(SI.Length.Nanometer).MultiplyBy((BigInteger)Math.Pow(10, 9)),
                Conversions.From<BigInteger>(SI.Length.Nanometer).To(SI.Length.Yoctometer).MultiplyBy((BigInteger)Math.Pow(10, 15))
                );

            var yocotmeters = graph.Convert(oneLightYear, SI.Length.Yoctometer);
            yocotmeters.Should().Be(BigInteger.Parse("9460528400000000000000000000000000000000"));
        }
    }
}