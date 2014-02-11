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
        
        private readonly IGameAreaFactory<GameCenter> _gameCenterAreaFactory;

        private FindGameWinnerStrategy _findGameWinnerStrategy=new FindGameWinnerStrategy();

        private IDictionary<int,BacteriaIncubator> _bacteriaIncubatorsByMutationLimit=new Dictionary<int, BacteriaIncubator>();
        private Random _random;
        private ExpansionManager _expansionManager;


        private FlatGameArea<Bacteria> _bacteriaArea;


        public BacteriaSurviveCalculator(int areaWidth, int areaHeight, uint stepCount, IGridHandler gridHadler, IGameAreaFactory<Bacteria> bacteriaAreaFactory, IGameAreaFactory<GameCenter> gameCenterAreaFactory)
        {
            
            _stepCount = stepCount;
            _gridHadler = gridHadler;
            _gameCenterAreaFactory = gameCenterAreaFactory;
            _bacteriaArea = bacteriaAreaFactory.CreateGameArea();
            _random = new Random();
            _bacteriaIncubatorsByMutationLimit = new Dictionary<int, BacteriaIncubator>();
            _expansionManager=new ExpansionManager(_random);

       
        }


      
     public FlatGameArea<Bacteria> GetArea()
     {
         return _bacteriaArea;
     }


     public void InsertBacteria(Bacteria bacteria, int x, int y)
     {

         if (!_bacteriaArea.IsInArea(x, y)) 
             throw new ArgumentOutOfRangeException("210844AD-6960-4156-BBE6-6AAAABC281BC:пытаемся посадить бактерию за пределы области");
         _bacteriaArea.Grid[x, y] = bacteria;

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
               FlatGameArea<GameCenter> gameCenterArea=_gameCenterAreaFactory.CreateGameArea();
               
               for (int i = 0; i < _bacteriaArea.Width; i++)
               {
                   for (int j = 0; j < _bacteriaArea.Height; j++)
                   {
                       if (!_bacteriaArea.IsInArea(i,j))
                           continue;
                       if (_bacteriaArea.Grid[i, j] == null)
                           continue;

                       Bacteria currentCellBacteria = _bacteriaArea.Grid[i, j];
                       Bacteria childBacteria1 = _bacteriaIncubatorsByMutationLimit[currentCellBacteria.CommonMutationLimit].GenerateChild(currentCellBacteria);

                       if (gameCenterArea.Grid[i, j] == null)
                           gameCenterArea.Grid[i, j] = new GameCenter(_random);
                       gameCenterArea.Grid[i, j].AddPlayer(childBacteria1);


                      

                       GridPoint top = null;
                       int topX = i;
                       int topY = j-1;
                       if (_bacteriaArea.IsInArea(topX,topY))
                           top = new GridPoint(_bacteriaArea.Grid[topX, topY], new Point(topX, topY));

                       GridPoint bot = null;
                       int botX = i;
                       int botY = j + 1;
                       if (_bacteriaArea.IsInArea(botX, botY))
                           bot = new GridPoint(_bacteriaArea.Grid[botX, botY], new Point(botX, botY));

                       GridPoint left = null;
                       int leftX = i-1;
                       int leftY = j;
                       if (_bacteriaArea.IsInArea(leftX, leftY))
                           left = new GridPoint(_bacteriaArea.Grid[leftX, leftY], new Point(leftX, leftY));

                       GridPoint right = null;
                       int rightX = i + 1;
                       int rightY = j;
                       if (_bacteriaArea.IsInArea(rightX, rightY))
                           right = new GridPoint(_bacteriaArea.Grid[rightX, rightY], new Point(rightX, rightY));

                       GridPoint expansionPoint= _expansionManager.GetExpansionPoint(new List<GridPoint>() {top, right, bot, left});

                       Bacteria childBacteria2 = _bacteriaIncubatorsByMutationLimit[currentCellBacteria.CommonMutationLimit].Clone(childBacteria1);

                       if (gameCenterArea.Grid[expansionPoint.Coordinate.X, expansionPoint.Coordinate.Y]==null)
                           gameCenterArea.Grid[expansionPoint.Coordinate.X, expansionPoint.Coordinate.Y] = new GameCenter(_random);
                       gameCenterArea.Grid[expansionPoint.Coordinate.X, expansionPoint.Coordinate.Y].AddPlayer(childBacteria2);


                   }
                   
               }



               _gridHadler.Handle(_bacteriaArea.Grid);
               gameWinnerBacteriaType = _findGameWinnerStrategy.GetWinner(_bacteriaArea.Grid);
               if (gameWinnerBacteriaType.HasValue)
               {
                   return new GameResult(step, gameWinnerBacteriaType);
               }

               for (int i = 0; i < _bacteriaArea.Width; i++)
               {
                   for (int j = 0; j < _bacteriaArea.Height; j++)
                   {
                       _bacteriaArea.Grid[i, j] = null;
                   }
               }



               for (int i = 0; i < _bacteriaArea.Width; i++)
               {
                   for (int j = 0; j < _bacteriaArea.Height; j++)
                   {

                       GameCenter currGameCenter = gameCenterArea.Grid[i, j];
                       if (currGameCenter != null)
                       {
                           Bacteria winner = currGameCenter.Play();
                           _bacteriaArea.Grid[i, j] = winner;
                       }

                   }
               }
           }


           _gridHadler.Handle(_bacteriaArea.Grid);
           gameWinnerBacteriaType = _findGameWinnerStrategy.GetWinner(_bacteriaArea.Grid);
           if (gameWinnerBacteriaType.HasValue)
           {
               return new GameResult((int)_stepCount, gameWinnerBacteriaType);
           }
           return new GameResult((int)_stepCount, null);
       }
    }
}
