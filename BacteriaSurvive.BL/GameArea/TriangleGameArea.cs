using System;
using System.Drawing;

namespace BacteriaSurvive.BL.GameArea
{
    public class TriangleGameArea<T>:FlatGameArea<T> where T:class 
    {
        public TriangleGameArea(int width, int height,  Point point) : base(width, height)
        {
        }

        public override bool IsInArea(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
