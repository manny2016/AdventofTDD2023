using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_3_Gear_Ratios
{

    public class GearRatios2 : GearRatio
    {
        public class Gear
        {
            public int Value { get; set; }
            public int AdjacentCount { get; set; }
        }

        private Dictionary<(int, int), Gear> multiples = new Dictionary<(int, int), Gear>();
        public GearRatios2(string input) : base(input)
        {
        }

        protected override bool isSymbole(int i, int j)
        {
            if (base.isSymbole(i, j))
            {
                return schematic[i][j] == '*';
            }
            return false;
        }

        public override int Caculate()
        {
            for (int i = 0; i < schematic.Count; i++)
            {
                int wordValue = 0;
                (int, int) gearPosition = (-1, -1);
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
                            isAdjacent = tryGetAdjacentPosition(i, j, out gearPosition);
                        }
                    }
                    else
                    {
                        if (wordValue > 0 && isAdjacent)
                        {
                            if (multiples.ContainsKey(gearPosition))
                            {
                                multiples[gearPosition].Value = multiples[(gearPosition)].Value * wordValue;
                                multiples[gearPosition].AdjacentCount++;
                            }
                            else
                            {
                                multiples.Add(gearPosition, new Gear { Value = wordValue, AdjacentCount = 1 }); 
                            }

                        }
                        wordValue = 0;
                        isAdjacent = false;
                    }
                }
            }

            return multiples.Where(x => x.Value.AdjacentCount == 2).Sum(x => x.Value.Value);
        }

        private bool tryGetAdjacentPosition(int i, int j, out (int, int) gearPosition)
        {
            return isSymbole(i - 1, j - 1, out gearPosition)
                || isSymbole(i - 1, j, out gearPosition)
                || isSymbole(i - 1, j + 1, out gearPosition)
                || isSymbole(i, j - 1, out gearPosition)
                || isSymbole(i, j + 1, out gearPosition)
                || isSymbole(i + 1, j - 1, out gearPosition)
                || isSymbole(i + 1, j, out gearPosition)
                || isSymbole(i + 1, j + 1, out gearPosition);
        }
        protected virtual bool isSymbole(int i, int j, out (int, int) position)
        {
            if (i < 0 || j < 0 || i >= schematic.Count || j >= schematic[i].Count)
            {
                position = (-1, -1);
                return false;
            }

            if (schematic[i][j] == '*')
            {
                position = (i, j);
                return true;
            }

            position = (-1, -1);
            return false;

        }
    }
}
