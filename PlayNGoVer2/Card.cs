namespace Poker_PlayNGo
{
    class Card
    {

        public enum VALUE
        {
            TWO = 2,
            THREE,
            FOUR,
            FIVE,
            SIX,
            SEVEN,
            EIGHT,
            NINE,
            TEN,
            JACK,
            QUEEN,
            KING,
            ACE
        }

        public enum SUIT
        {
            H,
            S,
            D,
            C
        }

        public VALUE V { get; set; }
        public SUIT S { get; set; }
    }
}
