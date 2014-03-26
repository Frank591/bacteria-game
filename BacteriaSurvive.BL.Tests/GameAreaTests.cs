using System;
using System.Collections.Generic;
using System.Drawing;
using BacteriaSurvive.BL.GameArea;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BacteriaSurvive.BL.Tests
{
    [TestClass]
    public class GameAreaTests
    {
        #region defects 

        /*
         * test cases for https://github.com/Frank591/bacteria-game/issues/1
         * incorrect work  polygonGameArea.IsInArea
         */
        [TestMethod]
        public void GitIssue1Case1()
        {
            IList<Point> nodePoints = new List<Point>();
            nodePoints.Add(new Point(0, 0));
            nodePoints.Add(new Point(5, 0));
            nodePoints.Add(new Point(7, 10));

            PolygonGameArea<Object> polygonGameArea = new PolygonGameArea<Object>(20, 20, nodePoints);

            //outer points
            Assert.AreEqual(polygonGameArea.IsInArea(0, 4), false);
            Assert.AreEqual(polygonGameArea.IsInArea(0, 8), false);
            Assert.AreEqual(polygonGameArea.IsInArea(11, 11), false);

            //inner points
            Assert.AreEqual(polygonGameArea.IsInArea(0, 0), true);
            Assert.AreEqual(polygonGameArea.IsInArea(3, 1), true);
            Assert.AreEqual(polygonGameArea.IsInArea(3, 2), true);
            Assert.AreEqual(polygonGameArea.IsInArea(5, 3), true);
            Assert.AreEqual(polygonGameArea.IsInArea(6, 8), true);
          
        }

        [TestMethod]
        public void GitIssue1Case2()
        {
            IList<Point> nodePoints = new List<Point>();
            nodePoints.Add(new Point(0, 0));
            nodePoints.Add(new Point(10, 2));
            nodePoints.Add(new Point(10, 0));

            PolygonGameArea<Object> polygonGameArea = new PolygonGameArea<Object>(20, 20, nodePoints);
            //outer points
            Assert.AreEqual(polygonGameArea.IsInArea(0, 4), false);
            Assert.AreEqual(polygonGameArea.IsInArea(0, 1), false);
            Assert.AreEqual(polygonGameArea.IsInArea(11, 11), false);
            Assert.AreEqual(polygonGameArea.IsInArea(1, 3), false);
            Assert.AreEqual(polygonGameArea.IsInArea(10, 3), false);
            Assert.AreEqual(polygonGameArea.IsInArea(3, 2), false);

            //inner points
            Assert.AreEqual(polygonGameArea.IsInArea(0, 0), true);
            Assert.AreEqual(polygonGameArea.IsInArea(9, 0), true);
            Assert.AreEqual(polygonGameArea.IsInArea(9, 1), true);
            Assert.AreEqual(polygonGameArea.IsInArea(9, 2), true);
            Assert.AreEqual(polygonGameArea.IsInArea(6, 1), true);
            Assert.AreEqual(polygonGameArea.IsInArea(3, 1), true);
        }



        #endregion

        [TestMethod]
        public void TriangleAreaTest()
        {
            IList<Point> nodePoints = new List<Point>();
            nodePoints.Add(new Point(0, 0));
            nodePoints.Add(new Point(5, 0));
            nodePoints.Add(new Point(5, 5));

            PolygonGameArea<Object> polygonGameArea = new PolygonGameArea<Object>(10, 10, nodePoints);
            Assert.AreEqual(polygonGameArea.IsInArea(0, 2), false);
            Assert.AreEqual(polygonGameArea.IsInArea(2, 0), true);
            Assert.AreEqual(polygonGameArea.IsInArea(0, 0), true);
            Assert.AreEqual(polygonGameArea.IsInArea(3, 2), true);
            Assert.AreEqual(polygonGameArea.IsInArea(2, 2), true);
            Assert.AreEqual(polygonGameArea.IsInArea(0, 5), false);
            Assert.AreEqual(polygonGameArea.IsInArea(5, 5), true);
            Assert.AreEqual(polygonGameArea.IsInArea(0, 6), false);
            Assert.AreEqual(polygonGameArea.IsInArea(6, 0), false);
        }



        [TestMethod]
        public void SquareAreaTest()
        {
            IList<Point> nodePoints = new List<Point>();
            nodePoints.Add(new Point(1, 1));
            nodePoints.Add(new Point(1, 6));
            nodePoints.Add(new Point(6, 6));
            nodePoints.Add(new Point(6, 1));
            PolygonGameArea<Object> polygonGameArea = new PolygonGameArea<Object>(10, 10, nodePoints);


            Assert.AreEqual(polygonGameArea.IsInArea(6, 5), true);
            Assert.AreEqual(polygonGameArea.IsInArea(1, 4), true);
            Assert.AreEqual(polygonGameArea.IsInArea(2, 2), true);
            Assert.AreEqual(polygonGameArea.IsInArea(4, 5), true);
            Assert.AreEqual(polygonGameArea.IsInArea(0, 0), false);
            Assert.AreEqual(polygonGameArea.IsInArea(7, 7), false);
            Assert.AreEqual(polygonGameArea.IsInArea(8, 4), false);

        }







        [TestMethod]
        public void PoligonAreaTest()
        {
            IList<Point> nodePoints = new List<Point>();
            nodePoints.Add(new Point(4, 4));
            nodePoints.Add(new Point(4, 8));
            nodePoints.Add(new Point(8, 10));
            nodePoints.Add(new Point(12, 8));
            nodePoints.Add(new Point(12, 4));
            nodePoints.Add(new Point(8, 2));
            PolygonGameArea<Object> polygonGameArea = new PolygonGameArea<Object>(20, 20, nodePoints);


         /*   Assert.AreEqual(polygonGameArea.IsInArea(8, 2), true);
            Assert.AreEqual(polygonGameArea.IsInArea(5, 8), true);
            Assert.AreEqual(polygonGameArea.IsInArea(5, 5), true);
            Assert.AreEqual(polygonGameArea.IsInArea(4, 7), true);
            Assert.AreEqual(polygonGameArea.IsInArea(0, 0), false);
            Assert.AreEqual(polygonGameArea.IsInArea(1, 1), false);
            Assert.AreEqual(polygonGameArea.IsInArea(10, 10), false);*/
            Assert.AreEqual(polygonGameArea.IsInArea(8, 4), true);

        }



    }
}
