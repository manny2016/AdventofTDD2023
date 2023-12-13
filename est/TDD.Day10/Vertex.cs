using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD.Day10
{
    public class Vertex
    {
        public string Id => GenerateId(Row, Column);

        public IList<Edge> Edges = new List<Edge>();

        public char Symbol { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public int Distance { get; set; }

        public static string GenerateId(int row, int column) => $"{row}:{column}";

        public void InitializeEdges(IDictionary<string, Vertex> vertexs)
        {
            Edges.Clear();

            foreach (var neighbor in new Edge[]
            {
                new Edge(){ From= Id, To = GenerateId(Row-1, Column), Direction =  Direction.North},//UP
                new Edge(){ From= Id, To = GenerateId(Row+1, Column), Direction =  Direction.South},//Down
                new Edge(){ From =Id, To = GenerateId(Row, Column + 1), Direction = Direction.East},//Right
                new Edge(){ From =Id, To = GenerateId(Row, Column - 1), Direction = Direction.West},//West Left                
            })
            {
                if (vertexs.ContainsKey(neighbor.To))
                    Edges.Add(neighbor);
            }
        }

        public List<Edge> GetEdgesBySymbol(IDictionary<string, Vertex> vertexs)
        {
            switch (Symbol)
            {
                case 'S':
                    var result = new List<Edge>();
                    foreach (var edge in Edges)
                    {
                        var to = vertexs[edge.To].GetEdgesBySymbol(vertexs).SingleOrDefault((x => x.To.Equals(Id)));
                        if (to != null)
                        {
                            result.Add(edge);
                        }
                    }
                    return result;
                case '|':
                    var north_south = new Direction[] { Direction.North, Direction.South };
                    return Edges.Where(x => north_south.Contains(x.Direction))
                        .ToList();
                case '-':
                    var east_west = new Direction[] { Direction.East, Direction.West };
                    return Edges.Where(x => east_west.Contains(x.Direction))
                        .ToList();
                case 'L':
                    var north_east = new Direction[] { Direction.North, Direction.East };
                    return Edges.Where(x => north_east.Contains(x.Direction))
                        .ToList();
                case 'J'://connecting north and west.
                    var north_west = new Direction[] { Direction.North, Direction.West };
                    return Edges.Where(x => north_west.Contains(x.Direction))
                        .ToList();
                case '7':
                    //connecting south and west.
                    var south_west = new Direction[] { Direction.South, Direction.West };
                    return Edges.Where(x => south_west.Contains(x.Direction))
                        .ToList();
                case 'F'://connecting south and east.
                    var south_east = new Direction[] { Direction.South, Direction.East };
                    return Edges.Where(x => south_east.Contains(x.Direction))
                       .ToList();
                default:
                    return new List<Edge> { };
            }
        }

    }
}
