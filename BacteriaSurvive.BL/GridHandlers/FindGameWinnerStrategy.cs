namespace BacteriaSurvive.BL.GridHandlers
{
    public class FindGameWinnerStrategy
    {

   

        private BacteriaType? GetWinAttribute(int stoneCount, int paperCount, int scissorsCount)
        {
            if ((paperCount == 0) && (stoneCount == 0) && (scissorsCount > 1))
                return BacteriaType.B;

            if ((paperCount == 0) && (stoneCount > 1) && (scissorsCount == 0))
                return BacteriaType.A;

            if ((paperCount > 1) && (stoneCount == 0) && (scissorsCount == 0))
                return BacteriaType.C;
            return null;
        }



        public BacteriaType? GetWinner(Bacteria[,] grid)
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
                            case BacteriaType.C:
                                paperCount++;
                                break;
                            case BacteriaType.B:
                                scissorsCount++;
                                break;
                            case BacteriaType.A:
                                stoneCount++;
                                break;
                        }
                    }
                }
            }
            BacteriaType? winBacteriaType = GetWinAttribute(stoneCount, paperCount, scissorsCount);
            return winBacteriaType;
        }
    }
}
