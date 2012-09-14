namespace Convertinator
{
    public class Measurement
    {
        public Measurement(Unit unit, decimal value)
        {
            Unit = unit;
            Value = value;
        }

        public Measurement(string unit, decimal value)
        {
            Unit = new Unit(unit);
            Value = value;
        }

        public Unit Unit { get; set; }
        public decimal Value { get; set; }

        public string ToAdjectiveString()
        {
            return string.Format("{0}-{1}", Value, Unit);
        }

        public override string ToString()
        {
            if(Value != 1M)
            {
                return string.Format("{0} {1}", Value, Unit.ToPluralString());
            }

            return string.Format("{0} {1}", Value, Unit);
        }

        public string ToAbbreviatedString()
        {
            return string.Format("{0} {1}", Value, Unit.ToAbbreviatedString());
        }
    }
}