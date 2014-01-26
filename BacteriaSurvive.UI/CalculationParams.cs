using System;
using System.Collections.Generic;


namespace BacteriaSurvive.UI
{
    

    class CalculationParams
    {
        public delegate void GameCalculationEndHandler();



        private string _vectorSaverResultsDir;
        private string _countSaverResultsDir;
        private string _gridSaverResultsDir;



        private uint _gamesStartIndex;
        private uint _gamesEndIndex;
        private uint _areaWidth;
        private uint _areaHeight;
        private uint _stepCount;
        private string _statisticResultsDir;
        private bool _isCountSaverEnabled;
        private bool _isVectorSaverEnabled;
        private bool _isGridSaverEnabled;

        public GameCalculationEndHandler GameCalculationEnd;
        private IList<BacteriaCoordinates> _players;


        public CalculationParams(uint gamesStartIndex, uint areaWidth, uint areaHeight, uint stepCount, string statisticResultsDir, bool isCountSaverEnabled, bool isVectorSaverEnabled, bool isGridSaverEnabled, IList<BacteriaCoordinates> players, uint gamesEndIndex, string vectorSaverResultsDir, string countSaverResultsDir, string gridSaverResultsDir)
        {
            _gamesStartIndex = gamesStartIndex;
            _areaWidth = areaWidth;
            _areaHeight = areaHeight;
            _stepCount = stepCount;
            _statisticResultsDir = statisticResultsDir;
            _players = players;
            _gamesEndIndex = gamesEndIndex;
            _vectorSaverResultsDir = vectorSaverResultsDir;
            _countSaverResultsDir = countSaverResultsDir;
            _gridSaverResultsDir = gridSaverResultsDir;
            _isCountSaverEnabled = isCountSaverEnabled;
            _isVectorSaverEnabled = isVectorSaverEnabled;
            _isGridSaverEnabled = isGridSaverEnabled;

            if (_gamesEndIndex <= _gamesStartIndex)
                throw new ArgumentOutOfRangeException( "22E95ACC-7B99-4605-AB00-B768B946A2D0: game start index>= game end index");

        }

        public void RaiseGameCalculationEndEvent()
        {
            if (GameCalculationEnd != null)
                GameCalculationEnd();
        }

        public uint GamesStartIndex
        {
            get { return _gamesStartIndex; }
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

        public uint GamesEndIndex
        {
            get { return _gamesEndIndex; }
        }

        public string VectorSaverResultsDir
        {
            get { return _vectorSaverResultsDir; }
        }

        public string CountSaverResultsDir
        {
            get { return _countSaverResultsDir; }
        }

        public string GridSaverResultsDir
        {
            get { return _gridSaverResultsDir; }
        }
    }
}
