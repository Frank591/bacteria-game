using BacteriaSurvive.BL.GridHandlers;
using BacteriaSurvive.BL.GridHandlers.Output;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BacteriaSurvive.BL.Tests
{
    [TestClass]
    public class BacteriaSurviveCalculatorTests
    {


     





        [TestMethod]
        public void Calculate100StepsAndUseCountFileSaver()
        {

            IGridHandler gridHandler = new BacteriaCountFileSaver(@"C:\temp\bacteriaLogCountTest.txt");
            BacteriaSurviveCalculator bacteriaSurviveCalculator = new BacteriaSurviveCalculator(4, 4, 50, gridHandler);

            bacteriaSurviveCalculator.InsertBacteria(new Bacteria(100, 0, 0, BacteriaType.A, 4), 0, 0);
            bacteriaSurviveCalculator.InsertBacteria(new Bacteria(0, 100, 0, BacteriaType.B, 4), 3, 0);
            bacteriaSurviveCalculator.InsertBacteria(new Bacteria(0, 0, 100, BacteriaType.C, 4), 0, 3);
            bacteriaSurviveCalculator.EvaluteGrid();

        }



        [TestMethod]
        public void Calculate100StepsAndUseGridFileSaver()
        {

            IGridHandler gridHandler = new GridFileSaver(@"C:\temp");
            BacteriaSurviveCalculator bacteriaSurviveCalculator = new BacteriaSurviveCalculator(100, 100, 100, gridHandler);

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

            BacteriaSurviveCalculator bacteriaSurviveCalculator = new BacteriaSurviveCalculator(10, 10, 100, handlersQueue);

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
            BacteriaSurviveCalculator bacteriaSurviveCalculator = new BacteriaSurviveCalculator(10, 10, 100, handlersQueue);
            bacteriaSurviveCalculator.InsertBacteria(new Bacteria(0, 0, 100, BacteriaType.C, 0), 0, 3);
            GameResult result=bacteriaSurviveCalculator.EvaluteGrid();
            Assert.AreEqual(result.Winner.Value, BacteriaType.C);
        }



    }
}
