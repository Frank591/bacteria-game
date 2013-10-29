using System.Collections.Generic;
using System.IO;

namespace BacteriaSurvive.BL.GridHandlers.Output
{
    public class BacteriaStrategyVectorFileSaver : IGridHandler
    {
        private string _filePath;

        private int _handleCount = 0;


        public BacteriaStrategyVectorFileSaver(string filePath)
        {
            _filePath = filePath;
            if (File.Exists(_filePath))
                File.Delete(_filePath);
        }



        public void Handle(Bacteria[,] grid)
        {

            
            // Example #4: Append new text to an existing file
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(_filePath, true))
            {


                VectorStrategy paperStrategy=new VectorStrategy(EBacteriaAttributes.Paper);
                VectorStrategy stoneStrategy = new VectorStrategy(EBacteriaAttributes.Stone);
                VectorStrategy scissorsStrategy = new VectorStrategy(EBacteriaAttributes.Scissors);

         

                
                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    for (int j = 0; j < grid.GetLength(1); j++)
                    {
                        Bacteria currBacteria = grid[i, j];
                        if (currBacteria==null)
                            continue;
                        if (currBacteria.Type == BacteriaType.A)
                        {
                            stoneStrategy.SummBacteriaAttributes(currBacteria);
                        }

                        if (currBacteria.Type == BacteriaType.B)
                        {
                           scissorsStrategy.SummBacteriaAttributes(currBacteria);
                        }

                        if (currBacteria.Type == BacteriaType.C)
                        {
                            paperStrategy.SummBacteriaAttributes(currBacteria);
                        }

                    }
                }


                _handleCount++;
       IDictionary<EBacteriaAttributes, int> stoneVector = stoneStrategy.GetVector();
                IDictionary<EBacteriaAttributes, int> paperVector = paperStrategy.GetVector();
                IDictionary<EBacteriaAttributes, int> scissorsVector = scissorsStrategy.GetVector();

                file.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}", _handleCount, stoneVector[EBacteriaAttributes.Stone], stoneVector[EBacteriaAttributes.Scissors], stoneVector[EBacteriaAttributes.Paper],
                               scissorsVector[EBacteriaAttributes.Stone], scissorsVector[EBacteriaAttributes.Scissors], scissorsVector[EBacteriaAttributes.Paper],
                               paperVector[EBacteriaAttributes.Stone], paperVector[EBacteriaAttributes.Scissors], paperVector[EBacteriaAttributes.Paper]);
            }

        }
    }

    public class VectorStrategy
    {
        private readonly EBacteriaAttributes _type;
        private IDictionary<EBacteriaAttributes, int>  _vectorComponents=new Dictionary<EBacteriaAttributes, int>();

        private int _summCount = 0;

        public VectorStrategy(EBacteriaAttributes type)
        {
            _type = type;
            _vectorComponents.Add(EBacteriaAttributes.Paper, 0);
            _vectorComponents.Add(EBacteriaAttributes.Stone, 0);
            _vectorComponents.Add(EBacteriaAttributes.Scissors, 0);
        }

        public void SummBacteriaAttributes(Bacteria bacteria)
        {
            _summCount = _summCount+1;

            _vectorComponents[EBacteriaAttributes.Paper] = _vectorComponents[EBacteriaAttributes.Paper] +
                                                          bacteria.PaperProbability;

            _vectorComponents[EBacteriaAttributes.Scissors] = _vectorComponents[EBacteriaAttributes.Scissors] +
                                                         bacteria.ScissorsProbability;

            _vectorComponents[EBacteriaAttributes.Stone] = _vectorComponents[EBacteriaAttributes.Stone] +
                                                         bacteria.StoneProbability;
        }


        public int MembersCount
        {
            get { return _summCount; }
        }

        public IDictionary<EBacteriaAttributes, int> GetVector()
        {
            IDictionary<EBacteriaAttributes,int> result=new Dictionary<EBacteriaAttributes, int>();
            foreach (KeyValuePair<EBacteriaAttributes, int> componentSumma in _vectorComponents)
            {
                if (_summCount>0)
                    result.Add(componentSumma.Key, componentSumma.Value/_summCount);
                else
                {
                    result.Add(componentSumma.Key, 0);
                }
            }
            return result;
        }

    }


}