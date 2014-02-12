
using System.Collections.Generic;
using System.Drawing;

namespace BacteriaSurvive.BL.GameArea
{
    public class PolygonGameAreaFactory<T> : IGameAreaFactory<T> where T : class
    {
        private readonly int _width;
        private readonly int _height;
        private readonly IList<Point> _nodes;

        public PolygonGameAreaFactory(int width, int height, IList<Point> nodes)
        {
            _width = width;
            _height = height;
            _nodes = nodes;
        }

        public FlatGameArea<T> CreateGameArea()
        {
            return new PolygonGameArea<T>(_width, _height,_nodes);
        }
    }
}
