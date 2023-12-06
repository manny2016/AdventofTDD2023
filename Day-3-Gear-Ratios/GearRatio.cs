using System.Collections.Generic;
using System.Diagnostics;

namespace Day_3_Gear_Ratios
{
    public class GearRatio
    {
        protected List<List<char>> schematic = new List<List<char>>();
        private HashSet<char> symbols = new HashSet<char>();

        public GearRatio(string input)
        {
            initSchematic(input);
        }

        public virtual int Caculate()
        {
            int totalSum = 0;

            for (int i = 0; i < schematic.Count; i++)
            {
                int wordValue = 0;
                bool isAdjacent = false;

                for (int j = 0; j < schematic[i].Count; j++)
                {
                    char currentChar = schematic[i][j];
                    if (char.IsNumber(currentChar))
                    {
                        // add to current word
                        int currentCharValue = int.Parse(currentChar.ToString());
                        wordValue = wordValue * 10 + currentCharValue;

                        if (!isAdjacent)
                        {
                            isAdjacent = checkAdjacent(i, j);
                        }
                    }
                    else
                    {
                        // pre is a valid number
                        // add to total
                        if (wordValue > 0 && isAdjacent)
                        {
                            Debug.WriteLine(wordValue);
                            totalSum += wordValue;
                        }
                        wordValue = 0;
                        isAdjacent = false;
                    }
                }
                if (wordValue > 0 && isAdjacent)
                {
                    totalSum += wordValue;
                }
            }
            return totalSum;
        }

        private bool isSymbol(char value)
        {
            return symbols.Contains(value);
        }

        private bool checkAdjacent(int i, int j)
        {
            // 8 position
            return isSymbole(i - 1, j - 1)
                || isSymbole(i - 1, j)
                || isSymbole(i - 1, j + 1)
                || isSymbole(i, j - 1)
                || isSymbole(i, j + 1)
                || isSymbole(i + 1, j - 1)
                || isSymbole(i + 1, j)
                || isSymbole(i + 1, j + 1);
        }

        protected virtual bool isSymbole(int i, int j)
        {
            if (i < 0 || j < 0 || i >=  schematic.Count || j >= schematic[i].Count)
            {
                return false;
            }
            return symbols.Contains(schematic[i][j]);
        }


        private void initSchematic(string input)
        {
            int row = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (i == 0)
                {
                    schematic.Add(new List<char>());
                }
                if (input[i] == '\r')
                {
                    schematic.Add(new List<char>());
                    row++;
                }
                else if(input[i] == '\n')
                {
                    // do nothing
                }
                else
                {
                    schematic[row].Add(input[i]);
                    if (!char.IsNumber(input[i]) && input[i] != '.')
                    {
                        symbols.Add(input[i]);
                    }
                }
            }
        }
    }
}
