using System;

namespace Convertinator.Systems
{
    public static class SI
    {
        private const string Metric = "metric";

        #region Nested type: Length

        public static class Length
        {
            private static readonly Lazy<Unit> _kilometer = new Lazy<Unit>(() => new Unit("kilometer").IsAlsoCalled("kilometre").CanBeAbbreviated("km").SystemIs(Metric));
            private static readonly Lazy<Unit> _meter = new Lazy<Unit>(() => new Unit("meter").IsAlsoCalled("metre").CanBeAbbreviated("m").SystemIs(Metric));
            private static readonly Lazy<Unit> _decimeter = new Lazy<Unit>(() => new Unit("decimeter").IsAlsoCalled("decimetre").CanBeAbbreviated("dm").SystemIs(Metric));
            private static readonly Lazy<Unit> _centimeter = new Lazy<Unit>(() => new Unit("centimeter").IsAlsoCalled("centimetre").CanBeAbbreviated("cm").SystemIs(Metric));
            private static readonly Lazy<Unit> _millimeter = new Lazy<Unit>(() => new Unit("millimeter").IsAlsoCalled("millimetre").CanBeAbbreviated("mm").SystemIs(Metric));
            private static readonly Lazy<Unit> _micrometer = new Lazy<Unit>(() => new Unit("micrometer").IsAlsoCalled("micrometre").CanBeAbbreviated("μ").SystemIs(Metric));
            private static readonly Lazy<Unit> _nanometer = new Lazy<Unit>(() => new Unit("nanometer").IsAlsoCalled("nanometre").CanBeAbbreviated("n").SystemIs(Metric));
            private static readonly Lazy<Unit> _picometer = new Lazy<Unit>(() => new Unit("picometer").IsAlsoCalled("picometre").CanBeAbbreviated("p").SystemIs(Metric));
            private static readonly Lazy<Unit> _femtometer = new Lazy<Unit>(() => new Unit("femtometer").IsAlsoCalled("femtometre").CanBeAbbreviated("f").SystemIs(Metric));
            private static readonly Lazy<Unit> _attometer = new Lazy<Unit>(() => new Unit("attometer").IsAlsoCalled("attometre").CanBeAbbreviated("a").SystemIs(Metric));
            private static readonly Lazy<Unit> _zeptometer = new Lazy<Unit>(() => new Unit("zeptometer").IsAlsoCalled("zeptometre").CanBeAbbreviated("z").SystemIs(Metric));
            private static readonly Lazy<Unit> _yoctometer = new Lazy<Unit>(() => new Unit("yoctometer").IsAlsoCalled("yoctometre").CanBeAbbreviated("y").SystemIs(Metric));

            public static Unit Kilometer
            {
                get { return _kilometer.Value; }
            }

            public static Unit Meter
            {
                get { return _meter.Value; }
            }

            public static Unit Decimeter
            {
                get { return _decimeter.Value; }
            }

            public static Unit Centimeter
            {
                get { return _centimeter.Value; }
            }

            public static Unit Millimeter
            {
                get { return _millimeter.Value; }
            }

            public static Unit Micrometer
            {
                get { return _micrometer.Value; }
            }

            public static Unit Nanometer
            {
                get { return _nanometer.Value; }
            }

            public static Unit Picometer
            {
                get { return _picometer.Value; }
            }

            public static Unit Femtometer
            {
                get { return _femtometer.Value; }
            }

            public static Unit Attometer
            {
                get { return _attometer.Value; }
            }

            public static Unit Zeptometer
            {
                get { return _zeptometer.Value; }
            }

            public static Unit Yoctometer
            {
                get { return _yoctometer.Value; }
            }
        }

        #endregion

        #region Nested type: Temperature

        public static class Temperature
        {
            private static readonly Lazy<Unit> _celcius = new Lazy<Unit>(() => new Unit("degrees Celcius").IsAlsoCalled("Celcius"));
            
            private static readonly Lazy<Unit> _kelvin= new Lazy<Unit>(() => new Unit("kelvin"));

            public static Unit Celcius
            {
                get { return _celcius.Value; }
            }

            public static Unit Kelvin
            {
                get { return _kelvin.Value; }
            }
        }

        #endregion

        #region Nested type: Time

        /// <summary>
        ///     For now, just handling time measured in seconds, minutes, hours.
        ///     Things like metric time and leap seconds can wait.
        /// </summary>
        public static class Time
        {
            private static readonly Lazy<Unit> _second = new Lazy<Unit>(() => new Unit("second").CanBeAbbreviated("s", "sec"));
            private static readonly Lazy<Unit> _minute = new Lazy<Unit>(() => new Unit("minute").CanBeAbbreviated("min"));
            private static readonly Lazy<Unit> _hour = new Lazy<Unit>(() => new Unit("hour").CanBeAbbreviated("h", "hr"));

            public static Unit Second
            {
                get { return _second.Value; }
            }

            public static Unit Minute
            {
                get { return _minute.Value; }
            }

            public static Unit Hour
            {
                get { return _hour.Value; }
            }
        }

        #endregion

        #region Nested type: Volume

        public static class Volume
        {
            public static Unit Liter
            {
                get { return _liter.Value; }
            }

            private static readonly Lazy<Unit> _liter = new Lazy<Unit>(() => new Unit("liter").IsAlsoCalled("litre").CanBeAbbreviated("l", "L"));
        }

        #endregion
    }
}