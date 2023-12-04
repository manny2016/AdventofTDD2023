namespace AdventofTDD2023.Tests
{
    public class Day2Tests
    {
        [Fact]
        public void GetAnswer_InvalidArgument_ThrowException()
        {
            var day2 = new Day2();
            Assert.Throws<ArgumentException>(() =>
            {
                day2.GetAnswer(string.Empty, 0, 0, 0);
            });
            Assert.Throws<ArgumentException>(() =>
            {
                day2.GetAnswer(null, 0, 0, 0);
            });
        }

        [Fact]
        public void GetAnswer_ThrowException()
        {
            var day2 = new Day2();
            var inputText = @"
Game 1: 2 green, 6 blue, 7 red; 12 green, 6 blue, 3 red; 5 red, 18 green, 4 blue
Game 2: 10 green, 4 red; 2 red; 12 green, 11 red, 1 blue; 1 blue, 11 red, 5 green; 10 red, 9 green, 1 blue
Game 3: 3 green; 15 red, 7 blue, 1 green; 3 red, 6 blue, 1 green; 14 blue, 13 red, 2 green; 1 green, 6 blue, 6 red; 16 red, 13 blue, 2 green
Game 4: 5 blue; 8 blue, 7 red; 9 blue, 5 red, 4 green; 4 red, 1 green; 8 red, 6 blue; 2 blue, 4 red, 3 green
Game 5: 3 blue, 4 red, 10 green; 13 green, 8 blue, 2 red; 2 red, 4 green, 6 blue; 3 blue, 5 green, 2 red; 4 red, 13 blue, 8 green; 9 green, 2 red
";

        }
    }
}