namespace Convertinator.Systems
{
    public class US
    {
        #region Nested type: Length

        public static class Length
        {
            public static readonly Unit Mile = new Unit("mile")
                .CanBeAbbreviated("mi");
            
            public static readonly Unit Foot = new Unit("foot");
        }

        #endregion

        #region Nested type: Temperature

        public static class Temperature
        {
            public static readonly Unit Fahrenheit = new Unit("degrees Fahrenheit")
                .IsAlsoCalled("Fahrenheit");
        }

        #endregion

        #region Nested type: Volume

        public static class Volume
        {
            public static readonly Unit Gallon = new Unit("gallon");
        }

        #endregion
    }
}