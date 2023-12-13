using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TDD.Day10
{
    public class Graphic
    {
        private readonly IDictionary<string, Vertex> _vertexs = new Dictionary<string, Vertex>();
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


        public void BFS()
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

            var maxSteps = _vertexs.Values.Max(x => x.Distance);
        }


        public void Write()
        {
            var neighbors = startVertex.Edges.Select(x => x.To);

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            for (var row = 0; row <= _vertexs.Values.Max(x => x.Row); row++)
            {
                for (var column = 0; column <= _vertexs.Values.Max(x => x.Column); column++)
                {
                    var id = Vertex.GenerateId(row, column);
                    if (_vertexs[Vertex.GenerateId(row, column)].Symbol == '.')
                        Console.Write(_vertexs[Vertex.GenerateId(row, column)].Symbol.ToString().PadLeft(4));
                    else
                        Console.Write(_vertexs[Vertex.GenerateId(row, column)].Distance.ToString().PadLeft(4));

                    //if (neighbors.Any(x => x.Equals(id)))
                    //{
                    //    Console.ForegroundColor = ConsoleColor.Red;
                    //    Console.Write(_vertexs[Vertex.GenerateId(row, column)].Symbol.ToString().PadLeft(4));
                    //    Console.ForegroundColor = ConsoleColor.White;
                    //}
                    //else
                    //{
                    //    //Console.Write(_vertexs[Vertex.GenerateId(row, column)].Symbol.ToString().PadLeft(4));
                    //    if (_vertexs[Vertex.GenerateId(row, column)].Symbol == '.')
                    //        Console.Write(_vertexs[Vertex.GenerateId(row, column)].Symbol.ToString().PadLeft(4));
                    //    else
                    //        Console.Write(_vertexs[Vertex.GenerateId(row, column)].Distance.ToString().PadLeft(4));
                    //}

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
