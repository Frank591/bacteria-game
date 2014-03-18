using System.Collections.Generic;
using System.Drawing;
using BacteriaSurvive.BL.GameArea;
using BacteriaSurvive.BL.GridHandlers;
using BacteriaSurvive.BL.GridHandlers.Output;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BacteriaSurvive.BL.Tests
{
    [TestClass]
    public class BacteriaSurviveCalculatorTests
    {




        #region square game area tests



        [TestMethod]
        public void Calculate100StepsAndUseCountFileSaver()
        {

            SquareGameAreaFactory<Bacteria> bacteriaAreaFactory = new SquareGameAreaFactory<Bacteria>(4, 4);
            SquareGameAreaFactory<GameCenter> gameCenterAreaFactory = new SquareGameAreaFactory<GameCenter>(4, 4);

            IGridHandler gridHandler = new BacteriaCountFileSaver(@"C:\temp\bacteriaLogCountTest.txt");
            BacteriaSurviveCalculator bacteriaSurviveCalculator = new BacteriaSurviveCalculator(50, gridHandler, bacteriaAreaFactory, gameCenterAreaFactory);

            bacteriaSurviveCalculator.InsertBacteria(new Bacteria(100, 0, 0, BacteriaType.A, 4), 0, 0);
            bacteriaSurviveCalculator.InsertBacteria(new Bacteria(0, 100, 0, BacteriaType.B, 4), 3, 0);
            bacteriaSurviveCalculator.InsertBacteria(new Bacteria(0, 0, 100, BacteriaType.C, 4), 0, 3);
            bacteriaSurviveCalculator.EvaluteGrid();

        }



        [TestMethod]
        public void Calculate100StepsAndUseGridFileSaver()
        {

            IGridHandler gridHandler = new GridFileSaver(@"C:\temp");
            SquareGameAreaFactory<Bacteria> bacteriaAreaFactory = new SquareGameAreaFactory<Bacteria>(100, 100);
            SquareGameAreaFactory<GameCenter> gameCenterAreaFactory = new SquareGameAreaFactory<GameCenter>(100, 100);
            BacteriaSurviveCalculator bacteriaSurviveCalculator = new BacteriaSurviveCalculator(100, gridHandler,bacteriaAreaFactory,gameCenterAreaFactory);

            bacteriaSurviveCalculator.InsertBacteria(new Bacteria(100, 0, 0, BacteriaType.A, 4), 0, 0);
            bacteriaSurviveCalculator.InsertBacteria(new Bacteria(0, 100, 0, BacteriaType.B, 4), 3, 0);
            bacteriaSurviveCalculator.InsertBacteria(new Bacteria(0, 0, 100, BacteriaType.C, 4), 0, 3);
           
            bacteriaSurviveCalculator.EvaluteGrid();
           
        }
        



                [TestMethod]
                public void Calculate100StepsAndUserAllSavers()
                {

                    IGridHandler vectorHandler = new BacteriaStrategyVectorFileSaver(@"C:\temp\bacteriaLogVectorTest.txt");
                    IGridHandler gridHandler = new GridFileSaver(@"C:\temp");
                    IGridHandler countHandler = new BacteriaCountFileSaver(@"C:\temp\bacteriaLogCountTest.txt");

                    GridHandlersQueue handlersQueue = new GridHandlersQueue();
                    handlersQueue.Add(vectorHandler);
                    handlersQueue.Add(gridHandler);
                    handlersQueue.Add(countHandler);


                    SquareGameAreaFactory<Bacteria> bacteriaAreaFactory = new SquareGameAreaFactory<Bacteria>(10, 10);
                    SquareGameAreaFactory<GameCenter> gameCenterAreaFactory = new SquareGameAreaFactory<GameCenter>(10, 10);
                    BacteriaSurviveCalculator bacteriaSurviveCalculator = new BacteriaSurviveCalculator(100, handlersQueue, bacteriaAreaFactory, gameCenterAreaFactory);

                    bacteriaSurviveCalculator.InsertBacteria(new Bacteria(100, 0, 0, BacteriaType.A, 4), 0, 0);
                    bacteriaSurviveCalculator.InsertBacteria(new Bacteria(0, 100, 0, BacteriaType.B, 4), 3, 0);
                    bacteriaSurviveCalculator.InsertBacteria(new Bacteria(0, 0, 100, BacteriaType.C, 4), 0, 3);
                    bacteriaSurviveCalculator.EvaluteGrid();
            
                }

        
                [TestMethod]
                public void TestBacteryWidthMutation0()
                {

                    IGridHandler vectorHandler = new BacteriaStrategyVectorFileSaver(@"C:\temp\bacteriaLogVectorTest.txt");
                    IGridHandler gridHandler = new GridFileSaver(@"C:\temp");
                    IGridHandler countHandler = new BacteriaCountFileSaver(@"C:\temp\bacteriaLogCountTest.txt");



                    GridHandlersQueue handlersQueue = new GridHandlersQueue();
                    handlersQueue.Add(vectorHandler);
                    handlersQueue.Add(gridHandler);
                    handlersQueue.Add(countHandler);


                    SquareGameAreaFactory<Bacteria> bacteriaAreaFactory = new SquareGameAreaFactory<Bacteria>(10, 10);
                    SquareGameAreaFactory<GameCenter> gameCenterAreaFactory = new SquareGameAreaFactory<GameCenter>(10, 10);
                    BacteriaSurviveCalculator bacteriaSurviveCalculator = new BacteriaSurviveCalculator(100, handlersQueue,bacteriaAreaFactory, gameCenterAreaFactory);
                    bacteriaSurviveCalculator.InsertBacteria(new Bacteria(0, 0, 100, BacteriaType.C, 0), 0, 3);
                    GameResult result=bacteriaSurviveCalculator.EvaluteGrid();
                    Assert.AreEqual(result.Winner.Value, BacteriaType.C);
                }

        #endregion


                #region trinagle game area test

                [TestMethod]
                public void Calculate100StepsAndUseCountFileSaver_TriangeGameArea()
                {
                    IList<Point> nodePoints = new List<Point>();
                    nodePoints.Add(new Point(0, 0));
                    nodePoints.Add(new Point(5, 0));
                    nodePoints.Add(new Point(5, 5));

                    PolygonGameAreaFactory<Bacteria> bacteriaAreaFactory = new PolygonGameAreaFactory<Bacteria>(10, 10, nodePoints);
                    PolygonGameAreaFactory<GameCenter> gameCenterAreaFactory = new PolygonGameAreaFactory<GameCenter>(10, 10, nodePoints);

                    IGridHandler vectorHandler = new BacteriaStrategyVectorFileSaver(@"C:\temp\bacteriaLogVectorTest.txt");
                    IGridHandler gridHandler = new GridFileSaver(@"C:\temp");
                    IGridHandler countHandler = new BacteriaCountFileSaver(@"C:\temp\bacteriaLogCountTest.txt");

                    GridHandlersQueue handlersQueue = new GridHandlersQueue();
                    handlersQueue.Add(vectorHandler);
                    handlersQueue.Add(gridHandler);
                    handlersQueue.Add(countHandler);
                    BacteriaSurviveCalculator bacteriaSurviveCalculator = new BacteriaSurviveCalculator(50, handlersQueue, bacteriaAreaFactory, gameCenterAreaFactory);

                    bacteriaSurviveCalculator.InsertBacteria(new Bacteria(100, 0, 0, BacteriaType.A, 4), 0, 0);
                    bacteriaSurviveCalculator.InsertBacteria(new Bacteria(0, 100, 0, BacteriaType.B, 4), 5, 0);
                    bacteriaSurviveCalculator.InsertBacteria(new Bacteria(0, 0, 100, BacteriaType.C, 4), 5, 5);
                    bacteriaSurviveCalculator.EvaluteGrid();

                }

                #endregion 

        
                #region polygon game area test

                [TestMethod]
                public void Calculate100StepsAndUseCountFileSaver_PolygonGameArea()
                {
                    IList<Point> nodePoints = new List<Point>();
                    nodePoints.Add(new Point(4, 4));
                    nodePoints.Add(new Point(4, 8));
                    nodePoints.Add(new Point(8, 10));
                    nodePoints.Add(new Point(12, 8));
                    nodePoints.Add(new Point(12, 4));
                    nodePoints.Add(new Point(8, 2));

                    PolygonGameAreaFactory<Bacteria> bacteriaAreaFactory = new PolygonGameAreaFactory<Bacteria>(20, 20, nodePoints);
                    PolygonGameAreaFactory<GameCenter> gameCenterAreaFactory = new PolygonGameAreaFactory<GameCenter>(20, 20, nodePoints);

                    IGridHandler vectorHandler = new BacteriaStrategyVectorFileSaver(@"C:\temp\bacteriaLogVectorTest.txt");
                    IGridHandler gridHandler = new GridFileSaver(@"C:\temp");
                    IGridHandler countHandler = new BacteriaCountFileSaver(@"C:\temp\bacteriaLogCountTest.txt");

                    GridHandlersQueue handlersQueue = new GridHandlersQueue();
                    handlersQueue.Add(vectorHandler);
                    handlersQueue.Add(gridHandler);
                    handlersQueue.Add(countHandler);
                    BacteriaSurviveCalculator bacteriaSurviveCalculator = new BacteriaSurviveCalculator(50, handlersQueue, bacteriaAreaFactory, gameCenterAreaFactory);

                    bacteriaSurviveCalculator.InsertBacteria(new Bacteria(100, 0, 0, BacteriaType.A, 4), 4, 4);
                    bacteriaSurviveCalculator.InsertBacteria(new Bacteria(0, 100, 0, BacteriaType.B, 4), 12, 4);
                    bacteriaSurviveCalculator.InsertBacteria(new Bacteria(0, 0, 100, BacteriaType.C, 4), 8, 4);
                    bacteriaSurviveCalculator.EvaluteGrid();

                }

                #endregion 






    }
}
