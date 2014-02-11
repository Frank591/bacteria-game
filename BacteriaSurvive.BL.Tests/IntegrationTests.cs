using System;
using System.IO;
using BacteriaSurvive.BL.GameArea;
using BacteriaSurvive.BL.GridHandlers;
using BacteriaSurvive.BL.GridHandlers.Output;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BacteriaSurvive.BL.Tests
{
    [TestClass]
    public class IntegrationTests
    {


        



        [TestMethod]
        public void RunGame100Times()
        {


            string resultsDir = @"C:\temp\тестовый расчет_" + DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("hh_mm");

            if (!Directory.Exists(resultsDir))
                Directory.CreateDirectory(resultsDir);


            string vectorSaverResultsDir = Path.Combine(resultsDir, "VectorSaverResults");
            if (!Directory.Exists(vectorSaverResultsDir))
                Directory.CreateDirectory(vectorSaverResultsDir);

            string countSaverResultsDir = Path.Combine(resultsDir, "BacteriaCountSaverResults");
            if (!Directory.Exists(countSaverResultsDir))
                Directory.CreateDirectory(countSaverResultsDir);

            string gridSaverResultsDir = Path.Combine(resultsDir, "gridSaverResults");
            if (!Directory.Exists(gridSaverResultsDir))
                Directory.CreateDirectory(gridSaverResultsDir);


            string statisticsFilePath = Path.Combine(resultsDir, "statistics.txt");



            using (System.IO.StreamWriter file = new System.IO.StreamWriter(statisticsFilePath, true))
            {
                file.AutoFlush = true;

                for (int i = 0; i < 100; i++)
                {
                    //

                    string iterationName = "iteration_" + i;
                    string vectorResultsFilePath = Path.Combine(vectorSaverResultsDir, iterationName + ".txt");
                    string countResultsFilePath = Path.Combine(countSaverResultsDir, iterationName + ".txt");
                    string gridSaverDirPath = Path.Combine(gridSaverResultsDir, iterationName);

                    if (!Directory.Exists(gridSaverDirPath))
                        Directory.CreateDirectory(gridSaverDirPath);



                    IGridHandler vectorHandler = new BacteriaStrategyVectorFileSaver(vectorResultsFilePath);
                    IGridHandler gridHandler = new GridFileSaver(gridSaverDirPath);
                    IGridHandler countHandler = new BacteriaCountFileSaver(countResultsFilePath);



                    GridHandlersQueue handlersQueue = new GridHandlersQueue();
                    handlersQueue.Add(vectorHandler);
                    handlersQueue.Add(gridHandler);
                    handlersQueue.Add(countHandler);


                    SquareGameAreaFactory<Bacteria> bacteriaAreaFactory = new SquareGameAreaFactory<Bacteria>(4, 4);
                    SquareGameAreaFactory<GameCenter> gameCenterAreaFactory = new SquareGameAreaFactory<GameCenter>(4, 4);

                    BacteriaSurviveCalculator bacteriaSurviveCalculator = new BacteriaSurviveCalculator(4, 4, 100, handlersQueue,bacteriaAreaFactory,gameCenterAreaFactory);

                    bacteriaSurviveCalculator.InsertBacteria(new Bacteria(100, 0, 0, BacteriaType.A, 10), 0, 0);
                    bacteriaSurviveCalculator.InsertBacteria(new Bacteria(0, 100, 0, BacteriaType.B, 8), 3, 0);
                    bacteriaSurviveCalculator.InsertBacteria(new Bacteria(0, 0, 100, BacteriaType.C, 0), 0, 3);



                    GameResult gameResult = bacteriaSurviveCalculator.EvaluteGrid();



                    int winnerType = 0;
                    if (gameResult.Winner.HasValue)
                        winnerType = (int)gameResult.Winner.Value;

                    file.WriteLine(i + " " + gameResult.IterationNumber + " " + winnerType);


                }

            }

        }

    }
}
