
namespace BacteriaSurvive.BL.GameArea
{
    public class SquareGameAreaFactory<T>:IGameAreaFactory<T> where T:class 
    {
        private readonly int _width;
        private readonly int _height;

        public SquareGameAreaFactory(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public FlatGameArea<T> CreateGameArea()
        {
            return new SquareArea<T>(_width, _height);
        }
    }
}
