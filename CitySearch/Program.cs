using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace CitySearch
{
    /// <summary>
    /// Console city search application
    /// </summary>
    class Program
    {
        //Good working example found at https://stackoverflow.com/questions/45509117/fastest-starts-with-search-algorithm although this includes deletion functionality, 
        //and could possibly return non-terminated values if building/deletion was incorrectly carried out
        //also the yield return does not offer benefit to ICollection in interface result

        static void Main(string[] args)
        {
            Console.WriteLine("Initialising");

            //Set up dependency injection for icity finder
            var container = new UnityContainer();
            container.RegisterType<ICityFinder, CitySearch.Models.SuffixTree.CityFinder>();
            Console.WriteLine("Dependency Injection initialised");

            //Initialise our cities
            var cityReader = new CityFileJsonReader();
            var cityList = cityReader.ReadCities("CitySearch.Data.world-cities_json.json.txt");
            Console.WriteLine("City store initialised");

            //Resolving the city finder instead of the input manager as there is a need to populate the cities through the instance rather than create singleton
            var cityFinder = container.Resolve<ICityFinder>();
            cityFinder.Populate(cityList);
            Console.WriteLine("Initialised");

            var inputManager = new InputManager(cityFinder);
            //Loop to read input until exit character is entered
            inputManager.ReadInput();
            Console.WriteLine("Exited");
        }
    }
}
