using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BacteriaSurvive.BL;
using BacteriaSurvive.BL.GridHandlers;

namespace BacteriaSurvive.UI
{
    class CalculationParams
    {

        private uint _gamesCount;
        private uint _areaWidth;
        private uint _areaHeight;
        private uint _stepCount;
        private string _resultsDir;
        private bool _isCountSaverEnabled;
        private bool _isVectorSaverEnabled;
        private bool _isGridSaverEnabled;

        
        private IList<BacteriaCoordinates> _players;


        public CalculationParams(uint gamesCount, uint areaWidth, uint areaHeight, uint stepCount, string resultsDir, bool isCountSaverEnabled, bool isVectorSaverEnabled, bool isGridSaverEnabled, IList<BacteriaCoordinates> players)
        {
            _gamesCount = gamesCount;
            _areaWidth = areaWidth;
            _areaHeight = areaHeight;
            _stepCount = stepCount;
            _resultsDir = resultsDir;
            _players = players;
            _isCountSaverEnabled = isCountSaverEnabled;
            _isVectorSaverEnabled = isVectorSaverEnabled;
            _isGridSaverEnabled = isGridSaverEnabled;
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

        public string ResultsDir
        {
            get { return _resultsDir; }
            
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
    }
}
