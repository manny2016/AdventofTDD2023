using System;
using day_4_Scratchcards;

class ScoreCount
{
    public ScoreCount() { }
    //part1
    /*static void Main(string[] args)
    {
        string path = "../../../input.txt";
        string[] input = File.ReadAllLines(path);
        int sum = 0;
        foreach (string line in input)
        {
            ScratchCardPart1 card = new ScratchCardPart1(line);
            //Console.WriteLine(line);
            sum += card.GetCardPoints();
        }
        Console.WriteLine(sum);
    }*/
    //part2
    static void Main(string[] args)
    {
        string path = "../../../input.txt";
        string[] input = File.ReadAllLines(path);
        int sum = 0;
        List<ScratchCardPart2> cardList = new List<ScratchCardPart2>();
        foreach (string line in input)
        {
            ScratchCardPart2 card = new ScratchCardPart2(line);
            card.CalculateMatchNumber();
            cardList.Add(card);
        }
        for (int i = 0; i < cardList.Count; i++)
        {
            int matchNumber = cardList[i].GetMatchNumber();
            int curInstanceNumber = cardList[i].GetInstanceNumber();
            sum += curInstanceNumber;
            if (matchNumber == 0)
                continue;
            else
            {
                
                for (int j = i + 1; j <= i + matchNumber; ++j)
                {
                    cardList[j].AddInstanceNumber(curInstanceNumber);
                }
            }
        }
        Console.WriteLine(sum);
    }
}