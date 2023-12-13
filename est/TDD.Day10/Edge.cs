using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD.Day10
{
    public class Edge
    {
        public string From { get; set; }

        public Direction Direction { get; set; }

        public string To { get; set; }

        public bool Visited { get; set; }
    }
}
