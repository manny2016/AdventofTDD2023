using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace day_1_Trebuchet
{
    public class TextLinesPart2
    {
        private string[] textLine;
        private int[] calibration;
        static string[] numberChoice1 = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};
        static string[] numberChoice2 = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", 
        "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};
        

        public TextLinesPart2(string[] input)
        {
            textLine = input;
            calibration = new int[textLine.Length];
        }

        public int getCalibration(int index)
        {
            this.calibration[index] = 0;
            string[] numbers;
            if (Regex.Replace(this.textLine[index], @"[^0-9]+", "") == "")
            {
                numbers = numberChoice1;
            }
            else
            {
                numbers = numberChoice2;
            }
            int indexBegin = this.textLine[index].Length;
            int indexEnd = -1;
            string numberBegin = "";
            string numberEnd = "";
            foreach (string number in numbers)
            {
                int index1 = this.textLine[index].IndexOf(number);
                
                int index2 = this.textLine[index].LastIndexOf(number);
                if (index1 != -1 && index1 < indexBegin)
                {   
                    indexBegin = index1;
                    numberBegin = number;
                }
                if (index2 > indexEnd)
                {
                    indexEnd = index2;
                    numberEnd = number;
                }
            }
            for (int i = 0; i < 20; ++i)
            {
                if (numberChoice2[i] == numberBegin)
                {
                    this.calibration[index] += (i % 10) * 10;
                    break;
                }
            }
            for (int i = 0; i < 20; ++i)
            {
                if (numberChoice2[i] == numberEnd)
                {
                    this.calibration[index] += i % 10;
                    break;
                }
            }
            return this.calibration[index];
        }

        public int getSum()
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
