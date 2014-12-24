using FluentAssertions;
using NUnit.Framework;

namespace Convertinator.Tests.DefaultConfigurations
{
    [TestFixture]
    public class TimeTests
    {
        private ConversionGraph<decimal> _time;

        [SetUp]
        public void SetUp()
        {
            _time = Convertinator.DefaultConfigurations.Time();
        }

        [Test]
        public void OneHourIsSixtyMinutes()
        {
            _time.Convert(new Measurement<decimal>("hour", 1M), "minute").Should().Be(60M);
        }
    }
}