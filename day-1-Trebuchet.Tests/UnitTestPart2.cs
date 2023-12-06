using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day_1_Trebuchet.Tests
{
    public class UnitTestPart2
    {
        [Fact]
        public void oneLetterDigit()
        {
            string[] str = new string[1] { "two" };
            TextLinesPart2 textLines = new TextLinesPart2(str);
            Assert.Equal(22, textLines.getSum());
        }
        [Fact]
        public void twoLetterDigits()
        {
            string[] str = new string[1] { "fivenine" };
            TextLinesPart2 textLines = new TextLinesPart2(str);
            Assert.Equal(59, textLines.getSum());
        }
        [Fact]
        public void moreThanTwoLetterDigits()
        {
            string[] str = new string[1] { "eightwothree" };
            TextLinesPart2 textLines = new TextLinesPart2(str);
            Assert.Equal(83, textLines.getSum());
        }
        [Fact]
        public void letterDigitsAndLetters()
        {
            string[] str = new string[1] { "sdfsevengdfiveeeeightg" };
            TextLinesPart2 textLines = new TextLinesPart2(str);
            Assert.Equal(78, textLines.getSum());
        }
        [Fact]
        public void letterDigitsAndDigits()
        {
            string[] str = new string[1] { "skksseven8900dddddectwo99p" };
            TextLinesPart2 textLines = new TextLinesPart2(str);
            Assert.Equal(79, textLines.getSum());
        }
        [Fact]
        public void multiTextLines()
        {
            string[] str = new string[9] { "dfgrdblphsd6ooojfdd", "458179", "ds77799", "9796sff9", "two", "fivenine", "eightwothree", "sdfsevengdfiveeeeightg", "skksseven8900dddddectwo99p" };
            TextLinesPart2 textLines = new TextLinesPart2(str);
            Assert.Equal(614, textLines.getSum());
        }
    }
}
