using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day_4_Scratchcards.Tests
{
    public class UnitTest2
    {
        [Fact]
        public void NoMatchNumber()
        {
            ScratchCardPart2 card1 = new ScratchCardPart2("Card 1: 31 18 13 56 72 | 11");
            card1.CalculateMatchNumber();
            Assert.Equal(0, card1.GetMatchNumber());
        }
        [Fact]
        public void OneMatchNumber()
        {
            ScratchCardPart2 card2 = new ScratchCardPart2("Card 2: 72 | 74 77 10 72 35 67 36 11");
            card2.CalculateMatchNumber();
            Assert.Equal(1, card2.GetMatchNumber());
        }
        [Fact]
        public void SeveralMatchNumbers()
        {
            ScratchCardPart2 card3 = new ScratchCardPart2("Card 3: 31 18 13 56 72 | 18 77 10 72 35 67 36 31");
            card3.CalculateMatchNumber();
            Assert.Equal(3, card3.GetMatchNumber());
        }
        [Fact]
        public void AllTheSameMatchNumbers()
        {
            ScratchCardPart2 card4 = new ScratchCardPart2("Card 1: 41 48 83 86 17 | 41 41 41 41 41 41 41 41");
            card4.CalculateMatchNumber();
            Assert.Equal(8, card4.GetMatchNumber());
        }
        [Fact]
        public void HaveOneInstanceWhenCreated()
        {
            ScratchCardPart2 card5 = new ScratchCardPart2("Card 1: 1 2 3 4 | 5 6 7 8");
            Assert.Equal(1, card5.GetInstanceNumber());
        }
        [Fact]
        public void Have5InstancesWhenAdded4Instances()
        {
            ScratchCardPart2 card6 = new ScratchCardPart2("Card 1: 1 2 3 4 | 5 6 7 8");
            card6.AddInstanceNumber(4);
            Assert.Equal(5, card6.GetInstanceNumber());
        }
    }
}
