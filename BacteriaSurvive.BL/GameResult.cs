namespace BacteriaSurvive.BL
{
    public class GameResult
    {
        private int _iterationNumber;
        private BacteriaType? _winner;

        public GameResult(int iterationNumber, BacteriaType? winner)
        {
            _iterationNumber = iterationNumber;
            _winner = winner;
        }

        public int IterationNumber
        {
            get { return _iterationNumber; }
        }

        public BacteriaType? Winner
        {
            get { return _winner; }
        }
    }
}