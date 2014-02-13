using System.Collections.Generic;
using System.Drawing;


namespace BacteriaSurvive.UI
{
    

    class CalculationParams
    {
        public delegate void GameCalculationEndHandler();





        private uint _gamesCount;
        
        private uint _areaWidth;
        private uint _areaHeight;
        private uint _stepCount;
        private readonly IList<Point> _nodes;
        private string _statisticResultsDir;
        private bool _isCountSaverEnabled;
        private bool _isVectorSaverEnabled;
        private bool _isGridSaverEnabled;


        public GameCalculationEndHandler GameCalculationEnd;
        private IList<BacteriaCoordinates> _players;


        public CalculationParams(uint gamesCount, uint areaWidth, uint areaHeight, uint stepCount, IList<Point> nodes,  string statisticResultsDir, bool isCountSaverEnabled, bool isVectorSaverEnabled, bool isGridSaverEnabled, IList<BacteriaCoordinates> players)
        {
            _gamesCount = gamesCount;
            _areaWidth = areaWidth;
            _areaHeight = areaHeight;
            _stepCount = stepCount;
            _nodes = nodes;
            _statisticResultsDir = statisticResultsDir;
            _players = players;
            
      
            _isCountSaverEnabled = isCountSaverEnabled;
            _isVectorSaverEnabled = isVectorSaverEnabled;
            _isGridSaverEnabled = isGridSaverEnabled;
        }

        public void RaiseGameCalculationEndEvent()
        {
            if (GameCalculationEnd != null)
                GameCalculationEnd();
        }

        public uint GamesCount
        {
            get { return _gamesCount; }
        }

        public uint AreaWidth
        {
            get { return _areaWidth; }
        }

        public uint AreaHeight
        {
            get { return _areaHeight; }
        }

        public uint StepCount
        {
            get { return _stepCount; }
        }

        public string StatisticResultsDir
        {
            get { return _statisticResultsDir; }
            
        }

        public IList<BacteriaCoordinates> Players
        {
            get { return _players; }
        }

        public bool IsCountSaverEnabled
        {
            get { return _isCountSaverEnabled; }
        }

        public bool IsVectorSaverEnabled
        {
            get { return _isVectorSaverEnabled; }
        }

        public bool IsGridSaverEnabled
        {
            get { return _isGridSaverEnabled; }
        }

       


        public IList<Point> Nodes
        {
            get { return _nodes; }
        }
    }
}
