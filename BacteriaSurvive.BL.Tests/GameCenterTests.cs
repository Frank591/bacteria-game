using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BacteriaSurvive.BL.Tests
{
    [TestClass]
    public class GameCenterTests
    {

        [TestMethod]
        public void Game1Player()
        {
            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                GameCenter gameCenter = new GameCenter(random);
                gameCenter.AddPlayer(new Bacteria(90, 5, 5, BacteriaType.A, 4));
                Bacteria winner = gameCenter.Play();
                Assert.AreEqual(winner.Type, BacteriaType.A);
            }
        }


        [TestMethod]
        public void Game4Players()
        {
            Random random = new Random();

            for (int i = 0; i < 1000000; i++)
            {
                GameCenter gameCenter = new GameCenter(random);
                gameCenter.AddPlayer(new Bacteria(90, 5, 5, BacteriaType.A, 4));
                gameCenter.AddPlayer(new Bacteria(33, 33, 34, BacteriaType.A, 4));
                gameCenter.AddPlayer(new Bacteria(5, 90, 5, BacteriaType.B, 4));
                gameCenter.AddPlayer(new Bacteria(5, 5, 90, BacteriaType.C, 4));
                Bacteria winner = gameCenter.Play();
            }


        }
    }
}
