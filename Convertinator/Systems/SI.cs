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
                        .CanBeAbbreviated("m")
                        .SystemIs("metric");
                }
            }

            public static Unit Kilometer
            {
                get
                {
                    return new Unit("kilometer")
                        .IsAlsoCalled("kilometre")
                        .CanBeAbbreviated("km").SystemIs("metric");
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
            {
                get { return new Unit("kelvin"); }
            }
        }

        #endregion

        #region Nested type: Time

        /// <summary>
        /// For now, just handling time measured in seconds, minutes, hours. 
        /// Things like metric time and leap seconds can wait.
        /// </summary>
        public static class Time
        {
            public static Unit Second
            {
                get
                {
                    return new Unit("second")
                        .CanBeAbbreviated("s", "sec");
                }
            }

            public static Unit Minute
            {
                get
                {
                    return new Unit("minute")
                        .CanBeAbbreviated("min");
                }
            }

            public static Unit Hour
            {
                get { return new Unit("hour")
                    .CanBeAbbreviated("h", "hr"); }
            }
        }

        #endregion

        #region Nested type: Volume

        public static class Volume
        {
            public static Unit Liter
            {
                get
                {
                    return new Unit("liter")
                        .IsAlsoCalled("litre")
                        .CanBeAbbreviated("l", "L");
                }
            }
        }

        #endregion
    }
}