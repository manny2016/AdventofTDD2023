//using day_1_Trebuchet.Program;
namespace day_1_Trebuchet.Tests
{
    public class UnitTestPart1
    {
        [Fact]
        public void digitsAtBothEnds()
        {
            string[] str = new string[1] { "1asdfdsf8" };
            TextLinesPart1 textLines = new TextLinesPart1(str);
            Assert.Equal(18, textLines.getSum());
        }
        [Fact]
        public void digitsInString()
        {
            string[] str = new string[1] { "hts4grgg9gg" };
            TextLinesPart1 textLines = new TextLinesPart1(str);
            Assert.Equal(49, textLines.getSum());
        }
        [Fact]
        public void moreThanTwoDigits()
        {
            string[] str = new string[1] { "5sdf6848971gdg" };
            TextLinesPart1 textLines = new TextLinesPart1(str);
            Assert.Equal(51, textLines.getSum());
        }
        [Fact]
        public void onlyOneDigit()
        {
            string[] str = new string[1] { "dfgrdblphsd6ooojfdd" };
            TextLinesPart1 textLines = new TextLinesPart1(str);
            Assert.Equal(66, textLines.getSum());
        }
        [Fact]
        public void onlyOneDigitAtBeginning()
        {
            string[] str = new string[1] { "5dfgrdblphsdd" };
            TextLinesPart1 textLines = new TextLinesPart1(str);
            Assert.Equal(55, textLines.getSum());
        }
        [Fact]
        public void onlyOneDigitAtEnd()
        {
            string[] str = new string[1] { "dfgrdblphsdd9" };
            TextLinesPart1 textLines = new TextLinesPart1(str);
            Assert.Equal(99, textLines.getSum());
        }
        [Fact]
        public void multiTextLines()
        {
            string[] str = new string[4] { "dfgrdblphsd6ooojfdd", "458179", "ds77799", "9796sff9" };
            TextLinesPart1 textLines = new TextLinesPart1(str);
            Assert.Equal(293, textLines.getSum());
        }
    }
}