
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BacteriaSurvive.BL.Tests
{
    [TestClass]
    public class BacteriaIncubatorTests
    {
        [TestMethod]
        public void CreateBacteriaChildOneGeneration()
        {

            BacteriaIncubator bacteriaIncubator = new BacteriaIncubator(4, new Random());

            Bacteria parent = new Bacteria(100, 0, 0, BacteriaType.A, 4);
            for (int i = 0; i < 100000; i++)
            {
                Bacteria child = bacteriaIncubator.GenerateChild(parent);
                Assert.AreEqual(child.Type, parent.Type);
                Assert.AreEqual(child.PaperProbability + child.ScissorsProbability + child.StoneProbability <= 100, true);
            }


            parent = new Bacteria(0, 100, 0, BacteriaType.B, 4);
            for (int i = 0; i < 100000; i++)
            {
                Bacteria child = bacteriaIncubator.GenerateChild(parent);
                Assert.AreEqual(child.Type, parent.Type);
                Assert.AreEqual(child.PaperProbability + child.ScissorsProbability + child.StoneProbability <= 100, true);
            }

            parent = new Bacteria(0, 0, 100, BacteriaType.C, 4);
            for (int i = 0; i < 1000000; i++)
            {
                Bacteria child = bacteriaIncubator.GenerateChild(parent);
                Assert.AreEqual(child.Type, parent.Type);
                Assert.AreEqual(child.PaperProbability + child.ScissorsProbability + child.StoneProbability <= 100, true);
            }


        }


        [TestMethod]
        public void CreateBacteriaChildRecursive()
        {

            BacteriaIncubator bacteriaIncubator = new BacteriaIncubator(4, new Random());

            Bacteria parent = new Bacteria(100, 0, 0, BacteriaType.A, 4);
            for (int i = 0; i < 1000000; i++)
            {
                Bacteria child = bacteriaIncubator.GenerateChild(parent);
                Assert.AreEqual(child.Type, parent.Type);
                Assert.AreEqual(child.PaperProbability + child.ScissorsProbability + child.StoneProbability <= 100, true);
                parent = child;
            }


            parent = new Bacteria(0, 100, 0, BacteriaType.B, 4);
            for (int i = 0; i < 1000000; i++)
            {
                Bacteria child = bacteriaIncubator.GenerateChild(parent);
                Assert.AreEqual(child.Type, parent.Type);
                Assert.AreEqual(child.PaperProbability + child.ScissorsProbability + child.StoneProbability <= 100, true);
                parent = child;
            }

            parent = new Bacteria(0, 0, 100, BacteriaType.C, 4);
            for (int i = 0; i < 1000000; i++)
            {
                Bacteria child = bacteriaIncubator.GenerateChild(parent);
                Assert.AreEqual(child.Type, parent.Type);
                Assert.AreEqual(child.PaperProbability + child.ScissorsProbability + child.StoneProbability <= 100, true);
                parent = child;
            }


            Assert.AreEqual(parent.Type, parent.Type);

        }
    }
}
