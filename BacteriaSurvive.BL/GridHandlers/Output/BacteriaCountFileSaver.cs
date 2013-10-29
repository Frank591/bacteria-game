using System.IO;

namespace BacteriaSurvive.BL.GridHandlers.Output
{
    public class BacteriaCountFileSaver:IGridHandler
    {
        private string _filePath;

        private int _saveCount = 0;
       

        public BacteriaCountFileSaver(string filePath)
        {
            _filePath = filePath;
            if (File.Exists(_filePath))
                File.Delete(_filePath);
        }


   

        public void Handle(Bacteria[,] grid)
        {

            int gridCellsCOunt = grid.GetLength(0)*grid.GetLength(1);
            // Example #4: Append new text to an existing file
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(_filePath, true))
            {

                int paperCount = 0;
                int scissorsCount = 0;
                int stoneCount = 0;
                int emptyCount = 0;

                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    for (int j = 0; j < grid.GetLength(1); j++)
                    {
                        Bacteria currBacteria = grid[i, j];

                        if (currBacteria == null)
                            emptyCount++;
                        else
                        {
                            switch (currBacteria.Type)
                            {
                                case  BacteriaType.C:
                                    paperCount++;
                                    break;
                                case  BacteriaType.B:
                                    scissorsCount++;
                                    break;
                                case  BacteriaType.A:
                                    stoneCount++;
                                    break;
                            }
                        }
                    }
                }
                
                file.WriteLine("{0} {1} {2} {3} {4}",_saveCount, gridCellsCOunt-emptyCount,stoneCount,  scissorsCount, paperCount);

                
                
            }

            _saveCount++;

        }
    }
}