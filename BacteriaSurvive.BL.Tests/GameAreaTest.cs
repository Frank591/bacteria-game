using System;
using System.Collections.Generic;
using System.Drawing;
using BacteriaSurvive.BL.GameArea;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BacteriaSurvive.BL.Tests
{
    [TestClass]
    public class GameAreaTest
    {


        [TestMethod]
        public void TriangleAreaTest1()
        {
            IList<Point> nodePoints = new List<Point>();
            nodePoints.Add(new Point(0, 0));
            nodePoints.Add(new Point(5, 0));
            nodePoints.Add(new Point(5, 5));

            TestGameArea<Object> testGameArea = new TestGameArea<Object>(10, 10, nodePoints);
            Assert.AreEqual(testGameArea.IsInArea(0, 2), false);
            Assert.AreEqual(testGameArea.IsInArea(2, 0), true);
            Assert.AreEqual(testGameArea.IsInArea(0, 0), true);
            Assert.AreEqual(testGameArea.IsInArea(3, 2), true);
            Assert.AreEqual(testGameArea.IsInArea(2, 2), true);
            Assert.AreEqual(testGameArea.IsInArea(0, 5), false);
            Assert.AreEqual(testGameArea.IsInArea(5, 5), true);
            Assert.AreEqual(testGameArea.IsInArea(0, 6), false);
            Assert.AreEqual(testGameArea.IsInArea(6, 0), false);
        }



        [TestMethod]
        public void TriangleAreaTest2()
        {
            IList<Point> nodePoints = new List<Point>();
            nodePoints.Add(new Point(0, 0));
            nodePoints.Add(new Point(0, 6));
            nodePoints.Add(new Point(9, 0));
            TestGameArea<Object> testGameArea = new TestGameArea<Object>(10, 10, nodePoints);
            testGameArea.IsInArea(2, 2);

        }
        

        
        
        
        [TestMethod]
        public void TestMethod1()
        {
        }



    }
}
