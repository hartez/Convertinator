using System;
using System.Numerics;
using Convertinator.Systems;
using FluentAssertions;
using NUnit.Framework;

namespace Convertinator.Tests
{
    [TestFixture]
    public class ConversionTests
    {
        private ConversionGraph _graph;
        
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _graph = new ConversionGraph();

            var meter = SI.Length.Meter;
            var feet = US.Length.Foot;
            var kilometer = SI.Length.Kilometer;

            Conversion<decimal> c = Conversions.One<decimal>(meter).In(feet).Is(3.28084M);

            _graph.AddConversion(c);

            Conversion<decimal> x = Conversions.One<decimal>(kilometer).In(meter).Is(1000M);

            _graph.AddConversion(x);

            _graph
                .RoundUsing(MidpointRounding.AwayFromZero)
                .RoundToDecimalPlaces(4);
        }

        #endregion

        [Test]
        public void ConvertOneFootToMeters()
        {
            var oneFoot = new Measurement<decimal>(US.Length.Foot, 1M);

            decimal meters = _graph.Convert(oneFoot, SI.Length.Meter);

            meters.Should().Be(0.3048M);
        }

        [Test]
        public void ConvertOneMeterToFeet()
        {
            var oneMeter = new Measurement<decimal>(SI.Length.Meter, 1M);

            decimal feet = _graph.Convert(oneMeter, US.Length.Foot);

            feet.Should().Be(3.2808M);
        }

        [Test]
        public void ConvertOneKilometerToFeet()
        {
            var oneKilometer = new Measurement<decimal>(SI.Length.Kilometer, 1M);

            decimal feet = _graph.Convert(oneKilometer, US.Length.Foot);

            feet.Should().Be(3280.84M);
        }

        [Test]
        public void ConvertOneMeterToMeters()
        {
            var oneMeter = new Measurement<decimal>(SI.Length.Meter, 1M);

            decimal feet = _graph.Convert(oneMeter, SI.Length.Meter);

            feet.Should().Be(1M);
        }

        [Test]
        public void ConvertOneLightYearToYoctometersWithDecimal()
        {
            var lightYear = new Unit("Light Year"); 
            var oneLightYear = new Measurement<decimal>(lightYear, 1M);

            var graph = new ConversionGraph<decimal>();

            graph.AddConversion(
                Conversions.One<decimal>(lightYear).In(SI.Length.Meter).Is((decimal) (9.4605284 * Math.Pow(10, 15))),
                Conversions.From<decimal>(SI.Length.Meter).To(SI.Length.Nanometer).MultiplyBy((decimal) Math.Pow(10, 9)),
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

            var graph = new ConversionGraph<BigInteger>();

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