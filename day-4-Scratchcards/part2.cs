using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace day_4_Scratchcards
{
    public class ScratchCardPart2
    {
        private int[] winningNumbers;
        private int[] myNumbers;
        private int instanceNumber = 1;
        private int matchNumber = 0;
        public ScratchCardPart2(string input)
        {
            string processedInput = new Regex("[\\s]+").Replace(input, " ");
            string[] words = processedInput.Split(' ');
            int[] tempArray = new int[100];
            int index = 2;
            while (words[index] != "|")
            {
                tempArray[index - 2] = int.Parse(words[index++]);
            }
            winningNumbers = tempArray.Skip(0).Take(index - 2).ToArray();
            int separatorIndex = ++index;
            for (; index < words.Length; ++index)
            {
                tempArray[index - separatorIndex] = int.Parse(words[index]);
            }
            myNumbers = tempArray.Skip(0).Take(index - separatorIndex).ToArray();
        }
        public void AddInstanceNumber(int newCopyNumber)
        {
            instanceNumber += newCopyNumber;
        }
        public void CalculateMatchNumber()
        {
            matchNumber = 0;
            foreach (int n in myNumbers)
            {
                if (Array.IndexOf(winningNumbers, n) != -1)
                {
                    matchNumber++;
                }
            }
        }
        public int GetMatchNumber()
        {
            return matchNumber;
        }

        public int GetInstanceNumber()
        {
            return instanceNumber;
        }
    }
}
