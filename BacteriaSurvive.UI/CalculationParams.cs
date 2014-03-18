using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using BacteriaSurvive.BL;
using BacteriaSurvive.BL.GridHandlers;

namespace BacteriaSurvive.UI
{
    class CalculationParams
    {


        private uint _gamesStartIndex;
        private uint _gamesEndIndex;
        private uint _areaWidth;
        private uint _areaHeight;
        private uint _stepCount;
        private readonly IList<Point> _nodes;
        private string _resultsDir;
        private bool _isCountSaverEnabled;
        private bool _isVectorSaverEnabled;
        private bool _isGridSaverEnabled;


        private IList<BacteriaSettings> _players;


        public CalculationParams(uint gamesStartIndex, uint gamesEndIndex, uint areaWidth, uint areaHeight, uint stepCount, IList<Point> nodes, string resultsDir, bool isCountSaverEnabled, bool isVectorSaverEnabled, bool isGridSaverEnabled, IList<BacteriaSettings> players)
        {
            _gamesStartIndex = gamesStartIndex;
            _gamesEndIndex = gamesEndIndex;
            _areaWidth = areaWidth;
            _areaHeight = areaHeight;
            _stepCount = stepCount;
            _nodes = nodes;
            _resultsDir = resultsDir;
            _players = players;
            _isCountSaverEnabled = isCountSaverEnabled;
            _isVectorSaverEnabled = isVectorSaverEnabled;
            _isGridSaverEnabled = isGridSaverEnabled;
        }

        public uint GamesStartIndex
        {
            get { return _gamesStartIndex; }
        }

        public uint GamesEndIndex
        {
            get { return _gamesEndIndex; }
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

        public IList<BacteriaSettings> Players
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
