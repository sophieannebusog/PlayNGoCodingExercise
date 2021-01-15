using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker_PlayNGo
{
    class PlayPoker
    {
        /// <summary>
        /// Compare the total values of cards in hand
        /// and Displays the name of winner
        /// </summary>

        public string[] playerNames, cardsAtHand;

        public Card[] p1hand, p2hand, p3hand;

        public Card[] sortedp1hand, sortedp2hand, sortedp3hand;

        public PlayPoker()
        {
            playerNames = new string[3];
            cardsAtHand = new string[3];

            p1hand = new Card[5];
            sortedp1hand = new Card[5];

            p2hand = new Card[5];
            sortedp2hand = new Card[5];

            p3hand = new Card[5];
            sortedp3hand = new Card[5];
        }

        public void play()
        {
            sortCards();
            evaluateHands();
        }

        /// <summary>
        /// Sort the cards based on values
        /// </summary>
       
        private void sortCards()
        {
            var sortedHand1 = from hand in p1hand
                              orderby hand.V
                              select hand;

            var sortedHand2 = from hand in p2hand
                                orderby hand.V
                                select hand;

            var sortedHand3 = from hand in p3hand
                     orderby hand.V
                     select hand;

            var index = 0;
            foreach(var e in sortedHand1.ToList())
            {
                sortedp1hand[index] = e;
                index++;
            }

            index = 0;
            foreach (var e in sortedHand2.ToList())
            {
                sortedp2hand[index] = e;
                index++;
            }

            index = 0;
            foreach (var e in sortedHand3.ToList())
            {
                sortedp3hand[index] = e;
                index++;
            }
        }

        private void evaluateHands()
        {
            HandEvaluator _sortedp1hand = new HandEvaluator(sortedp1hand);
            HandEvaluator _sortedp2hand = new HandEvaluator(sortedp2hand);
            HandEvaluator _sortedp3hand = new HandEvaluator(sortedp3hand);

            Hand player1Hand = _sortedp1hand.checkPokerHand();
            Hand player2Hand = _sortedp2hand.checkPokerHand();
            Hand player3Hand = _sortedp3hand.checkPokerHand();

            //Console.WriteLine("player1Hand: " + player1Hand);
            //Console.WriteLine("player2Hand: " + player2Hand);
            //Console.WriteLine("player3Hand: " + player3Hand);

            if (player1Hand > player2Hand && player1Hand > player2Hand)
            {
                Console.WriteLine("******************");
                Console.WriteLine(playerNames[0] + " WINS! " + cardsAtHand[0] + " (" + player1Hand + ")");
                Console.WriteLine("******************");

            }
            else if (player2Hand > player1Hand && player2Hand > player3Hand)
            {
                Console.WriteLine("******************");
                Console.WriteLine(playerNames[1] + " WINS! " + cardsAtHand[1] + " (" + player2Hand + ")");
                Console.WriteLine("******************");

            }
            else if(player3Hand > player1Hand && player3Hand > player2Hand)
            {
                Console.WriteLine("******************");
                Console.WriteLine(playerNames[2] + " WINS! " + cardsAtHand[2] + " (" + player3Hand + ")");
                Console.WriteLine("******************");

            }
            else //if the hands are the same, evaluate the values
            {
                // Compare the total values of cards
                Dictionary<int, int> total = new Dictionary<int, int>();
                total.Add(0, _sortedp1hand.cardValues.Total);
                total.Add(1, _sortedp2hand.cardValues.Total);
                total.Add(2, _sortedp3hand.cardValues.Total);

                Dictionary<int, int> highcards = new Dictionary<int, int>();
                highcards.Add(0, _sortedp1hand.cardValues.HighestCard);
                highcards.Add(1, _sortedp2hand.cardValues.HighestCard);
                highcards.Add(2, _sortedp3hand.cardValues.HighestCard);

                // Highest values in dict
                var maxTotals = total.Aggregate((l, r) => l.Value > r.Value ? l : r);
                var maxHighCards = highcards.Aggregate((l, r) => l.Value > r.Value ? l : r) ;

                //Console.WriteLine("maxTotals.Value: " + maxTotals.Value);
                //Console.WriteLine("maxHighCards.Value: " + maxHighCards.Value);

                Hand currentHand = maxTotals.Key == 0 ? player1Hand : maxTotals.Key == 1 ? player2Hand : player3Hand;

                var duplicateVals = total.GroupBy(r => r.Value)
                    .Where(grp => grp.Count() == 2)
                    .SelectMany(grp => grp.Select(subItem => subItem.Key))
                    .ToList();

                /*foreach (var v in duplicateVals)
                {
                    Console.WriteLine("whats this: " + v);

                }*/

                // Same value and different suit, both should win
                if (duplicateVals.Count > 0)
                {
                    Console.WriteLine("******************");
                    Console.WriteLine("2 Winners!!");
                    Console.WriteLine(playerNames[duplicateVals[0]] + " WINS! " + cardsAtHand[duplicateVals[0]] + " (" + player1Hand + ")");
                    Console.WriteLine(playerNames[duplicateVals[1]] + " WINS! " + cardsAtHand[duplicateVals[1]] + " (" + player1Hand + ")");
                    Console.WriteLine("******************");
                }
                else
                {
                    if (maxTotals.Value > maxHighCards.Value)
                    {
                        Console.WriteLine("******************");
                        Console.WriteLine(playerNames[maxTotals.Key] + " WINS! " + cardsAtHand[maxTotals.Key] + " (" + currentHand + ")");
                        Console.WriteLine("******************");

                    }
                    else if (maxHighCards.Value > maxTotals.Value)
                    {
                        Console.WriteLine("******************");
                        Console.WriteLine(playerNames[maxHighCards.Key] + " WINS! " + cardsAtHand[maxHighCards.Key] + " (" + currentHand + ")");
                        Console.WriteLine("******************");

                    }
                    else
                    {
                        Console.WriteLine("DRAW, no one wins!");

                    }
                }
            }
        }
    }
}
