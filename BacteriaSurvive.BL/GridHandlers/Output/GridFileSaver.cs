using System;
using System.IO;

namespace BacteriaSurvive.BL.GridHandlers.Output
{
    public class GridFileSaver : IGridHandler
    {
        private string _folderPath;

        private int _saveCount = 0;


        public GridFileSaver(string folderPath)
        {
            _folderPath = folderPath;
            if (!Directory.Exists(_folderPath))
                throw new DirectoryNotFoundException(" �� ������� ��������� "+_folderPath);
            string calculateResultsFolderName = "FileGridSaver_������� �� " + DateTime.Now.ToString("yyyyMMdd hh_mm");
            _folderPath = Path.Combine(_folderPath, calculateResultsFolderName);
            Directory.CreateDirectory(_folderPath);
        }


    

        public void Handle(Bacteria[,] grid)
        {

            string filePath = Path.Combine(_folderPath, "step" + _saveCount + ".txt");
            // Example #4: Append new text to an existing file
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, true))
            {
                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    for (int j = 0; j < grid.GetLength(1); j++)
                    {
                        Bacteria bacteria = grid[i, j];

                        if (bacteria==null)
                            file.Write("0 ");
                        else
                        {
                            file.Write((int)grid[i, j].Type +" ");    
                        }
                    }
                    file.WriteLine();
                }
            }

            _saveCount++;

        }
    }
}