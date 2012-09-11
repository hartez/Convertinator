namespace Convertinator.Systems
{
    public static class SI
    {
        #region Nested type: Length

        public static class Length
        {
            public static Unit Meter = new Unit("meter")
                .IsAlsoCalled("metre")
                .CanBeAbbreviated("m");

            public static readonly Unit Kilometer = new Unit("kilometer")
                .IsAlsoCalled("kilometre")
                .CanBeAbbreviated("km");
        }

        #endregion

        #region Nested type: Temperature

        public static class Temperature
        {
            public static readonly Unit Kelvin = new Unit("kelvin");
            public static readonly Unit Celcius = new Unit("degrees Celcius")
                .IsAlsoCalled("Celcius");
        }

        #endregion

        public static class Volume
        {
            public static readonly Unit Liter = new Unit("liter");
        }
    }
}

