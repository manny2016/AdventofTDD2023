using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TDD.Day10
{
    public class DotArea
    {
        public IList<Vertex> Dots { get; set; }

        public IList<Vertex> ClosedPipes { get; set; }

        public bool IsValid()
        {
            return Dots.All(x => x.Edges.Count.Equals(4));
        }

        public bool Contains(Vertex vertex)
        {
            return Dots.Any(x => x.Id.Equals(vertex.Id));
        }

        public bool IsClosedPipe(Vertex vertex)
        {
            if (vertex.Symbol.Equals('.')) return false;
            return ClosedPipes.Any(x => x.Id.Equals(vertex.Id));
        }
        private void Reset()
        {
            foreach (var vertex in Dots)
                vertex.Visited = false;

            foreach (var vertex in ClosedPipes)
                vertex.Visited = false;
        }
        public bool Enclosed()
        {
            Reset();
            var dict = Dots.Concat(ClosedPipes).ToDictionary(x => x.Id);
            return ClosedPipes.Any(x => x.Edges.Any(y => dict.ContainsKey(y.To)));
        }
    }
}
