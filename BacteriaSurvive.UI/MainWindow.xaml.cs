using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using BacteriaSurvive.BL;
using BacteriaSurvive.BL.GridHandlers;
using BacteriaSurvive.BL.GridHandlers.Output;
using MessageBox = System.Windows.MessageBox;


namespace BacteriaSurvive.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private  FolderBrowserDialog _folderBrowserDialog = new FolderBrowserDialog();

        BackgroundWorker worker;

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
            
            

            worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);     

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
            double value = 0;



            string vectorSaverResultsDir = Path.Combine(calculationParams.ResultsDir, "VectorSaverResults");
            if (!Directory.Exists(vectorSaverResultsDir))
                Directory.CreateDirectory(vectorSaverResultsDir);

            string countSaverResultsDir = Path.Combine(calculationParams.ResultsDir, "BacteriaCountSaverResults");
            if (!Directory.Exists(countSaverResultsDir))
                Directory.CreateDirectory(countSaverResultsDir);

            string gridSaverResultsDir = Path.Combine(calculationParams.ResultsDir, "gridSaverResults");
            if (!Directory.Exists(gridSaverResultsDir))
                Directory.CreateDirectory(gridSaverResultsDir);



            string statisticsFilePath = Path.Combine(calculationParams.ResultsDir, "statistics.txt");


           

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(statisticsFilePath, true))
            {
                file.AutoFlush = true;

                BacteriaIncubator bacteriaIncubator=new BacteriaIncubator(0,new Random());

                for (int i = 0; i < calculationParams.GamesCount; i++)
                {
                
                    IList<BacteriaCoordinates> clonedPlayers=new List<BacteriaCoordinates>();

                    foreach (var player in calculationParams.Players)
                    {
                        BacteriaCoordinates clonedPlayer = new BacteriaCoordinates(bacteriaIncubator.Clone(player.Bacteria),player.X,player.Y);
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







                    BacteriaSurviveCalculator bacteriaSurviveCalculator = new BacteriaSurviveCalculator(calculationParams.AreaWidth, calculationParams.AreaHeight, calculationParams.StepCount, handlersQueue);
                    foreach (BacteriaCoordinates player in clonedPlayers)
                    {
                        bacteriaSurviveCalculator.InsertBacteria(player.Bacteria, player.X, player.Y);
                    }

                    GameResult gameResult = bacteriaSurviveCalculator.EvaluteGrid();
                    int winnerType = 0;
                    if (gameResult.Winner.HasValue)
                        winnerType = (int)gameResult.Winner.Value;

                    file.WriteLine(i + " " + gameResult.IterationNumber + " " + winnerType);

                    Dispatcher.Invoke(updProgress, new object[] { System.Windows.Controls.ProgressBar.ValueProperty, ++value });

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

            IList<BacteriaCoordinates> players=new List<BacteriaCoordinates>();

             if (chkAEnabled.IsChecked.HasValue && chkAEnabled.IsChecked.Value)
                    {
                        try
                        {
                            uint aX = uint.Parse(tbAStoneX.Text);
                            uint aY = uint.Parse(tbAStoneY.Text);
                            int aStonePercent = int.Parse(tbAStonePercent.Text);
                            int aPaperPercent = int.Parse(tbAPaperPercent.Text);
                            int aScissorsPercent = int.Parse(tbAScissorsPercent.Text);
                            int aMutation = int.Parse(tbAStoneMutationLimit.Text);
                            if (aMutation % 2 != 0)
                                throw new ApplicationException("бактерия А: Константа мутации должна быть четным числом");
                            Bacteria bacteriaA = new Bacteria(aStonePercent, aScissorsPercent, aPaperPercent,
                                                              BacteriaType.A, aMutation);
                            players.Add( new BacteriaCoordinates(bacteriaA, aX, aY));
                        }
                        catch (Exception error)
                        {
                            MessageBox.Show("один из параметров А бактерии задан неверно ");
                            return;
                        }
                    }

                    if (chkBEnabled.IsChecked.HasValue && chkBEnabled.IsChecked.Value)
                    {

                        try
                        {
                            uint bX = uint.Parse(tbBStoneX.Text);
                            uint bY = uint.Parse(tbBStoneY.Text);
                            int bStonePercent = int.Parse(tbBStonePercent.Text);
                            int bPaperPercent = int.Parse(tbBPaperPercent.Text);
                            int bScissorsPercent = int.Parse(tbBScissorsPercent.Text);
                            int bMutation = int.Parse(tbBStoneMutationLimit.Text);
                            if (bMutation % 2 != 0)
                                throw new ApplicationException("бактерия B: Константа мутации должна быть четным числом");
                            Bacteria bacteriaB = new Bacteria(bStonePercent, bScissorsPercent, bPaperPercent,
                                                              BacteriaType.B, bMutation);
                            players.Add(new BacteriaCoordinates(bacteriaB, bX, bY));
                        }
                        catch (Exception error)
                        {
                            MessageBox.Show("один из параметров B бактерии задан неверно ");
                            return;
                        }
                    }


                    if (chkCEnabled.IsChecked.HasValue && chkCEnabled.IsChecked.Value)
                    {

                        try
                        {
                            uint cX = uint.Parse(tbCStoneX.Text);
                            uint cY = uint.Parse(tbCStoneY.Text);
                            int cStonePercent = int.Parse(tbCStonePercent.Text);
                            int cPaperPercent = int.Parse(tbCPaperPercent.Text);
                            int cScissorsPercent = int.Parse(tbCScissorsPercent.Text);
                            int cMutation = int.Parse(tbCStoneMutationLimit.Text);
                            if (cMutation % 2 != 0)
                                throw new ApplicationException("бактерия C: Константа мутации должна быть четным числом");
                            Bacteria bacteriaC = new Bacteria(cStonePercent, cScissorsPercent, cPaperPercent,
                                                              BacteriaType.C, cMutation);
                            players.Add( new BacteriaCoordinates(bacteriaC, cX, cY));
                        }
                        catch (Exception error)
                        {
                            MessageBox.Show("один из параметров C бактерии задан неверно ");
                            return;
                        }

                    }



            progressBar.Maximum = gamesCount;
            progressBar.Value = 0;

            CalculationParams calculationParams = new CalculationParams(gamesCount, areaWidth, areaHeight, stepCount,
                                                                        resultsDir, isCountSaverEnebled,
                                                                        isVectorSaverEnebled, isGridSaverEnebled,
                                                                        players);
            worker.RunWorkerAsync(calculationParams);
        }

        private void chkAEnabled_Checked(object sender, RoutedEventArgs e)
        {
            if (chkAEnabled.IsChecked.HasValue && chkAEnabled.IsChecked.Value)
            {
                if (tbAPaperPercent==null)
                    return;
                tbAPaperPercent.Visibility = Visibility.Visible;
                tbAScissorsPercent.Visibility = Visibility.Visible;
                tbAStoneMutationLimit.Visibility = Visibility.Visible;
                tbAStonePercent.Visibility = Visibility.Visible;
                tbAStoneX.Visibility = Visibility.Visible;
                tbAStoneY.Visibility = Visibility.Visible;
                
            }
            else
            {
                tbAPaperPercent.Visibility = Visibility.Hidden;
                tbAScissorsPercent.Visibility = Visibility.Hidden;
                tbAStoneMutationLimit.Visibility = Visibility.Hidden;
                tbAStonePercent.Visibility = Visibility.Hidden;
                tbAStoneX.Visibility = Visibility.Hidden;
                tbAStoneY.Visibility = Visibility.Hidden;
                
            }
        }

        private void chkBEnabled_Checked(object sender, RoutedEventArgs e)
        {
            if (chkBEnabled.IsChecked.HasValue && chkBEnabled.IsChecked.Value)
            {
                if (tbBPaperPercent == null)
                    return;
                tbBPaperPercent.Visibility = Visibility.Visible;
                tbBScissorsPercent.Visibility = Visibility.Visible;
                tbBStoneMutationLimit.Visibility = Visibility.Visible;
                tbBStonePercent.Visibility = Visibility.Visible;
                tbBStoneX.Visibility = Visibility.Visible;
                tbBStoneY.Visibility = Visibility.Visible;

            }
            else
            {
                tbBPaperPercent.Visibility = Visibility.Hidden;
                tbBScissorsPercent.Visibility = Visibility.Hidden;
                tbBStoneMutationLimit.Visibility = Visibility.Hidden;
                tbBStonePercent.Visibility = Visibility.Hidden;
                tbBStoneX.Visibility = Visibility.Hidden;
                tbBStoneY.Visibility = Visibility.Hidden;
            }
        }

        private void chkCEnabled_Checked(object sender, RoutedEventArgs e)
        {
            if (chkCEnabled.IsChecked.HasValue && chkCEnabled.IsChecked.Value)
            {
                if (tbCPaperPercent == null)
                    return;
                tbCPaperPercent.Visibility = Visibility.Visible;
                tbCScissorsPercent.Visibility = Visibility.Visible;
                tbCStoneMutationLimit.Visibility = Visibility.Visible;
                tbCStonePercent.Visibility = Visibility.Visible;
                tbCStoneX.Visibility = Visibility.Visible;
                tbCStoneY.Visibility = Visibility.Visible;

            }
            else
            {
                tbCPaperPercent.Visibility = Visibility.Hidden;
                tbCScissorsPercent.Visibility = Visibility.Hidden;
                tbCStoneMutationLimit.Visibility = Visibility.Hidden;
                tbCStonePercent.Visibility = Visibility.Hidden;
                tbCStoneX.Visibility = Visibility.Hidden;
                tbCStoneY.Visibility = Visibility.Hidden;
            }
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
    }


    
}
