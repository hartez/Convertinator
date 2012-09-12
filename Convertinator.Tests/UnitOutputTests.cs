using System;
using Convertinator.Systems;
using FluentAssertions;
using NUnit.Framework;

namespace Convertinator.Tests
{
    [TestFixture]
    public class UnitOutputTests
    {
        [Test]
        public void DefaultDisplayNameIsUnitName()
        {
            var meter = new Unit("meter");

            meter.ToString().Should().Be("meter");
        }

        [Test]
        public void OverrideDisplayName()
        {
            var meter = new Unit("meter")
                .DisplayWithName("metre");

            meter.ToString().Should().Be("metre");
        }

        [Test]
        public void AbbreviateWithFullNameIfNoAbbreviationsAreSet()
        {
            var meter = new Unit("meter");

            meter.ToAbbreviatedString().Should().Be("meter");
        }

        [Test]
        public void AbbreviateWithFirstAbbreviationIfNoDisplayAbbreviationsIsSet()
        {
            var meter = new Unit("meter").CanBeAbbreviated("m", "mtr", "foo");

            meter.ToAbbreviatedString().Should().Be("m");
        }

        [Test]
        public void OverrideAbbreviation()
        {
            var meter = new Unit("meter").CanBeAbbreviated("m", "mtr", "foo").DisplayWithAbbreviation("mtr");

            meter.ToAbbreviatedString().Should().Be("mtr");
        }
    }
}