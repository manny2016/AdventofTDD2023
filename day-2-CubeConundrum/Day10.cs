using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace AdventofTDD2023
{
    public class Day10
    {

        private readonly IDictionary<char, Func<Point, Point[]>> _moveNext = new
            Dictionary<char, Func<Point, Point[]>>
        {
            { '|', (current) => {
                // | is a vertical pipe connecting north and south. (Up & Down)
                return new Point[]{
                    new Point(current.X + 1,current.Y),//Move down
                    new Point(current.X - 1,current.Y)//Move Up
                };
            }},
            { '-', (current) => {
                // - is a horizontal pipe connecting east and west.( Left & Right)
                return new Point[] {
                    new Point(current.X,current.Y+1),//Move right 
                    new Point(current.X,current.Y-1)//Move left
                };
            }},
            { 'L', (current) => {
                // L is a 90-degree bend connecting north and east. (Right & Up) 
                return new Point[] {
                    new Point(current.X-1,current.Y),//Move up
                    new Point(current.X,current.Y+1),//Move right
                };
            }},
            { 'J', (current) => {
                //J is a 90-degree bend connecting north and west. (Up & Left)
                return new Point[] {
                    new Point(current.X-1,current.Y),//move up
                    new Point(current.X,current.Y-1),//move left
                };
            }},
            { '7', (current) => {
                //7 is a 90-degree bend connecting south and west.  (Down & Left)
                return new Point[] {
                    new Point(current.X+1,current.Y),//Move Down
                    new Point(current.X,current.Y-1),//Move Left
                };
            }},
            { 'F', (current) => {
                //F is a 90-degree bend connecting south and east. (Down & Right)
                return new Point[] {
                    new Point(current.X+1,current.Y),//Move Down
                    new Point(current.X,current.Y+1),//Move right
                };
            }},
            {
                'S',(current)=>{
                    return new Point[]
                    {
                        new Point(current.X+1,current.Y),//Move Down
                        new Point(current.X-1,current.Y),//Move UP
                        new Point(current.X,current.Y+1),//Move Right
                        new Point(current.X,current.Y-1)//Move Right
                    };
                }

            }
        };
        private readonly HashSet<Point> _touchedPoints = new HashSet<Point>(PointEqualityComparer.Instance);
        public decimal GetMaxSteps(string inputText)
        {
            var square = GetSquare(inputText);
            var current = GetStartPoint(square);
            var result = MoveNext(square, current, 0, null);
            return Math.Ceiling(result / 2);
        }

        private decimal MoveNext(char[][] square, Point current, decimal steps, Point? previous)
        {
            _touchedPoints.Add(current);

            var nextPoints = _moveNext[square[current.X][current.Y]].Invoke(current);
            var tempSteps = steps;

            foreach (var next in nextPoints)
            {
                if (IsOutofRange(square, next) || next == previous || _touchedPoints.Contains(next)) continue;
               
                tempSteps = Math.Max(tempSteps, MoveNext(square, next, steps + 1, current));
            }

            return tempSteps;
        }


        public Point GetStartPoint(char[][] square)
        {
            for (var x = 0; x < square.Length; x++)
            {
                for (var y = 0; y < square[x].Length; y++)
                {
                    if (square[x][y].Equals('S'))
                        return new Point(x, y);
                }
            }
            throw new KeyNotFoundException("Can not found the start point.");
        }

        public char[][] GetSquare(string inputData)
        {
            var lines = inputData.Split("\r\n".ToCharArray()).Where(x => !string.IsNullOrEmpty(x)).ToArray();
            var square = new char[lines.Length][];
            for (var i = 0; i < lines.Length; i++)
            {
                square[i] = lines[i].Trim().ToCharArray();
            }
            return square;
        }

        public bool IsOutofRange(char[][] square, Point point)
        {
            return (point.X == -1
                || point.Y == -1
                || point.X >= square[0].Length
                || point.Y >= square.Length
                || square[point.X][point.Y] == '.'
                || square[point.X][point.Y] == 'S');
        }
    }

    class PointEqualityComparer : IEqualityComparer<Point>
    {
        public static PointEqualityComparer Instance = new PointEqualityComparer();

        public bool Equals(Point x, Point y)
        {
            return x.X == y.X && x.Y == y.Y;
        }

        public int GetHashCode([DisallowNull] Point obj)
        {
            return $"{obj.X}:{obj.Y}".GetHashCode();
        }
    }
}
