using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySearch.Models.SuffixTree
{
    /// <summary>
    /// radix/prefix trie
    /// </summary>
    internal class Trie
    {
        /// <summary>
        /// Root of all nodes, key is not used this node is used to call search methods for all child nodes
        /// </summary>
        readonly Node _root;

        public Trie()
        {
            //Instantiate root node so we have an instance to search child keys 
            _root = new Node(' ', 0);
        }

        /// <summary>
        /// Find node that matches the provided prefix, although not recursive itself implements child find and will return closest key/node if a child key is not found
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public Node FindNodeByPrefix(string s)
        {
            //start from root node
            var currentNode = _root;
            var result = currentNode;

            foreach (var c in s)
            {
                //find the next child using the next char in the prefix
                currentNode = currentNode.FindChild(c);

                if (currentNode == null)
                    //no matching child so break loop and return latest key in search string if any
                    break;

                result = currentNode;
            }

            return result;
        }

        /// <summary>
        /// Loop all cities to add individually
        /// </summary>
        /// <param name="items"></param>
        public void AddCities(IEnumerable<string> items)
        {
            foreach (string item in items)
                AddCity(item);
        }

        /// <summary>
        /// Add individual city by looping char at a time and creating child keys where not existing until complete depth exists in trie
        /// </summary>
        /// <param name="cityName">Name of city</param>
        public void AddCity(string cityName)
        {
            var commonPrefix = FindNodeByPrefix(cityName);
            var current = commonPrefix;

            //current.depth will be latest character in search string that already exists
            for (var i = current.Depth; i < cityName.Length; i++)
            {
                var newNode = new Node(cityName[i], current.Depth + 1);
                current.Children.Add(newNode);
                current = newNode;
            }
            //Add terminating node
            current.Children.Add(new Node('$', current.Depth + 1));
        }
    }
}
