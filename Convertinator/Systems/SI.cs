namespace Convertinator.Systems
{
    public static class SI
    {
        #region Nested type: Length

        public static class Length
        {
            public static Unit Meter
            {
                get
                {
                    return new Unit("meter")
                        .IsAlsoCalled("metre")
                        .CanBeAbbreviated("m");
                }
            }

            public static Unit Kilometer
            {
                get
                {
                    return new Unit("kilometer")
                        .IsAlsoCalled("kilometre")
                        .CanBeAbbreviated("km");
                }
            }
        }

        #endregion

        #region Nested type: Temperature

        public static class Temperature
        {
            public static Unit Celcius
            {
                get
                {
                    return new Unit("degrees Celcius")
                        .IsAlsoCalled("Celcius");
                }
            }

            public static Unit Kelvin
            {get
                {
                    return new Unit("kelvin");
                }
            }
        }

        #endregion

        #region Nested type: Volume

        public static class Volume
        {
            public static Unit Liter
            {
                get { return new Unit("liter")
                    .IsAlsoCalled("litre")
                    .CanBeAbbreviated("l", "L"); }
            }
        }

        #endregion
    }
}