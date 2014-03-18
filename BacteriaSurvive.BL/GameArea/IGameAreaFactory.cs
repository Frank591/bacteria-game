

namespace BacteriaSurvive.BL.GameArea
{
    public interface IGameAreaFactory<T> where T:class 
    {
        FlatGameArea<T> CreateGameArea();
    }
}
