using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
namespace day_1_Trebuchet
{
    public class TextLinesPart1
    {
        private string[] textLine;
        private int[] calibration;

        public TextLinesPart1(string[] input)
        {
            textLine = input;
            calibration = new int[textLine.Length];
        }

        public int getCalibration (int index)
        {
            this.calibration[index] = 0;
            string digitStr = Regex.Replace(this.textLine[index], @"[^0-9]+", "");
            this.calibration[index] += (digitStr[0] - '0') * 10;
            this.calibration[index] += digitStr[digitStr.Length - 1] - '0';
            int length = digitStr.Length;
            return this.calibration[index];
        }

        public int getSum ()
        {
            int sum = 0;
            int len = this.textLine.Length;
            for (int j = 0; j < len; j++)
            {
                sum += getCalibration(j);
            }
            return sum;
        }
    }
}
