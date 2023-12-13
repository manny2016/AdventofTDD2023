using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TDD.Day10
{
    public class DotZone
    {
        public IList<IList<Vertex>> Matrix { get; set; } = new List<IList<Vertex>>();

        public ConsoleColor[] colors = new ConsoleColor[] {
             ConsoleColor.Red,
             ConsoleColor.Green,
             ConsoleColor.Blue,
             ConsoleColor.Yellow,
             ConsoleColor.DarkBlue,
             ConsoleColor.Red,
             ConsoleColor.Green,
             ConsoleColor.Blue,
             ConsoleColor.Yellow,
             ConsoleColor.DarkBlue,
        };

        public ConsoleColor GetConsoleColor(Vertex vertex)
        {
            for (var i = 1; i <= Matrix.Count; i++)
            {
                for (var j = 1; j <= Matrix[i - 1].Count; j++)
                {
                    if (Matrix[i - 1][j - 1].Id == vertex.Id)
                    {
                        var index = ((colors.Length) % i);
                        if (index >= colors.Length) index = colors.Length - 1;

                        return colors[index];
                    }
                }
            }
            return ConsoleColor.White;
        }

        public bool Closed(IDictionary<string, Vertex> vertexs, IList<Vertex> matrix)
        {
            var edgeVertexs = matrix.Where(x => IsEdgeIsEdgeVertex(vertexs, x))
                .SelectMany(x => x.Edges)
                .Where(x => !vertexs[x.To].Symbol.Equals('.'))
                .Select(x => vertexs[x.To])
                .Distinct(VertexEqualityComparer.Instance)
                .ToDictionary(x => x.Id);


            var queue = new Queue<Vertex>();
            var start = edgeVertexs.FirstOrDefault().Value;
            queue.Enqueue(start);
            while (queue.Any())
            {
                var temp = queue.Dequeue();
                edgeVertexs.Remove(temp.Id);
                var edges = temp.GetEdgesBySymbol(vertexs);
                if (!edges.Any(x => vertexs[x.To].Id.Equals(start.Id)))
                {
                    return true;
                }
                foreach (var edge in edges.Where(x => edgeVertexs.ContainsKey(x.To)))
                {
                    queue.Enqueue(edgeVertexs[edge.To]);
                }
            }
            //

            return false;
        }

        public bool IsEdgeIsEdgeVertex(IDictionary<string, Vertex> vertexs, Vertex vertex)
        {
            return vertex.Edges.Any(x => !vertexs[x.To].Symbol.Equals('.'));
        }
    }
}
