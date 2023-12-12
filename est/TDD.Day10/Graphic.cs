using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD.Day10
{
    public class Graphic
    {
        private readonly IDictionary<string, Vertex> vertexs = new Dictionary<string, Vertex>();
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
                    vertexs[Vertex.GenerateId(column, row)] = new Vertex()
                    {
                        Symbol = lines[row][column],
                        Row = row,
                        Column = column,
                    };
                    if (lines[row][column].Equals('S'))
                    {
                        startVertex = vertexs[Vertex.GenerateId(column, row)];
                    }
                }
            }

            foreach (var vertex in vertexs.Values)
            {
                vertex.InitializeEdges(vertexs);
            }
        }

        public void Write()
        {
            var neighbors = vertexs[Vertex.GenerateId(1, 1)].Edges.Select(x => x.To);

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            for (var row = 0; row <= vertexs.Values.Max(x => x.Row); row++)
            {
                for (var column = 0; column <= vertexs.Values.Max(x => x.Column); column++)
                {
                    var id = Vertex.GenerateId(row, column);
                    if (neighbors.Any(x => x.Equals(id)))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(vertexs[Vertex.GenerateId(row, column)].Symbol.ToString().PadLeft(4));
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.Write(vertexs[Vertex.GenerateId(row, column)].Symbol.ToString().PadLeft(4));
                    }

                }
                Console.WriteLine();
            }
        }
    }
}
