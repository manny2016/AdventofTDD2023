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
        private IList<DotArea> _dotareas = new List<DotArea>();
        private readonly ConsoleColor[] colors = new ConsoleColor[] {
             ConsoleColor.Green,
             ConsoleColor.Blue,
             ConsoleColor.Yellow
        };
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
                    if (_vertexs[edge.To].Distance > 0
                        || _vertexs[edge.To].Symbol.Equals('.')
                        || _vertexs[edge.To].Symbol.Equals('S'))
                        continue;

                    _vertexs[edge.To].Distance = _vertexs[edge.From].Distance + 1;
                    linked.Enqueue(_vertexs[edge.To]);
                }
            }

            return _vertexs.Values.Max(x => x.Distance);
        }

        public int GetAnswerOfPart2()
        {
            var largest = GetAnswerOfPart1();
            var current = _vertexs.SingleOrDefault(x => x.Value.Distance.Equals(largest)).Value;
            
            while (!current.Distance.Equals(0))
            {
                var exceptEdge = current.Edges.FirstOrDefault(x => _vertexs[x.To].Distance == current.Distance - 1);
                current.Flag = true;
                current = _vertexs[exceptEdge.To];                
            }
            //_dotareas = InitializeDotArea();
            //var answer = 0;
            //foreach (var area in _dotareas)
            //{
            //    if (!area.IsValid() || !area.Enclosed()) continue;
            //    answer += area.Dots.Count;
            //}

            return 0;
        }

        private IList<DotArea> InitializeDotArea()
        {
            var result = new List<DotArea>();
            var alldots = _vertexs.Where(x => x.Value.Symbol.Equals('.'))
                .Select(x => x.Value)
                .ToDictionary(x => x.Id);

            while (alldots.Any())
            {
                var vertex = alldots.OrderBy(x => x.Key).FirstOrDefault().Value;

                var adjacentDots = GetAdjacentDots(alldots, vertex);

                if (adjacentDots.Any())
                {
                    result.Add(new DotArea()
                    {
                        Dots = adjacentDots,
                        ClosedPipes = adjacentDots.SelectMany(x => x.Edges)
                        .Where(x => !_vertexs[x.To].Symbol.Equals('.'))
                        .Select(x => _vertexs[x.To])
                        .Distinct(VertexEqualityComparer.Instance)
                        .ToList()
                    });
                }

                foreach (var item in adjacentDots)
                {
                    alldots.Remove(item.Id);
                }
            }
            return result;
        }


        private IList<Vertex> GetAdjacentDots(IDictionary<string, Vertex> vertexs, Vertex startVertex)
        {
            var result = new List<Vertex>() { startVertex };
            var linked = new Queue<Vertex>();
            linked.Enqueue(startVertex);
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
            Console.ForegroundColor = ConsoleColor.White;
            for (var row = 0; row <= _vertexs.Values.Max(x => x.Row); row++)
            {
                for (var column = 0; column <= _vertexs.Values.Max(x => x.Column); column++)
                {
                    Console.ForegroundColor = GetConsoleColor(_vertexs[Vertex.GenerateId(row, column)]);
                    Console.Write(_vertexs[Vertex.GenerateId(row, column)].Symbol.ToString().PadLeft(4));
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        private ConsoleColor GetConsoleColor(Vertex vertex)
        {
            for (var i = 0; i < _dotareas.Count; i++)
            {
                if (_dotareas[i].Contains(vertex) && _dotareas[i].IsValid())
                {
                    return colors[i % colors.Length];
                }
                if (_dotareas[i].IsClosedPipe(vertex) && _dotareas[i].IsValid())
                {
                    return ConsoleColor.DarkRed;
                }
            }

            return ConsoleColor.White;
        }

        public void Write1()
        {
            for (var row = 0; row <= _vertexs.Values.Max(x => x.Row); row++)
            {
                for (var column = 0; column <= _vertexs.Values.Max(x => x.Column); column++)
                {
                    var vertex = _vertexs[Vertex.GenerateId(row, column)];
                    Console.ForegroundColor = vertex.Flag ? ConsoleColor.Red : ConsoleColor.White;
                }
                Console.WriteLine();
            }
        }
    }
}
