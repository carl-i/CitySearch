using CitySearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySearch.Models.SuffixTree
{
    /// <summary>
    /// ICity Finder implementation using a tree implementation (https://en.wikipedia.org/wiki/Trie#Dictionary_representation) 
    /// </summary>
    public class CityFinder : ICityFinder
    {
        //Search tree
        private Trie _cityTrie = null;

        /// <summary>
        /// Populate all cities into the city trie implementation
        /// </summary>
        /// <param name="Cities">List of city strings</param>
        public void Populate(ICollection<string> Cities)
        {
            _cityTrie = new Trie();
            _cityTrie.AddCities(Cities);
        }

        /// <summary>
        /// Find all cities starting with the current search string
        /// </summary>
        /// <param name="searchString">String to search for</param>
        /// <returns>ICity result collection of matching cities and all letters that can follow the current search string</returns>
        public ICityResult Search(string searchString)
        {
            //Find the node for the current token
            var node = _cityTrie.FindNodeByPrefix(searchString);
            ICollection<string> allCities = new List<string>();
            ICollection<string> nextLetters = new List<string>();

            if (node.Depth == searchString.Length)
            {
                //have to traverse all child nodes concatening values to build list of cities
                allCities = getCitiesByNode(node, searchString);
                //Only returning the next letter so get the char values of all child nodes of this one
                nextLetters = node.Children.Where(c => !c.IsTerminator()).Select(c => c.Value.ToString()).ToList();
            }
            return new CityResult
            {
                NextCities = allCities,
                NextLetters = nextLetters
            };
        }

        /// <summary>
        /// Returns a list of cities by traversing child nodes and concatening char values
        /// </summary>
        /// <param name="Parent">Root node of the current prefix (search string)</param>
        /// <param name="Prefix">Current search string to prepend to results</param>
        /// <returns></returns>
        static ICollection<string> getCitiesByNode(Node Parent, string Prefix)
        {
            //string builder used for performance of concatanation and removing single character when returning from child node, also passing by ref wouldn't be 
            var sb = new StringBuilder();
            return getChildCities(Parent, sb).Select(citySuffix => Prefix + citySuffix.TrimEnd('$')).ToList();
        }

        /// <summary>
        /// Returns a list of cities by recursively traversing child nodes and concatening char values
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="current"></param>
        /// <returns></returns>
        static ICollection<string> getChildCities(Node parent, StringBuilder current)
        {
            List<string> matchedCities = new List<string>();
            if (parent.IsTerminator())
            {
                //We've reached a leaf node meaning this is a terminating value so return the current string builder
                matchedCities.Add(current.ToString());
            }
            else
            {
                foreach (var child in parent.Children)
                {
                    //Append the current value to the string token
                    current.Append(child.Value);

                    matchedCities.AddRange(getChildCities(child, current));

                    //remove child value as we may be traversing other children and the current string is a prefix of all children
                    --current.Length;
                }
            }
            return matchedCities;
        }
    }
}
