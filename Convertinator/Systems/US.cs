namespace Convertinator.Systems
{
    public class US
    {
        #region Nested type: Length

        public static class Length
        {
            public static Unit Mile
            {
                get
                {
                    return new Unit("mile")
                        .CanBeAbbreviated("mi").SystemIs("US");
                }
            }

            public static Unit Foot
            {
                get { return new Unit("foot").CanBeAbbreviated("ft", "'").SystemIs("US").PluralizeAs("feet"); }
            }

            public static Unit Inch
            {
                get { return new Unit("inch").CanBeAbbreviated("in", "\"").SystemIs("US").PluralizeAs("inches"); }
            }
        }

        #endregion

        #region Nested type: Temperature

        public static class Temperature
        {
            public static Unit Fahrenheit
            {
                get
                {
                    return new Unit("degrees Fahrenheit")
                        .IsAlsoCalled("Fahrenheit");
                }
            }
        }

        #endregion

        #region Nested type: Volume

        public static class Volume
        {
            public static Unit Gallon
            {
                get { return new Unit("gallon")
                    .IsAlsoCalled("US liquid gallon")
                    .CanBeAbbreviated("gal"); }
            }
        }

        #endregion
    }
}