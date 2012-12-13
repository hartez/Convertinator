using System.Collections.Generic;
using System.Linq;

namespace Convertinator
{
    public class Unit
    {
        private readonly List<string> _abbreviations;
        private readonly List<string> _aliases;
        private string _displayAbbreviation;
        private string _displayName;
        private string _pluralFormat;
        private string _explicitPlural;

        private readonly List<Unit> _counterparts; 
        
        public Unit(string name)
        {
            Name = name;
            _displayName = name;
            _aliases = new List<string>();
            _abbreviations = new List<string>();
            _counterparts = new List<Unit>();
        }

        public string Name { get; set; }

        public string System { get; set; }

        public IEnumerable<string> Aliases
        {
            get { return _aliases.AsEnumerable(); }
        }

        public IEnumerable<string> Abbreviations
        {
            get { return _abbreviations.AsEnumerable(); }
        }

        public IEnumerable<Unit> Counterparts
        {
            get { return _counterparts; }
        }

        public Unit CanBeAbbreviated(string abbreviation, params string[] otherAbbreviations)
        {
            _abbreviations.Add(abbreviation);
            if(otherAbbreviations.Length > 0)
            {
                _abbreviations.AddRange(otherAbbreviations);
            }
            return this;
        }

        public Unit IsAlsoCalled(string alternateName, params string[] otherNames)
        {
            _aliases.Add(alternateName);
            if(otherNames.Length > 0)
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

        public Unit UsePluralFormat(string format)
        {
            _pluralFormat = format;
            return this;
        }

        public Unit PluralizeAs(string plural)
        {
            _explicitPlural = plural;
            return this;
        }

        public Unit HasCounterPart(Unit unit)
        {
            _counterparts.Add(unit);

            // Add the reverse counterpart as well
            if (!unit.Counterparts.Contains(this))
            {
                unit.HasCounterPart(this);
            }

            return this;
        }

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

        public string ToPluralString()
        {
            if(!string.IsNullOrEmpty(_explicitPlural))
            {
                return _explicitPlural;
            }

            if(!string.IsNullOrEmpty(_pluralFormat))
            {
                return string.Format(_pluralFormat, ToString());
            }

            return ToString();
        }

        public bool Matches(string name)
        {
            if(Name == name)
            {
                return true;
            }

            if(name == _displayAbbreviation)
            {
                return true;
            }

            if(name == _displayName)
            {
                return true;
            }

            if(name == ToPluralString())
            {
                return true;
            }

            return _aliases.Concat(_abbreviations).Contains(name);
        }

        public Unit SystemIs(string system)
        {
            System = system;
            return this;
        }
    }
}