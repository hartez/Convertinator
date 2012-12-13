namespace Convertinator.Systems
{
    public static class SI
    {
        const string Metric = "metric";

        #region Nested type: Length

        public static class Length
        {
            public static Unit Kilometer
            {
                get { return new Unit("kilometer").IsAlsoCalled("kilometre").CanBeAbbreviated("km").SystemIs(Metric); }
            }

            public static Unit Meter
            {
                get { return new Unit("meter").IsAlsoCalled("metre").CanBeAbbreviated("m").SystemIs(Metric); }
            }

            public static Unit Decimeter
            {
                get { return new Unit("decimeter").IsAlsoCalled("decimetre").CanBeAbbreviated("dm").SystemIs(Metric); }
            }

            public static Unit Centimeter
            {
                get { return new Unit("centimeter").IsAlsoCalled("centimetre").CanBeAbbreviated("cm").SystemIs(Metric); }
            }

            public static Unit Millimeter
            {
                get { return new Unit("millimeter").IsAlsoCalled("millimetre").CanBeAbbreviated("mm").SystemIs(Metric); }
            }

            public static Unit Micrometer
            {
                get { return new Unit("micrometer").IsAlsoCalled("micrometre").CanBeAbbreviated("μ").SystemIs(Metric); }
            }

            public static Unit Nanometer
            {
                get { return new Unit("nanometer").IsAlsoCalled("nanometre").CanBeAbbreviated("n").SystemIs(Metric); }
            }

            public static Unit Picometer
            {
                get { return new Unit("picometer").IsAlsoCalled("picometre").CanBeAbbreviated("p").SystemIs(Metric); }
            }

            public static Unit Femtometer
            {
                get { return new Unit("femtometer").IsAlsoCalled("femtometre").CanBeAbbreviated("f").SystemIs(Metric); }
            }

            public static Unit Attometer
            {
                get { return new Unit("attometer").IsAlsoCalled("attometre").CanBeAbbreviated("a").SystemIs(Metric); }
            }

            public static Unit Zeptometer
            {
                get { return new Unit("zeptometer").IsAlsoCalled("zeptometre").CanBeAbbreviated("z").SystemIs(Metric); }
            }

            public static Unit Yoctometer
            {
                get { return new Unit("yoctometer").IsAlsoCalled("yoctometre").CanBeAbbreviated("y").SystemIs(Metric); }
            }

            // micro, nano, pico, femto, atto, zepto, yocto
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