
using BacteriaSurvive.BL;

namespace BacteriaSurvive.UI
{
    class BacteriaCoordinates
    {
        private Bacteria _bacteria;
        private uint _x;
        private uint _y;

        public BacteriaCoordinates(Bacteria bacteria, uint x, uint y)
        {
            _bacteria = bacteria;
            _x = x;
            _y = y;
        }

        public Bacteria Bacteria
        {
            get { return _bacteria; }
        }

        public uint X
        {
            get { return _x; }
        }

        public uint Y
        {
            get { return _y; }
        }
    }
}
