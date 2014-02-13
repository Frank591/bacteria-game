
using BacteriaSurvive.BL;

namespace BacteriaSurvive.UI
{
    class BacteriaSettings
    {
        private Bacteria _bacteria;
        private int _x;
        private int _y;

        public BacteriaSettings(Bacteria bacteria, int x, int y)
        {
            _bacteria = bacteria;
            _x = x;
            _y = y;
        }

        public Bacteria Bacteria
        {
            get { return _bacteria; }
        }

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }
    }
}
