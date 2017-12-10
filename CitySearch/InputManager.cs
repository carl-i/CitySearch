using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CitySearch
{
    /// <summary>
    /// Accept command line input, provide termination
    /// </summary>
    internal class InputManager
    {
        //Interface to be resolved using Unity container
        private ICityFinder _cityFinder;

        //Key to enter with control modifier to exit
        private readonly ConsoleKey _exit = ConsoleKey.X;

        /// <summary>
        /// Manages user input through the console
        /// </summary>
        /// <param name="CityFinder">City Finder interface instance</param>
        public InputManager(ICityFinder CityFinder)
        {
            _cityFinder = CityFinder;
        }

        /// <summary>
        /// User input from command line read
        /// </summary>
        public string Input = string.Empty;

        /// <summary>
        /// Read console input and act like a client calling a city search service
        /// </summary>
        public void ReadInput()
        {
            var blnContinue = true;
            while (blnContinue)
            {
                if (Input.Trim() == string.Empty)
                    Console.WriteLine($"Please enter a string to search, press Ctrl+{_exit} to exit. Press enter to complete string");
                var key = Console.ReadKey();
                Console.Write(Environment.NewLine);
                if (key.Modifiers == ConsoleModifiers.Control && key.Key == ConsoleKey.X)
                {
                    Console.WriteLine("Exit sequence entered, exiting");
                    blnContinue = false;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    //We have finished with the current typeahead search, starting a new one
                    Input = string.Empty;
                }
                else
                {
                    //Validate input, not supporting special characters other than space and dash
                    if (ValidateInput(Char.ToLower(key.KeyChar)))
                    {
                        Input += key.KeyChar;
                        //Do matches
                        var matches = _cityFinder.Search(Input.ToLower());
                        //Output some information 
                        Console.WriteLine($"Found {matches.NextCities.Count} matches");
                        Console.WriteLine($"Cities:{string.Join(",", matches.NextCities.Select(c => c))}");
                        Console.WriteLine($"Next Letters:{string.Join(",", matches.NextLetters.Select(c => c))}");
                        //Add an empty line to make this easier to read
                        Console.WriteLine("");
                    }
                    else
                    {
                        Console.WriteLine("Invalid character");
                    }
                }
                Console.Write($"{Input}");
            }
        }

        /// <summary>
        /// Validates input to allow a-z characters, spaces and dashes
        /// </summary>
        /// <returns>bool to indicate character is valid or not</returns>
        private bool ValidateInput(Char Key)
        {
            //Accept 1 or more characters followed by an optional space or dash and allow multiple combinations of this
            var regex = new Regex(@"^([a-z]+([\s]|[\-])?)+$");
            return regex.IsMatch(Input.ToLower() + Key);
        }
    }
}
