using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySearch.Models.SuffixTree
{
    public class CityResult : ICityResult
    {
        public ICollection<string> NextCities { get; set; }

        public ICollection<string> NextLetters { get; set; }
    }
}
