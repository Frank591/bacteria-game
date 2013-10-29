using System.Drawing;

namespace BacteriaSurvive.BL
{
    public class GridPoint
    {
        public Bacteria Bacteria { get; private set; }
        public Point Coordinate { get; private set; }

        public GridPoint(Bacteria bacteria, Point coordinate)
        {
            Bacteria = bacteria;
            Coordinate = coordinate;
        }
    }
}