namespace Aay11
{
    public class FuelCell
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int SerialNumber { get; private set; }

        public FuelCell(int x, int y, int serialNumber)
        {
            this.X = x;
            this.Y = y;
            this.SerialNumber = serialNumber;
        }

        private int? _powerLevel = null;
        public int PowerLevel
        {
            get
            {
                if (_powerLevel == null || !_powerLevel.HasValue)
                {
                    int rackId = X + 10;
                    int start = ((Y * rackId) + SerialNumber) * rackId;

                    if (start < 100)
                    {
                        _powerLevel = -5;
                    }
                    else
                    {
                        _powerLevel = ((start / 100) % 10) - 5;
                    }
                }

                return _powerLevel.Value;
            }
        }
    }
}
