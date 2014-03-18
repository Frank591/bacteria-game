namespace BacteriaSurvive.BL.GameArea
{
    public class SquareArea<T> : FlatGameArea<T>  where T : class 
    {
        

        public SquareArea(int width, int height):base(width,height)
        {
            
        }
        
        public override bool IsInArea(int x, int y)
        {
            if ((x<0)||(x>Width-1))
            return false;
            if ((y < 0) || (y > Height-1))
                return false;
            return true;
        }
    }
}
