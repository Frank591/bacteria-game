using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

using System.Windows.Forms;
using BacteriaSurvive.BL;
using BacteriaSurvive.BL.GameArea;
using BacteriaSurvive.BL.GridHandlers;
using BacteriaSurvive.BL.GridHandlers.Output;
using MessageBox = System.Windows.MessageBox;
using Point = System.Drawing.Point;

namespace BacteriaSurvive.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private FolderBrowserDialog _folderBrowserDialog = new FolderBrowserDialog();



        public MainWindow()
        {
            InitializeComponent();

            if (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["defaultResultDirPath"]))
                return;
            tbResultsDir.Text = ConfigurationManager.AppSettings["defaultResultDirPath"];



            // Set the help text description for the FolderBrowserDialog.
            _folderBrowserDialog.Description =
                "Выберите папку для сохранения результатов";
            // Do not allow the user to create new files via the FolderBrowserDialog.
            _folderBrowserDialog.ShowNewFolderButton = false;







        }




        void worker_DoWork(object sender, DoWorkEventArgs e)
        {

            CalculationParams calculationParams = e.Argument as CalculationParams;

            RunGamesCalculation(calculationParams);
            MessageBox.Show("Готово!");
        }

        delegate void UpdateProgressBarDelegate(DependencyProperty dp, object value);



        private void RunGamesCalculation(CalculationParams calculationParams)
        {








            UpdateProgressBarDelegate updProgress = new UpdateProgressBarDelegate(progressBar.SetValue);
    



            string vectorSaverResultsDir = Path.Combine(calculationParams.ResultsDir, "VectorSaverResults");
            if (!Directory.Exists(vectorSaverResultsDir))
                Directory.CreateDirectory(vectorSaverResultsDir);

            string countSaverResultsDir = Path.Combine(calculationParams.ResultsDir, "BacteriaCountSaverResults");
            if (!Directory.Exists(countSaverResultsDir))
                Directory.CreateDirectory(countSaverResultsDir);

            string gridSaverResultsDir = Path.Combine(calculationParams.ResultsDir, "gridSaverResults");
            if (!Directory.Exists(gridSaverResultsDir))
                Directory.CreateDirectory(gridSaverResultsDir);



            string statisticsFilePath = Path.Combine(calculationParams.ResultsDir, string.Format("statistics_{0}_{1}.txt", calculationParams.GamesStartIndex, calculationParams.GamesEndIndex));


            double value = 0;


            using (System.IO.StreamWriter file = new System.IO.StreamWriter(statisticsFilePath, true))
            {
                file.AutoFlush = true;

                BacteriaIncubator bacteriaIncubator = new BacteriaIncubator(0, new Random());

                for (uint i = calculationParams.GamesStartIndex; i < calculationParams.GamesEndIndex; i++)
                {

                    IList<BacteriaSettings> clonedPlayers = new List<BacteriaSettings>();

                    foreach (var player in calculationParams.Players)
                    {
                        BacteriaSettings clonedPlayer = new BacteriaSettings(bacteriaIncubator.Clone(player.Bacteria), player.X, player.Y);
                        clonedPlayers.Add(clonedPlayer);
                    }


                    string iterationName = "iteration_" + i;
                    string vectorResultsFilePath = Path.Combine(vectorSaverResultsDir, iterationName + ".txt");
                    string countResultsFilePath = Path.Combine(countSaverResultsDir, iterationName + ".txt");
                    string gridSaverDirPath = Path.Combine(gridSaverResultsDir, iterationName);


                    GridHandlersQueue handlersQueue = new GridHandlersQueue();

                    if (calculationParams.IsCountSaverEnabled)
                    {
                        IGridHandler countHandler = new BacteriaCountFileSaver(countResultsFilePath);
                        handlersQueue.Add(countHandler);
                    }

                    if (calculationParams.IsVectorSaverEnabled)
                    {
                        IGridHandler vectorHandler = new BacteriaStrategyVectorFileSaver(vectorResultsFilePath);
                        handlersQueue.Add(vectorHandler);
                    }


                    if (calculationParams.IsGridSaverEnabled)
                    {
                        if (!Directory.Exists(gridSaverDirPath))
                            Directory.CreateDirectory(gridSaverDirPath);
                        IGridHandler gridHandler = new GridFileSaver(gridSaverDirPath);
                        handlersQueue.Add(gridHandler);
                    }





                    IGameAreaFactory<Bacteria> bacteriaGameAreaFactory = new PolygonGameAreaFactory<Bacteria>((int)calculationParams.AreaWidth, (int)calculationParams.AreaHeight, calculationParams.Nodes);
                    IGameAreaFactory<GameCenter> gameCenterGameAreaFactory = new PolygonGameAreaFactory<GameCenter>((int)calculationParams.AreaWidth, (int)calculationParams.AreaHeight, calculationParams.Nodes);

                    BacteriaSurviveCalculator bacteriaSurviveCalculator = new BacteriaSurviveCalculator(calculationParams.StepCount, handlersQueue,bacteriaGameAreaFactory,gameCenterGameAreaFactory);

                    try
                    {
                        foreach (BacteriaSettings player in clonedPlayers)
                        {
                            bacteriaSurviveCalculator.InsertBacteria(player.Bacteria, player.X, player.Y);
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                        return;
                    }


                    GameResult gameResult = bacteriaSurviveCalculator.EvaluteGrid();
                    int winnerType = 0;
                    if (gameResult.Winner.HasValue)
                        winnerType = (int)gameResult.Winner.Value;

                    file.WriteLine(i + " " + gameResult.IterationNumber + " " + winnerType);
                    Dispatcher.Invoke(updProgress, new object[] { System.Windows.Controls.ProgressBar.ValueProperty, value++});
                }

            }
        }



        private void btnRun_Click(object sender, RoutedEventArgs e)
        {

            


            uint areaWidth;
            uint areaHeight;
            uint stepCount;

            try
            {
                areaWidth = uint.Parse(tbAreaWidth.Text);
                areaHeight = uint.Parse(tbAreaHeight.Text);
                stepCount = uint.Parse(tbStepCount.Text);

            }
            catch (Exception error)
            {
                MessageBox.Show("Ширина,высота или количество ходов заданы некорректно");
                return;
            }


            uint gamesCount;

            try
            {
                gamesCount = uint.Parse(tbGamesCount.Text);
                
            }
            catch (Exception error)
            {
                MessageBox.Show("Количество игр задано некорректно");
                return;
            }

            string resultsDir = Path.Combine(tbResultsDir.Text, "рассчет от " + DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("hh_mm"));
            if (!Directory.Exists(resultsDir))
                Directory.CreateDirectory(resultsDir);


            bool isCountSaverEnebled = chkCountSaverEnabled.IsChecked.HasValue && chkCountSaverEnabled.IsChecked.Value;

            bool isVectorSaverEnebled = chkVectorSaverEnabled.IsChecked.HasValue && chkVectorSaverEnabled.IsChecked.Value;

            bool isGridSaverEnebled = chkGridSaverEnabled.IsChecked.HasValue && chkGridSaverEnabled.IsChecked.Value;

            IList<BacteriaSettings> players = new List<BacteriaSettings>();


            for (int i = 0; i < lvPlayers.Items.Count; i++)
            {

                BacteriaSettings currBacteriaSettings = (BacteriaSettings)lvPlayers.Items[i];
                try
                {
                    currBacteriaSettings.Bacteria.VerifyProperties();
                    players.Add(currBacteriaSettings);
                }
                catch (Exception error)
                {
                    MessageBox.Show(String.Format("Один из параметров  бактерии c индексом {0} задан неверно. Ошибка: {1} ", i, error.Message));
                    return;
                }


            }


            progressBar.Maximum = gamesCount;
            progressBar.Value = 0;




            IList<Point> nodes = new List<Point>();



            for (int i = 0; i < lvNodes.Items.Count; i++)
            {

                Point currNode = (Point)lvNodes.Items[i];
                nodes.Add(currNode);
            }



            int startGameIndex = 0;
            int gamesOnThread = (int)gamesCount/4;
            int endGameIndex = startGameIndex+gamesOnThread;

            CalculationParams calculationParams = new CalculationParams((uint)startGameIndex, (uint)endGameIndex, areaWidth, areaHeight, stepCount, nodes,
                                                                        resultsDir, isCountSaverEnebled,
                                                                        isVectorSaverEnebled, isGridSaverEnebled,
                                                                        players);

            startGameIndex = endGameIndex;
            endGameIndex = startGameIndex + gamesOnThread;
            CalculationParams calculationParams1 = new CalculationParams((uint)startGameIndex, (uint)endGameIndex, areaWidth, areaHeight, stepCount, nodes,
                                                                        resultsDir, isCountSaverEnebled,
                                                                        isVectorSaverEnebled, isGridSaverEnebled,
                                                                        players);
            startGameIndex = endGameIndex;
            endGameIndex = startGameIndex + gamesOnThread;
            CalculationParams calculationParams2 = new CalculationParams((uint)startGameIndex, (uint)endGameIndex, areaWidth, areaHeight, stepCount, nodes,
                                                                        resultsDir, isCountSaverEnebled,
                                                                        isVectorSaverEnebled, isGridSaverEnebled,
                                                                        players);

            startGameIndex = endGameIndex;
            endGameIndex = (int)gamesCount;
            CalculationParams calculationParams3 = new CalculationParams((uint)startGameIndex, (uint)endGameIndex, areaWidth, areaHeight, stepCount, nodes,
                                                                        resultsDir, isCountSaverEnebled,
                                                                        isVectorSaverEnebled, isGridSaverEnebled,
                                                                        players);





            Task.Factory.StartNew(() =>
            {

            /*    Thread.BeginThreadAffinity();
                int threadId = Thread.CurrentThread.ManagedThreadId;
                Process proc = Process.GetCurrentProcess();
                ProcessThread procThread = proc.Threads.Cast<ProcessThread>().Single(
                    pt => pt.Id == threadId
                );
                procThread.ProcessorAffinity = new IntPtr(0x01);
                //
                // work
                //*/


                RunGamesCalculation(calculationParams);


                /*procThread.ProcessorAffinity = new IntPtr(0xFFFF);
                Thread.EndThreadAffinity();*/

            }, TaskCreationOptions.LongRunning
                );


            Task.Factory.StartNew(() =>
            {

                RunGamesCalculation(calculationParams2);
            }, TaskCreationOptions.LongRunning
              );


            Task.Factory.StartNew(() =>
            {

                RunGamesCalculation(calculationParams1);
            }, TaskCreationOptions.LongRunning
             );

            Task.Factory.StartNew(() =>
            {

                RunGamesCalculation(calculationParams3);
            }, TaskCreationOptions.LongRunning
           );


        }





        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult result = _folderBrowserDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                if (!string.IsNullOrWhiteSpace(_folderBrowserDialog.SelectedPath))
                    tbResultsDir.Text = _folderBrowserDialog.SelectedPath;
            }

        }


        private void BtnAddbacteria_OnClick(object sender, RoutedEventArgs e)
        {
            lvPlayers.Items.Add(new BacteriaSettings(new Bacteria(100, 0, 0, BacteriaType.A, 4), 0, 0));
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (lvPlayers.SelectedItem != null)
            {
                lvPlayers.Items.Remove(lvPlayers.SelectedItem);

            }
            else
            {
                MessageBox.Show("Выберите бактерию для удаления");
            }
        }

        private void BtnRemoveNode_OnClick(object sender, RoutedEventArgs e)
        {
            if (lvNodes.SelectedItem != null)
            {
                lvNodes.Items.Remove(lvNodes.SelectedItem);

            }
            else
            {
                MessageBox.Show("Choose node for remove");
            }
        }

        private void BtnAddNode_OnClick(object sender, RoutedEventArgs e)
        {
            lvNodes.Items.Add(new Point( 0, 0));
        }
    }



}
