namespace Day_3_Gear_Ratios.Tests
{
    public class GearRatioTests
    {
        [Theory]
        [InlineData(".123..", 0)]
        [InlineData(".123$..", 123)]
        [InlineData(
@"467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598..", 4361)]
        public void One_line_no_symbol_should_0(string input, int expect)
        {
            var gear = new GearRatio(input);
            var actual = gear.Caculate();
            Assert.Equal(expect, actual);
        }

        [Fact]
        public void Input_from_fiile_should_correct()
        {
            string filePath = "./to-submit.txt";
            using StreamReader reader = new StreamReader(filePath);
            string input = reader.ReadToEnd();
            var gear = new GearRatio(input);
            var actual = gear.Caculate();
            int expected = 527446;
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(".123..", 0)]
        [InlineData(".123$..", 0)]
        [InlineData(
@"467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598..", 467835)]
        public void Gear2(string input, int expect)
        {
            var gear = new GearRatio(input);
            var actual = gear.Caculate();
            Assert.Equal(expect, actual);
        }
    }

}