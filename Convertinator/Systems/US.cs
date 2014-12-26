using System;

namespace Convertinator.Systems
{
    public class US
    {
        #region Nested type: Length

        public static class Length
        {
            private static readonly Lazy<Unit> _mile = new Lazy<Unit>(() => new Unit("mile").CanBeAbbreviated("mi").SystemIs("US"));
            private static readonly Lazy<Unit> _foot = new Lazy<Unit>(() => new Unit("foot").CanBeAbbreviated("ft", "'").SystemIs("US").PluralizeAs("feet"));
            private static readonly Lazy<Unit> _inch = new Lazy<Unit>(() => new Unit("inch").CanBeAbbreviated("in", "\"").SystemIs("US").PluralizeAs("inches"));

            public static Unit Mile
            {
                get { return _mile.Value; }
            }

            public static Unit Foot
            {
                get { return _foot.Value; }
            }

            public static Unit Inch
            {
                get { return _inch.Value; }
            }
        }

        #endregion

        #region Nested type: Temperature

        public static class Temperature
        {
            private static readonly Lazy<Unit> _fahrenheit = new Lazy<Unit>(() => new Unit("degrees Fahrenheit").IsAlsoCalled("Fahrenheit"));

            public static Unit Fahrenheit
            {
                get
                {
                    return _fahrenheit.Value;
                }
            }
        }

        #endregion

        #region Nested type: Volume

        public static class Volume
        {
            private static readonly Lazy<Unit> _gallon = new Lazy<Unit>(() => new Unit("gallon").IsAlsoCalled("US liquid gallon").CanBeAbbreviated("gal"));

            public static Unit Gallon
            {
                get { return _gallon.Value; }
            }
        }

        #endregion
    }
}