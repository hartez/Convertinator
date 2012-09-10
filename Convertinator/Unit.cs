using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Convertinator
{
    public class Unit
    {
        public Unit(string name)
        {
            Name = name;
            _aliases = new List<string>();
        }

        public Unit CanBeAbbreviated(string abbreviation, params string[] otherAbbreviations)
        {
            IsAlsoCalled(abbreviation, otherAbbreviations);
            return this;
        }

        public Unit IsAlsoCalled(string alternateName, params string[] otherNames)
        {
            _aliases.Add(alternateName);
            if (otherNames.Length > 0)
            {
                _aliases.AddRange(otherNames);
            }
            return this;
        }

        public string Name { get; set; }
        private readonly List<string> _aliases;
        public IEnumerable<string> Aliases { get { return _aliases.AsEnumerable(); } }

        public override string ToString()
        {
            return string.Format("{0}", Name);
        }

        public bool Matches(string name)
        {
            if(Name == name)
            {
                return true;
            }

            return _aliases.Contains(name);
        }
    }
}
