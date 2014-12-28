namespace Convertinator
{
    public class Measurement : Measurement<decimal>
    {
        public Measurement(Unit unit, decimal value) : base(unit, value)
        {
        }

        public Measurement(string unit, decimal value) : base(unit, value)
        {
        }
    }

    public class Measurement<T>
    {
        public Measurement(Unit unit, T value)
        {
            Unit = unit;
            Value = value;
        }

        public Measurement(string unit, T value)
        {
            Unit = new Unit(unit);
            Value = value;
        }

        public Unit Unit { get; set; }
        public T Value { get; set; }

        public string ToAdjectiveString()
        {
            return string.Format("{0}-{1}", Value, Unit);
        }

        public override string ToString()
        {
            if(IsOne(Value))
            {
                return string.Format("{0} {1}", Value, Unit);
            }

            return string.Format("{0} {1}", Value, Unit.ToPluralString());
        }

        public string ToAbbreviatedString()
        {
            return string.Format("{0} {1}", Value, Unit.ToAbbreviatedString());
        }

        private bool IsOne(dynamic value)
        {
            return value == 1;
        }
    }
}