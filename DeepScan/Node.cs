using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepScan
{
    [DebuggerDisplay("Info = {Name}")]
    public class Node
    {
        public string Name { get; set; }
        public List<Node> Children { get; set; }

        public Node(string name)
        {
            Name = name;
            Children = new List<Node>();
        }
    }
}
