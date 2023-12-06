namespace day_4_Scratchcards.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void NoMatchNumber()
        {
            ScratchCardPart1 card1 = new ScratchCardPart1("Card 1: 31 18 13 56 72 | 11");
            Assert.Equal(0, card1.GetCardPoints());
        }
        [Fact]
        public void OneMatchNumber()
        {
            ScratchCardPart1 card2 = new ScratchCardPart1("Card 2: 72 | 74 77 10 72 35 67 36 11");
            Assert.Equal(1, card2.GetCardPoints());
        }
        [Fact]
        public void SeveralMatchNumbers()
        {
            ScratchCardPart1 card3 = new ScratchCardPart1("Card 3: 31 18 13 56 72 | 18 77 10 72 35 67 36 31");
            Assert.Equal(4, card3.GetCardPoints());
        }
        [Fact]
        public void AllTheSameMatchNumbers()
        {
            ScratchCardPart1 card4 = new ScratchCardPart1("Card 1: 41 48 83 86 17 | 41 41 41 41 41 41 41 41");
            Assert.Equal(128, card4.GetCardPoints());
        }

    }
}