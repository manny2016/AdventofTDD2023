using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection;
using System.Reflection.Metadata;
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

        private IDictionary<string, Vertex> passing = new Dictionary<string, Vertex>();
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
            var queue = new Queue<Vertex>();
            queue.Enqueue(startVertex);

            while (queue.Any())
            {
                var temp = queue.Dequeue();

                var edges = temp.GetEdgesBySymbol(_vertexs).Where(x => !x.Visited);

                foreach (var edge in edges)
                {
                    edge.Visited = true;
                    if (_vertexs[edge.To].Distance > 0
                        || _vertexs[edge.To].Symbol.Equals('.')
                        || _vertexs[edge.To].Symbol.Equals('S'))
                        continue;

                    _vertexs[edge.To].Distance = _vertexs[edge.From].Distance + 1;
                    _vertexs[edge.To].Flag = true;
                    passing[edge.To] = _vertexs[edge.To];
                    //Insert(edge.From, _vertexs[edge.To]);
                    queue.Enqueue(_vertexs[edge.To]);
                }
            }

            return _vertexs.Values.Max(x => x.Distance);
        }

        //public void Insert(string from, Vertex vertex)
        //{
        //    var previous = paths.FirstOrDefault(x => x.Last.Value.Id.Equals(from));
        //    if (previous == null)
        //    {
        //        previous = new LinkedList<Vertex>();
        //        paths.Add(previous);
        //    }
        //    previous.AddLast(vertex);
        //}
        public int GetAnswerOfPart2()
        {
            var largest = GetAnswerOfPart1();            
            var answer = 0;
            _dotareas = InitializeDotArea();
            foreach (var dotArea in _dotareas)
            {
                if (!dotArea.IsValid()) continue;
                if (dotArea.ClosedPipes.All(x => passing.ContainsKey(x.Id)))
                {
                    answer += dotArea.Dots.Count;
                }
            }            

            return answer;
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
            //for (var row = 0; row <= _vertexs.Values.Max(x => x.Row); row++)
            //{
            //    for (var column = 0; column <= _vertexs.Values.Max(x => x.Column); column++)
            //    {
            //        var vertex = _vertexs[Vertex.GenerateId(row, column)];
            //        Console.ForegroundColor = GetColor(vertex);
            //        Console.Write(vertex.Symbol.ToString().PadRight(4));
            //    }
            //    Console.WriteLine();
            //}
        }

        //private ConsoleColor GetColor(Vertex vertex)
        //{
        //    var colors = new ConsoleColor[] {
        //        ConsoleColor.Red,
        //        ConsoleColor.Green
        //    };
        //    for (var i = 0; i < paths.Count; i++)
        //    {
        //        if (paths[i].Contains(vertex))
        //        {
        //            return colors[i % paths.Count];
        //        }
        //    }
        //    return ConsoleColor.White;
        //}
    }
}
