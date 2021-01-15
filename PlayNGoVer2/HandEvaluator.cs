using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker_PlayNGo
{
    /// <summary>
    /// Checks player hands cards, and
    /// determines the kind of pokerhand
    /// </summary>
   
    class HandEvaluator : Card
    {
        private int hTotal, dTotal, cTotal, sTotal;
        private Card[] cards;
        private CardsInHandVal cardValue;

        public HandEvaluator(Card[] sortedHand)
        {
            hTotal = 0;
            dTotal = 0;
            cTotal = 0;
            sTotal = 0;
            cards = new Card[5];
            Cards = sortedHand;
            cardValue = new CardsInHandVal();
        }

        public CardsInHandVal cardValues
        {
            get { return cardValue; }
            set { cardValue = value; }
        }

        public Card [] Cards
        {
            get { return cards; }
            set
            {
                for(int i = 0; i < 5; i++)
                {
                    cards[i] = value[i];
                }
            }
        }

        public Hand checkPokerHand()
        {
            foreach (var element in Cards)
            {
                if (element.S == SUIT.H)
                    hTotal++;
                else if (element.S == SUIT.D)
                    dTotal++;
                else if (element.S == SUIT.C)
                    cTotal++;
                else if (element.S == SUIT.S)
                    sTotal++;
            }

            if (isFourOfAKind())
                return Hand.FourKind;
            else if (isFullHouse())
                return Hand.FullHouse;
            else if (isFlush())
                return Hand.Flush;
            else if (isStraight())
                return Hand.Straight;
            else if (isThreeOfAKind())
                return Hand.ThreeKind;
            else if (isTwoPair())
                return Hand.TwoPairs;
            else if (isOnePair())
                return Hand.OnePair;

            cardValue.HighestCard = (int)cards[4].V;
            return Hand.HighCard;
        }

        private bool isOnePair()
        {
            if (cards[0].V == cards[1].V)
            {
                cardValue.Total = (int)cards[0].V + (int)cards[1].V
                    + (int)cards[2].V + (int)cards[3].V
                    + (int)cards[4].V;
                cardValue.HighestCard = (int)cards[4].V;

                return true;
            }
            else if (cards[1].V == cards[2].V)
            {
                cardValue.Total = (int)cards[0].V + (int)cards[1].V
                    + (int)cards[2].V + (int)cards[3].V
                    + (int)cards[4].V;
                cardValue.HighestCard = (int)cards[4].V;
                return true;
            }
            else if (cards[2].V == cards[3].V)
            {
                cardValue.Total = (int)cards[0].V + (int)cards[1].V
                    + (int)cards[2].V + (int)cards[3].V
                    + (int)cards[4].V;
                cardValue.HighestCard = (int)cards[4].V;
                return true;
            }
            else if (cards[3].V == cards[4].V)
            {
                cardValue.Total = (int)cards[0].V + (int)cards[1].V
                    + (int)cards[2].V + (int)cards[3].V
                    + (int)cards[4].V;
                cardValue.HighestCard = (int)cards[2].V;
                return true;
            }

            return false;
        }

        private bool isTwoPair()
        {
            if (cards[0].V == cards[1].V && cards[2].V == cards[3].V)
            {
                cardValue.Total = ((int)cards[1].V * 2) + ((int)cards[3].V * 2);
                cardValue.HighestCard = (int)cards[4].V;
                return true;
            }
            else if (cards[0].V == cards[1].V && cards[3].V == cards[4].V)
            {
                cardValue.Total = ((int)cards[1].V * 2) + ((int)cards[3].V * 2);
                cardValue.HighestCard = (int)cards[2].V;
                return true;
            }
            else if (cards[1].V == cards[2].V && cards[3].V == cards[4].V)
            {
                cardValue.Total = ((int)cards[1].V * 2) + ((int)cards[3].V * 2);
                cardValue.HighestCard = (int)cards[0].V;
                return true;
            }
            return false;
        }

        private bool isThreeOfAKind()
        {
            if ((cards[0].V == cards[1].V && cards[0].V == cards[2].V) ||
            (cards[1].V == cards[2].V && cards[1].V == cards[3].V))
            {
                cardValue.Total = (int)cards[2].V * 3;
                cardValue.HighestCard = (int)cards[4].V;
                return true;
            }
            else if (cards[2].V == cards[3].V && cards[2].V == cards[4].V)
            {
                cardValue.Total = (int)cards[2].V * 3;
                cardValue.HighestCard = (int)cards[1].V;
                return true;
            }
            return false;
        }

        private bool isFourOfAKind()
        {
            if(cards[0].V == cards[1].V && cards[0].V == cards[2].V && cards[0].V == cards[3].V)
            {
                cardValue.Total = (int)cards[1].V * 4;
                cardValue.HighestCard = (int)cards[4].V;
                return true;
            }
            else if (cards[1].V == cards[2].V && cards[1].V == cards[3].V && cards[1].V == cards[4].V)
            {
                cardValue.Total = (int)cards[1].V * 4;
                cardValue.HighestCard = (int)cards[0].V;
                return true;
            }

            return false;
        }

        private bool isFullHouse()
        {
            if ((cards[0].V == cards[1].V && cards[0].V == cards[2].V && cards[3].V == cards[4].V) ||
                (cards[0].V == cards[1].V && cards[2].V == cards[3].V && cards[2].V == cards[4].V))
            {
                cardValue.Total = (int)(cards[0].V) + (int)(cards[1].V) + (int)(cards[2].V) +
                    (int)(cards[3].V) + (int)(cards[4].V);
                return true;
            }

            return false;
        }

        private bool isFlush()
        {
            if (hTotal == 5 || dTotal == 5 || cTotal == 5 || sTotal == 5)
            {
                cardValue.Total = (int)cards[4].V;
                return true;
            }
            return false;
        }

        private bool isStraight()
        {
            if (cards[0].V + 1 == cards[1].V && cards[1].V + 1 == cards[2].V && cards[2].V + 1 == cards[3].V && cards[3].V + 1 == cards[4].V)
            {
                cardValue.Total = (int)cards[4].V;
                return true;
            }
            return false;
        }
    }

    public enum Hand
    {
        HighCard,
        OnePair,
        TwoPairs,
        ThreeKind,
        Straight,
        Flush,
        FullHouse,
        FourKind

        //FourKind,
        //FullHouse,
        //Flush,
        //Straight,
        //ThreeKind,
        //TwoPairs,
        //OnePair,
        //HighCard

    }

    public struct CardsInHandVal
    {
        public int Total { get; set; }
        public int HighestCard { get; set; }
    }
}
