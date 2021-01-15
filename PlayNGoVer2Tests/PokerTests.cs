using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker_PlayNGo;
using System;

namespace PlayNGoVer2Tests
{
    [TestClass]
    public class PokerTests
    {
        [TestMethod]
        public void TestInputs()
        {
            string player1 = "Joe";
            string player2 = "Jen";
            string player3 = "Bob";

            string cards1 = "3H, 6H, 8H, JH, KH";
            string cards2 = "3C, 3D, 3S, 8C, 10H";
            string cards3 = "2H, 5C, 7S, 10C, AC";
            //string cards3 = "2H, 4H, 3D, "; // If cards have incomplete information

           // Inputs should be correct to avoid any error in the dealing or
           // checking of the cards
           // Format should be correct too

            var p = new Program();
            try
            {
                p.startGame(player1, player2, player3,
                            cards1, cards2, cards3);
            }
            catch (System.IndexOutOfRangeException e)
            {
                Assert.ThrowsException<System.IndexOutOfRangeException>(() => p.startGame(player1, player2, player3,
                            cards1, cards2, cards3));
            }
            catch (System.FormatException e)
            {
                Assert.ThrowsException<System.FormatException>(() => p.startGame(player1, player2, player3,
                            cards1, cards2, cards3));
            }
        }
    }
}
