using System;
using Convertinator.Systems;
using FluentAssertions;
using NUnit.Framework;

namespace Convertinator.Tests
{
    [TestFixture]
    public class SystemConversionTests
    {
        private ConversionGraph _graph;

        [SetUp]
        public void Setup()
        {
            _graph = new ConversionGraph();

            var meter = SI.Length.Meter;
            var mile = US.Length.Mile;
            var feet = US.Length.Foot;
            var kilometer = SI.Length.Kilometer.HasCounterPart(mile);
            feet.PluralizeAs("feet");

            _graph.AddConversion(Conversions.One(meter).In(feet).Is(3.28084M));
            _graph.AddConversion(Conversions.One(kilometer).In(meter).Is(1000M));
            _graph.AddConversion(Conversions.One(mile).In(feet).Is(5280M));
            _graph
                .RoundUsing(MidpointRounding.AwayFromZero)
                .RoundToDecimalPlaces(4);
        }

        [Test]
        public void ImpliedCounterpartInAlternateSystem()
        {
            var result = _graph.ConvertSystem(new Measurement("meter", 1M), "US");

            result.Value.Should().Be(3.2808M);
            result.Unit.Name.Should().Be("foot");
        }

        [Test]
        public void ExplicitCounterpartInAlternateSystem()
        {
            var result = _graph.ConvertSystem(new Measurement("kilometer", 1M), "US");

            result.Value.Should().Be(0.6214M);
            result.Unit.Name.Should().Be("mile");
        }
    }
}