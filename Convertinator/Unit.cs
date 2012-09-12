using System.Collections.Generic;
using System.Linq;

namespace Convertinator
{
    public class Unit
    {
        public Unit(string name)
        {
            Name = name;
            _displayName = name;
            _aliases = new List<string>();
            _abbreviations = new List<string>();
        }

        public Unit CanBeAbbreviated(string abbreviation, params string[] otherAbbreviations)
        {
            _abbreviations.Add(abbreviation);
            if (otherAbbreviations.Length > 0)
            {
                _abbreviations.AddRange(otherAbbreviations);
            }
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

        public Unit DisplayWithName(string displayName)
        {
            _displayName = displayName;
            return this;
        }

        public Unit DisplayWithAbbreviation(string displayAbbreviation)
        {
            _displayAbbreviation = displayAbbreviation;
            return this;
        }

        public string Name { get; set; }
        private string _displayName;
        private string _displayAbbreviation;

        private readonly List<string> _aliases;
        public IEnumerable<string> Aliases { get { return _aliases.AsEnumerable(); } }

        private readonly List<string> _abbreviations;
        public IEnumerable<string> Abbreviations { get { return _abbreviations.AsEnumerable(); } }

        public override string ToString()
        {
            return string.Format("{0}", _displayName);
        }

        public string ToAbbreviatedString()
        {
            if(!string.IsNullOrEmpty(_displayAbbreviation))
            {
                return string.Format("{0}", _displayAbbreviation);
            }

            if(_abbreviations.Count > 0)
            {
                return string.Format("{0}", _abbreviations[0]);
            }

            return ToString();
        }

        public bool Matches(string name)
        {
            if(Name == name)
            {
                return true;
            }

            return _aliases.Concat(_abbreviations).Contains(name);
        }
    }
}
