using System.Collections.Generic;

namespace BacteriaSurvive.BL.GameArea
{
    public abstract class FlatGameArea<T> where T:class 
    {
        public T[,] Grid;
        public int Width { get; set; }
        public int Height { get; set; }
        public abstract bool IsInArea(int x, int y);

        protected FlatGameArea(int width, int height)
        {
            Width = width;
            Height = height;
            Grid=new T[Width,Height];
        }
    }
}
