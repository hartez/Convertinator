using System;
using System.Diagnostics;
using Convertinator.Systems;
using FluentAssertions;
using NUnit.Framework;

namespace Convertinator.Tests
{
    [TestFixture]
    public class SystemConversionTests
    {
        private ConversionGraph<decimal> _graph;

        [SetUp]
        public void Setup()
        {
            _graph = ConversionGraph.Build();

            var meter = SI.Length.Meter;
            var mile = US.Length.Mile;
            var feet = US.Length.Foot;
            feet.PluralizeAs("feet");

            var kilometer = SI.Length.Kilometer.HasCounterPart(mile);

            var nanofoot = new Unit("nanofoot").SystemIs("US");
            var nanometer = new Unit("nanometer").SystemIs("metric");
            var picometer = new Unit("picometer").SystemIs("metric");
            nanofoot.HasCounterPart(nanometer);

            _graph.AddConversion(Conversions.One(meter).In(feet).Is(3.28084M));
            _graph.AddConversion(Conversions.One(kilometer).In(meter).Is(1000M));
            _graph.AddConversion(Conversions.One(mile).In(feet).Is(5280M));
            _graph.AddConversion(Conversions.From(feet).To(nanofoot).MultiplyBy(0.000000001M));
            _graph.AddConversion(Conversions.From(picometer).To(nanometer).MultiplyBy(0.001M));
            _graph
                .RoundUsing(MidpointRounding.AwayFromZero)
                .RoundToDecimalPlaces(4);
        }

        [Test]
        public void SameSystem()
        {
            var result = _graph.ConvertSystem(new Measurement<decimal>("meter", 1M), "metric");

            result.Value.Should().Be(1M);
            result.Unit.Name.Should().Be("meter");
        }

        [Test]
        public void ImpliedCounterpartInAlternateSystem()
        {
            var result = _graph.ConvertSystem(new Measurement<decimal>("meter", 1M), "US");

            result.Value.Should().Be(3.2808M);
            result.Unit.Name.Should().Be("foot");
        }

        [Test]
        public void ExplicitCounterpartInAlternateSystem()
        {
            var result = _graph.ConvertSystem(new Measurement<decimal>("kilometer", 1M), "US");

            result.Value.Should().Be(0.6214M);
            result.Unit.Name.Should().Be("mile");
        }

        [Test]
        public void ImpliedReverseCounterpart()
        {
            var result = _graph.ConvertSystem(new Measurement<decimal>("mile", 0.6214M), "metric");

            result.Value.Should().Be(1M);
            result.Unit.Name.Should().Be("kilometer");
        }

        [Test]
        public void CounterpartWithNoPath()
        {
            Assert.Throws(typeof(ConversionNotFoundException),
                () => _graph.ConvertSystem(new Measurement<decimal>("nanofoot", 1M), "metric"));
        }
    }
}