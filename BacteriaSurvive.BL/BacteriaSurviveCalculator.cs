using System;
using System.Collections.Generic;
using System.Drawing;
using BacteriaSurvive.BL.GameArea;
using BacteriaSurvive.BL.GridHandlers;

namespace BacteriaSurvive.BL
{
    public class BacteriaSurviveCalculator
    {
        private readonly uint _stepCount;
        private readonly IGridHandler _gridHadler;

        private FindGameWinnerStrategy _findGameWinnerStrategy=new FindGameWinnerStrategy();

        private IDictionary<int,BacteriaIncubator> _bacteriaIncubatorsByMutationLimit=new Dictionary<int, BacteriaIncubator>();
        private Random _random;
        private ExpansionManager _expansionManager;


        private SquareArea<Bacteria> _area;


        public BacteriaSurviveCalculator(int areaWidth, int areaHeight, uint stepCount, IGridHandler gridHadler)
        {
            
            _stepCount = stepCount;
            _gridHadler = gridHadler;
            _area=new SquareArea<Bacteria>(areaWidth,areaHeight);
            _random = new Random();
            _bacteriaIncubatorsByMutationLimit = new Dictionary<int, BacteriaIncubator>();
            _expansionManager=new ExpansionManager(_random);
            


         /*   for (int i = 0; i < areaWidth; i++)
            {
                for (int j = 0; j < areaHeight; j++)
                {
                    _grid[i, j] = null;
                }
            }*/
        }


      
     public FlatGameArea<Bacteria> GetArea()
     {
         return _area;
     }


     public void InsertBacteria(Bacteria bacteria, int x, int y)
     {

         if (!_area.IsInArea(x,y)) 
             throw new ArgumentOutOfRangeException("210844AD-6960-4156-BBE6-6AAAABC281BC:пытаемся посадить бактерию за пределы области");
         _area.Grid[x, y] = bacteria;

         if (!_bacteriaIncubatorsByMutationLimit.ContainsKey(bacteria.CommonMutationLimit))
         {
             _bacteriaIncubatorsByMutationLimit.Add(bacteria.CommonMutationLimit, new BacteriaIncubator(bacteria.CommonMutationLimit, _random));
         }

     }




       public GameResult EvaluteGrid()
       {

           BacteriaType? gameWinnerBacteriaType;

           for (int step = 0; step < _stepCount; step++)
           {
               FlatGameArea<GameCenter> gameCenterArea=new SquareArea<GameCenter>(_area.Width, _area.Height);
               
               for (int i = 0; i < _area.Width; i++)
               {
                   for (int j = 0; j < _area.Height; j++)
                   {
                       if (_area.Grid[i,j]==null)
                           continue;

                       Bacteria currentCellBacteria = _area.Grid[i, j];
                       Bacteria childBacteria1 = _bacteriaIncubatorsByMutationLimit[currentCellBacteria.CommonMutationLimit].GenerateChild(currentCellBacteria);

                       if (gameCenterArea.Grid[i, j] == null)
                           gameCenterArea.Grid[i, j] = new GameCenter(_random);
                       gameCenterArea.Grid[i, j].AddPlayer(childBacteria1);


                      

                       GridPoint top = null;
                       int topX = i;
                       int topY = j-1;
                       if (topY >= 0)
                           top = new GridPoint(_area.Grid[topX, topY], new Point(topX, topY));

                       GridPoint bot = null;
                       int botX = i;
                       int botY = j + 1;
                       if (botY <_area.Height)
                           bot = new GridPoint(_area.Grid[botX, botY], new Point(botX, botY));

                       GridPoint left = null;
                       int leftX = i-1;
                       int leftY = j;
                       if (leftX >= 0)
                           left = new GridPoint(_area.Grid[leftX, leftY], new Point(leftX, leftY));

                       GridPoint right = null;
                       int rightX = i + 1;
                       int rightY = j;
                       if (rightX < _area.Width)
                           right = new GridPoint(_area.Grid[rightX, rightY], new Point(rightX, rightY));

                       GridPoint expansionPoint= _expansionManager.GetExpansionPoint(new List<GridPoint>() {top, right, bot, left});

                       Bacteria childBacteria2 = _bacteriaIncubatorsByMutationLimit[currentCellBacteria.CommonMutationLimit].Clone(childBacteria1);

                       if (gameCenterArea.Grid[expansionPoint.Coordinate.X, expansionPoint.Coordinate.Y]==null)
                           gameCenterArea.Grid[expansionPoint.Coordinate.X, expansionPoint.Coordinate.Y] = new GameCenter(_random);
                       gameCenterArea.Grid[expansionPoint.Coordinate.X, expansionPoint.Coordinate.Y].AddPlayer(childBacteria2);


                   }
                   
               }



               _gridHadler.Handle(_area.Grid);
               gameWinnerBacteriaType = _findGameWinnerStrategy.GetWinner(_area.Grid);
               if (gameWinnerBacteriaType.HasValue)
               {
                   return new GameResult(step, gameWinnerBacteriaType);
               }

               for (int i = 0; i < _area.Width; i++)
               {
                   for (int j = 0; j < _area.Height; j++)
                   {
                       _area.Grid[i, j] = null;
                   }
               }



               for (int i = 0; i < _area.Width; i++)
               {
                   for (int j = 0; j < _area.Height; j++)
                   {

                       GameCenter currGameCenter = gameCenterArea.Grid[i, j];
                       if (currGameCenter != null)
                       {
                           Bacteria winner = currGameCenter.Play();
                           _area.Grid[i, j] = winner;
                       }

                   }
               }
           }


           _gridHadler.Handle(_area.Grid);
           gameWinnerBacteriaType = _findGameWinnerStrategy.GetWinner(_area.Grid);
           if (gameWinnerBacteriaType.HasValue)
           {
               return new GameResult((int)_stepCount, gameWinnerBacteriaType);
           }
           return new GameResult((int)_stepCount, null);
       }
    }
}
