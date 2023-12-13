using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TDD.Day10
{
    public class Graphic
    {
        private readonly IDictionary<string, Vertex> _vertexs = new Dictionary<string, Vertex>();
        private readonly DotZone dotZone = new DotZone();
        private Vertex startVertex;
        public Graphic(string inputData)
        {
            Initialize(inputData);
        }

        private void Initialize(string inputData)
        {
            var lines = inputData.Split("\r\n").Select(x => x.Trim()).Where(x => !string.IsNullOrEmpty(x)).ToArray();
            for (var row = 0; row < lines.Length; row++)
            {
                for (var column = 0; column < lines[row].Length; column++)
                {
                    _vertexs[Vertex.GenerateId(row, column)] = new Vertex()
                    {
                        Symbol = lines[row][column],
                        Row = row,
                        Column = column,
                    };
                    if (lines[row][column].Equals('S'))
                    {
                        startVertex = _vertexs[Vertex.GenerateId(row, column)];
                    }
                }
            }

            foreach (var vertex in _vertexs.Values)
            {
                vertex.InitializeEdges(_vertexs);
            }
        }

        public int GetAnswerOfPart1()
        {
            var linked = new Queue<Vertex>();

            linked.Enqueue(startVertex);
            while (linked.Any())
            {
                var temp = linked.Dequeue();

                var edges = temp.GetEdgesBySymbol(_vertexs).Where(x => !x.Visited);

                foreach (var edge in edges)
                {
                    edge.Visited = true;
                    if (_vertexs[edge.To].Distance > 0 || _vertexs[edge.To].Symbol.Equals('.'))
                        continue;

                    _vertexs[edge.To].Distance = _vertexs[edge.From].Distance + 1;
                    linked.Enqueue(_vertexs[edge.To]);
                }
            }

            return _vertexs.Values.Max(x => x.Distance);
        }

        public int GetAnswerOfPart2()
        {
            var vertexs = _vertexs.Where(x => x.Value.Symbol.Equals('.'))
                .Select(x => x.Value)
                .ToDictionary(x => x.Id);
            while (vertexs.Any())
            {
                var vertex = vertexs.OrderBy(x => x.Key).FirstOrDefault().Value;
                //var row = vertexs.Values.Min(x => x.Row);
                //var column = vertexs.Values.Min(x => x.Column);

                var adjacentDots = GetAdjacentDots(vertexs, vertex);

                if (adjacentDots.Any())
                {
                    dotZone.Matrix.Add(adjacentDots);
                }

                foreach (var item in adjacentDots)
                {
                    vertexs.Remove(item.Id);
                }
            }

            var answer = 0;
            foreach (var zone in dotZone.Matrix)
            {
                if (zone.Any(x => !x.Edges.Count.Equals(4)))
                    continue;
                if (dotZone.Closed(_vertexs, zone))
                {
                    answer += zone.Count;
                }
            }

            return answer;
        }
        private IList<Vertex> GetAdjacentDots(IDictionary<string, Vertex> vertexs, Vertex vertex)
        {
            var result = new List<Vertex>() { vertex };
            var linked = new Queue<Vertex>();
            linked.Enqueue(vertex);

            while (linked.Any())
            {
                var temp = linked.Dequeue();
                foreach (var adjacent in temp.Edges.Where(x => (x.Direction == Direction.East || x.Direction == Direction.South)
                && vertexs.ContainsKey(x.To) && vertexs[x.To].Symbol.Equals('.'))
                    .Select(x => vertexs[x.To]))
                {
                    result.Add(adjacent);
                    linked.Enqueue(adjacent);
                }
            }
            return result.Distinct(VertexEqualityComparer.Instance).ToList();
        }

        public void Write()
        {
            var neighbors = startVertex.Edges.Select(x => x.To);
            Console.ForegroundColor = ConsoleColor.White;
            for (var row = 0; row <= _vertexs.Values.Max(x => x.Row); row++)
            {
                for (var column = 0; column <= _vertexs.Values.Max(x => x.Column); column++)
                {
                    Console.ForegroundColor = dotZone.GetConsoleColor(_vertexs[Vertex.GenerateId(row, column)]);
                    Console.Write(_vertexs[Vertex.GenerateId(row, column)].Symbol.ToString().PadLeft(4));
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.WriteLine();
            }
        }

        public void Write2()
        {
            foreach (var vertex in _vertexs.Values)
            {
                Console.WriteLine($"{vertex.Id} -> {string.Join(", ", vertex.GetEdgesBySymbol(_vertexs).Select(x => $"{x.Direction}; From:{x.From} To:{x.To}"))}");
            }
        }

    }
}
