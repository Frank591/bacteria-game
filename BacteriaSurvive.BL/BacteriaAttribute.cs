namespace BacteriaSurvive.BL
{
    class BacteriaAttribute
    {

        private EBacteriaAttributes _attributeType;
        private int _value;

        public BacteriaAttribute(EBacteriaAttributes attributeType, int value)
        {
            _attributeType = attributeType;
            _value = value;
        }

        public EBacteriaAttributes AttributeType
        {
            get { return _attributeType; }
        }

        public int Value
        {
            get { return _value; }
        }
    }
}