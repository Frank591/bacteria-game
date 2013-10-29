using System;
using System.Configuration;
using System.Windows;
using BacteriaSurvive.BL;
using BacteriaSurvive.BL.GridHandlers;
using BacteriaSurvive.BL.GridHandlers.Output;

namespace BacteriaSurvive.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["defaultCountFilePath"]))
                return;
            tbCountFileName.Text = ConfigurationManager.AppSettings["defaultCountFilePath"];


            if (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["defaultVectorFilePath"]))
                return;
            tbVectorFileName.Text = ConfigurationManager.AppSettings["defaultVectorFilePath"];


            if (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["defaultMatrixDirPath"]))
                return;
            tbMatrixFolderName.Text = ConfigurationManager.AppSettings["defaultMatrixDirPath"];



        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            uint areaWidth;
            uint areaHeight;
            uint stepCount;

            IGridHandler vectorHandler = new BacteriaStrategyVectorFileSaver(tbVectorFileName.Text);
            IGridHandler gridHandler = new GridFileSaver(tbMatrixFolderName.Text);
            IGridHandler countHandler = new BacteriaCountFileSaver(tbCountFileName.Text);

            GridHandlersQueue handlersQueue = new GridHandlersQueue();
            handlersQueue.Add(vectorHandler);
            handlersQueue.Add(gridHandler);
            handlersQueue.Add(countHandler);
            try
            {
                 areaWidth = uint.Parse(tbAreaWidth.Text);
                 areaHeight = uint.Parse(tbAreaHeight.Text);
                 stepCount = uint.Parse(tbStepCount.Text);

            }
            catch(Exception error)
            {
                MessageBox.Show("Ширина,высота или количество ходов заданы некорректно");
                return;
            }


            BacteriaSurviveCalculator bacteriaSurviveCalculator = new BacteriaSurviveCalculator(areaWidth, areaHeight, stepCount, handlersQueue);


            try
            {
                uint aX = uint.Parse(tbAStoneX.Text);
                uint aY = uint.Parse(tbAStoneY.Text);
                int aStonePercent = int.Parse(tbAStonePercent.Text);
                int aPaperPercent = int.Parse(tbAPaperPercent.Text);
                int aScissorsPercent = int.Parse(tbAScissorsPercent.Text);
                int aMutation = int.Parse(tbAStoneMutationLimit.Text);
                if (aMutation%2!=0)
                    throw new ApplicationException("бактерия А: Константа мутации должна быть четным числом");
                Bacteria bacteriaA=new Bacteria(aStonePercent,aScissorsPercent,aPaperPercent,BacteriaType.A, aMutation);
                bacteriaSurviveCalculator.InsertBacteria(bacteriaA,aX,aY);
            }
             catch (Exception error)
             {
                 MessageBox.Show("один из параметров А бактерии задан неверно ");
                 return;
             }

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
                Bacteria bacteriaB = new Bacteria(bStonePercent, bScissorsPercent, bPaperPercent, BacteriaType.B, bMutation);
                bacteriaSurviveCalculator.InsertBacteria(bacteriaB, bX, bY);
            }
            catch (Exception error)
            {
                MessageBox.Show("один из параметров B бактерии задан неверно ");
                return;
            }

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
                Bacteria bacteriaC = new Bacteria(cStonePercent, cScissorsPercent, cPaperPercent, BacteriaType.C, cMutation);
                bacteriaSurviveCalculator.InsertBacteria(bacteriaC, cX, cY);
            }
            catch (Exception error)
            {
                MessageBox.Show("один из параметров C бактерии задан неверно ");
                return;
            }

            try
            {
                bacteriaSurviveCalculator.EvaluteGrid();
            }
            catch (Exception error)
            {

                MessageBox.Show(error.Message);
            }

            

            MessageBox.Show("Готово!");


        }
    }
}
