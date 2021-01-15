using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker_PlayNGo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Program main = new Program();

            string[] playerNamesInput = new string[] { "", "", "" };
            string[] playerCardsInput = new string[] { "", "", "" };

            while (true)
            {
                Console.WriteLine("\nLet's Play Poker! Please input player names and cards!");
                for (int i = 0; i < 3; i++)
                {
                    Console.Write((i + 1) + " player name: ");
                    playerNamesInput[i] = Console.ReadLine();

                    Console.Write("Cards are? (separate cards by comma): ");
                    playerCardsInput[i] = Console.ReadLine();
                }

                main.startGame(playerNamesInput[0], playerNamesInput[1], playerNamesInput[2],
                    playerCardsInput[0], playerCardsInput[1], playerCardsInput[2]);
            }
        }

        public void startGame(string p1name, string p2name, string p3name,
                             string p1cards, string p2cards, string p3cards)
        {
            PlayPoker p = new PlayPoker();
                
            List<string> cards1 = p1cards.Replace(" ", string.Empty).Split(',').ToList();
            List<string> cards2 = p2cards.Replace(" ", string.Empty).Split(',').ToList();
            List<string> cards3 = p3cards.Replace(" ", string.Empty).Split(',').ToList();

            p.playerNames[0] = p1name;
            p.playerNames[1] = p2name;
            p.playerNames[2] = p3name;

            p.cardsAtHand[0] = p1cards;
            p.cardsAtHand[1] = p2cards;
            p.cardsAtHand[2] = p3cards;

            // Player should input 5 cards 
            if (cards1.Count != 5 || cards2.Count != 5 || cards3.Count != 5)
            {
                Console.WriteLine("Please provide 5 cards for each players.");
                return;
            }

            for (int j = 0; j < 5; j++)
            {
                string getValue = cards1[j].ToString().Length > 2 ? cards1[j].Substring(0, 2).ToString() : cards1[j][0].ToString();
                int getSuit = cards1[j].ToString().Length > 2 ? 2 : 1;

                //Console.WriteLine("********** start ***********");
                //Console.WriteLine("whats the substring of 0-1: " + cards1[j].Substring(0, 2).ToString()); 
                //Console.WriteLine("What is getValue: " + getValue);
                //Console.WriteLine("What is getSuit: " + getSuit);

                // Card values should range from 2-10. A-J are accepted too
                // else card is unknown, do not procees

                if (!getValue.Equals("2") && !getValue.Equals("3") && !getValue.Equals("4")
                    && !getValue.Equals("5") && !getValue.Equals("6")
                    && !getValue.Equals("7") && !getValue.Equals("8")
                    && !getValue.Equals("9") && !getValue.Equals("10"))
                {
                    getValue = getValue.Equals("A") ? "14" : getValue.Equals("K") ? "13" :
                        getValue.Equals("Q") ? "12" : getValue.Equals("J") ? "11" : "unknown";
                }

                if (getValue.Equals("unknown") || !isSuitValid(cards1[j][getSuit].ToString()))
                {
                    Console.WriteLine("Cards are invalid");
                    return;
                }

                if (!isSuitValid(cards1[j][getSuit].ToString()))
                {
                    Console.WriteLine("Cards are invalid");
                    return;
                }
                Card.VALUE val = (Card.VALUE)int.Parse(getValue);
                Card.SUIT st = (Card.SUIT)Enum.Parse(typeof(Card.SUIT), cards1[j][getSuit].ToString());
                p.p1hand[j] = new Card { S = st, V = val };
                p.p1hand[j].V = val;
                p.p1hand[j].S = st;
            }

            // loop the cards for the second player
            for (int j = 0; j < 5; j++)
            {
                string getValue = cards2[j].ToString().Length > 2 ? cards2[j].Substring(0, 2).ToString() : cards2[j][0].ToString();
                int getSuit = cards2[j].ToString().Length > 2 ? 2 : 1;

                if (!getValue.Equals("2") && !getValue.Equals("3") && !getValue.Equals("4")
                    && !getValue.Equals("5") && !getValue.Equals("6")
                    && !getValue.Equals("7") && !getValue.Equals("8")
                    && !getValue.Equals("9") && !getValue.Equals("10")
                    )
                {
                    getValue = getValue.Equals("A") ? "14" : getValue.Equals("K") ? "13" :
                            getValue.Equals("Q") ? "12" : getValue.Equals("J") ? "11" : "unknown";
                }
                if (getValue.Equals("unknown") || !isSuitValid(cards2[j][getSuit].ToString()))
                {
                    Console.WriteLine("Cards are invalid");
                    return;
                }
                
                Card.VALUE val = (Card.VALUE)int.Parse(getValue);
                Card.SUIT st = (Card.SUIT)Enum.Parse(typeof(Card.SUIT), cards2[j][getSuit].ToString());
                p.p2hand[j] = new Card { S = st, V = val };

                p.p2hand[j].V = val;
                p.p2hand[j].S = st;
               
            }

            // loop the cards for the third player
            for (int j = 0; j < 5; j++)
            {
                string getValue = cards3[j].ToString().Length > 2 ? cards3[j].Substring(0, 2).ToString() : cards3[j][0].ToString();
                int getSuit = cards3[j].ToString().Length > 2 ? 2 : 1;

                if (!getValue.Equals("2") && !getValue.Equals("3") && !getValue.Equals("4")
                    && !getValue.Equals("5") && !getValue.Equals("6")
                    && !getValue.Equals("7") && !getValue.Equals("8")
                    && !getValue.Equals("9") && !getValue.Equals("10")
                    )
                {
                    getValue = getValue.Equals("A") ? "14" : getValue.Equals("K") ? "13" :
                              getValue.Equals("Q") ? "12" : getValue.Equals("J") ? "11" : "unknown";
                }
                if (getValue.Equals("unknown") || !isSuitValid(cards3[j][getSuit].ToString()))
                {
                    Console.WriteLine("Cards are invalid");
                    return;
                }
               
                Card.VALUE val = (Card.VALUE)int.Parse(getValue);
                Card.SUIT st = (Card.SUIT)Enum.Parse(typeof(Card.SUIT), cards3[j][getSuit].ToString());
                p.p3hand[j] = new Card { S = st, V = val };

                p.p3hand[j].V = val;
                p.p3hand[j].S = st;
            }
            p.play();

        }

        private bool isSuitValid(string getSuit)
        {
            if (!getSuit.Equals("H") && !getSuit.Equals("D")
                  && !getSuit.Equals("C") && !getSuit.Equals("S"))
                return false;

            return true;
        }
    }
}