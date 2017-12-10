using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySearch.Models.SuffixTree
{
    /// <summary>
    /// Trie Node
    /// </summary>
    internal class Node
    {
        /// <summary>
        /// Key for the current node
        /// </summary>
        public char Value { get; set; }
        /// <summary>
        /// Child nodes
        /// </summary>
        public List<Node> Children { get; set; }
        /// <summary>
        /// Trie node depth/level
        /// </summary>
        public int Depth { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">Trie Node Key/Value</param>
        /// <param name="depth">Trie node depth/level</param>
        public Node(char value, int depth)
        {
            Value = value;
            Children = new List<Node>();
            Depth = depth;
        }

        /// <summary>
        /// Is terminating node as contains no children
        /// </summary>
        /// <returns></returns>
        public bool IsTerminator()
        {
            return Value == '$';
        }

        /// <summary>
        /// Find child node by key/value
        /// </summary>
        /// <param name="key">child node key/value to find</param>
        /// <returns></returns>
        public Node FindChild(char key)
        {
            return Children.FirstOrDefault(child => child.Value == key);
        }
    }
}
