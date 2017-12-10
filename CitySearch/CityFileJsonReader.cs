using CitySearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySearch
{
    /// <summary>
    /// Reads cities from an embedded resource file and returns as an ordered string collection
    /// </summary>
    internal class CityFileJsonReader
    {
        /// <summary>
        /// Read cities in a Json format returning only the city name
        /// </summary>
        /// <param name="File"></param>
        /// <returns></returns>
        public ICollection<string> ReadCities(string Resource)
        {
            var jsonText = GetTextFromAssemblyResource(Resource);
            var jsonCities = ParseCities(jsonText);
            //we have cities that are duplicated so must be located in more than one country for example but as we are only showing city info lets distinct the list. Real scenario would require a tooltip or more info in the UI
            return jsonCities.Select(city => city.Name.ToLower()).Distinct().OrderBy(city => city).ToList();
        }

        private string GetTextFromAssemblyResource(string Resource)
        {
            //Read file from the assembly so there are no hard coded paths
            var resourceFile = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(Resource);
            var reader = new System.IO.StreamReader(resourceFile);
            return reader.ReadToEnd();
        }

        private ICollection<CountryAndCityJsonModel> ParseCities(string JsonText)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ICollection<CountryAndCityJsonModel>>(JsonText);
        }
    }
}
