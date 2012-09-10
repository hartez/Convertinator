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
    }
}