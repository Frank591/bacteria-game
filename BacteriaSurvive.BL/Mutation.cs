namespace BacteriaSurvive.BL
{
    public class Mutation
    {
        private int _attribute1;
        private int _attribute2;
        private int _attribute3;

        public Mutation(int attribute1, int attribute2, int attribute3)
        {
            _attribute1 = attribute1;
            _attribute2 = attribute2;
            _attribute3 = attribute3;
        }

        public int Attribute1
        {
            get { return _attribute1; }
        }

        public int Attribute2
        {
            get { return _attribute2; }
            
        }

        public int Attribute3
        {
            get { return _attribute3; }
        }
    }
}