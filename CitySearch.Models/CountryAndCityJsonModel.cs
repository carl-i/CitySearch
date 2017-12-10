using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySearch.Models
{
    /// <summary>
    /// Represents the data I have stored to build a city store rather than using a dynamic var
    /// </summary>
    public class CountryAndCityJsonModel
    {
        //example format from text file
        //{"country": "Andorra", "geonameid": 3040051, "name": "les Escaldes", "subcountry": "Escaldes-Engordany"}
        public string Country { get; set; }
        public int GeoNameId { get; set; }
        public string Name { get; set; }
        public string SubCountry { get; set; }
    }
}
