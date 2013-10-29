using System;
using System.Collections.Generic;
using System.Drawing;
using BacteriaSurvive.BL.GridHandlers;

namespace BacteriaSurvive.BL
{
    public class BacteriaSurviveCalculator
    {
        private readonly uint _areaWidth;
        private readonly uint _areaHeight;
        private readonly uint _stepCount;
        private readonly IGridHandler _gridHadler;

        private FindGameWinnerStrategy _findGameWinnerStrategy=new FindGameWinnerStrategy();

        private IDictionary<int,BacteriaIncubator> _bacteriaIncubatorsByMutationLimit=new Dictionary<int, BacteriaIncubator>();
        private Random _random;
        private ExpansionManager _expansionManager;


        private Bacteria[,] _grid;


        public BacteriaSurviveCalculator(uint areaWidth, uint areaHeight, uint stepCount, IGridHandler gridHadler)
        {
            _areaWidth = areaWidth;
            _areaHeight = areaHeight;
            _stepCount = stepCount;
            _gridHadler = gridHadler;
            _grid=new Bacteria[_areaWidth,_areaHeight];
            _random = new Random();
            _bacteriaIncubatorsByMutationLimit = new Dictionary<int, BacteriaIncubator>();
            _expansionManager=new ExpansionManager(_random);
            


            for (int i = 0; i < areaWidth; i++)
            {
                for (int j = 0; j < areaHeight; j++)
                {
                    _grid[i, j] = null;
                }
            }
        }


      
     public Bacteria[,] GetGrid()
     {
         return _grid;
     }


     public void InsertBacteria(Bacteria bacteria, uint x, uint y)
     {
         if ((x>_areaWidth) || (y>_areaHeight)) 
             throw new ArgumentOutOfRangeException("210844AD-6960-4156-BBE6-6AAAABC281BC:пытаемся посадить бактерию за пределы области");
         _grid[x, y] = bacteria;

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
               GameCenter[,] gameCenterGrid = new GameCenter[_areaWidth, _areaHeight];
               for (int i = 0; i < _areaWidth; i++)
               {
                   for (int j = 0; j < _areaHeight; j++)
                   {
                       if (_grid[i,j]==null)
                           continue;

                       Bacteria currentCellBacteria = _grid[i, j];
                       Bacteria childBacteria1 = _bacteriaIncubatorsByMutationLimit[currentCellBacteria.CommonMutationLimit].GenerateChild(currentCellBacteria);

                       if (gameCenterGrid[i,j]==null)
                           gameCenterGrid[i,j]=new GameCenter(_random);
                       gameCenterGrid[i, j].AddPlayer(childBacteria1);


                      

                       GridPoint top = null;
                       int topX = i;
                       int topY = j-1;
                       if (topY >= 0)
                           top = new GridPoint(_grid[topX,topY], new Point(topX, topY));

                       GridPoint bot = null;
                       int botX = i;
                       int botY = j + 1;
                       if (botY <_areaHeight)
                           bot = new GridPoint(_grid[botX, botY], new Point(botX, botY));

                       GridPoint left = null;
                       int leftX = i-1;
                       int leftY = j;
                       if (leftX >= 0)
                           left = new GridPoint(_grid[leftX, leftY], new Point(leftX, leftY));

                       GridPoint right = null;
                       int rightX = i + 1;
                       int rightY = j;
                       if (rightX < _areaWidth)
                           right = new GridPoint(_grid[rightX, rightY], new Point(rightX, rightY));

                       GridPoint expansionPoint= _expansionManager.GetExpansionPoint(new List<GridPoint>() {top, right, bot, left});

                       Bacteria childBacteria2 = _bacteriaIncubatorsByMutationLimit[currentCellBacteria.CommonMutationLimit].Clone(childBacteria1);

                       if (gameCenterGrid[expansionPoint.Coordinate.X, expansionPoint.Coordinate.Y]==null)
                           gameCenterGrid[expansionPoint.Coordinate.X, expansionPoint.Coordinate.Y]=new GameCenter(_random);
                       gameCenterGrid[expansionPoint.Coordinate.X, expansionPoint.Coordinate.Y].AddPlayer(childBacteria2);


                   }
                   
               }



               _gridHadler.Handle(_grid);
               gameWinnerBacteriaType = _findGameWinnerStrategy.GetWinner(_grid);
               if (gameWinnerBacteriaType.HasValue)
               {
                   return new GameResult(step, gameWinnerBacteriaType);
               }

               for (int i = 0; i < _areaWidth; i++)
               {
                   for (int j = 0; j < _areaHeight; j++)
                   {
                       _grid[i, j] = null;
                   }
               }



               for (int i = 0; i < _areaWidth; i++)
               {
                   for (int j = 0; j < _areaHeight; j++)
                   {

                       GameCenter currGameCenter = gameCenterGrid[i, j];
                       if (currGameCenter != null)
                       {
                           Bacteria winner = currGameCenter.Play();
                           _grid[i, j] = winner;
                       }

                   }
               }
           }


           _gridHadler.Handle(_grid);
           gameWinnerBacteriaType = _findGameWinnerStrategy.GetWinner(_grid);
           if (gameWinnerBacteriaType.HasValue)
           {
               return new GameResult((int)_stepCount, gameWinnerBacteriaType);
           }
           return new GameResult((int)_stepCount, null);
       }
    }
}
