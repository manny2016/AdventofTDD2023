using System.Drawing;

namespace AdventofTDD2023.Tests
{
    public class Day10Test
    {
        [Fact]
        public void GetSquareTest()
        {
            var inputData = @"
.....
.F-7.
.|.|.
.L-J.
.....
";
            var day10 = new Day10();
            var square = day10.GetSquare(inputData);
            Assert.Equal(5, square.Length);
            Assert.True(square.All(x => x.Length.Equals(5)));
        }

        [Fact]
        public void IsOutofRange_ReturnTrue()
        {
            var sequare = new char[][] {
                new char[]{ '.','.','.','.'},
                new char[]{ '.','L','R','.'},
                new char[]{ '.','L','S','.'},
                new char[]{ '.','.','.','.'},
            };
            var day10 = new Day10();
            Assert.True(day10.IsOutofRange(sequare, new Point(0, 0)));
            Assert.True(day10.IsOutofRange(sequare, new Point(0, 1)));
            Assert.True(day10.IsOutofRange(sequare, new Point(0, -1)));
            Assert.True(day10.IsOutofRange(sequare, new Point(-1, -1)));
            Assert.True(day10.IsOutofRange(sequare, new Point(-1, 100)));
            Assert.True(day10.IsOutofRange(sequare, new Point(100, 100)));
            Assert.False(day10.IsOutofRange(sequare, new Point(1, 1)));
            Assert.True(day10.IsOutofRange(sequare, new Point(2, 2)));
        }

        [Fact]
        public void GetMaxStepsTest()
        {
            var inputData = @"
.....
.S-7.
.|.|.
.L-J.
.....
";
            var day10 = new Day10();
            

            inputData = @"
..F7.
.FJ|.
SJ.L7
|F--J
LJ...
";
            Assert.Equal(8, day10.GetMaxSteps(inputData));
        }

        [Fact]

        public void GetStartPointTest()
        {
            var sequare = new char[][] {
                new char[]{ '.','.','.','.'},
                new char[]{ '.','L','R','.'},
                new char[]{ '.','L','S','.'},
                new char[]{ '.','.','.','.'},
            };
            var day10 = new Day10();
            Assert.Equal(new Point(2, 2), day10.GetStartPoint(sequare));
        }

        
    }
}
