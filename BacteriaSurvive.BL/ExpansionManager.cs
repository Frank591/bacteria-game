using System;
using System.Collections.Generic;
using System.Linq;

namespace BacteriaSurvive.BL
{
    public class ExpansionManager
    {
        private readonly Random _random;

        public ExpansionManager(Random random)
        {
            _random = random;
        }

      

        public GridPoint GetExpansionPoint(IList<GridPoint> allCoordinates)
        {
            IList<GridPoint> availablePoints = new List<GridPoint>();

            GridPoint result = null;
            foreach (GridPoint bacteriaCoordinate in allCoordinates)
            {
                if (bacteriaCoordinate!=null)
                    availablePoints.Add(bacteriaCoordinate);
            }


            IList<GridPoint> emptyPoints = availablePoints.Where(coord=>coord.Bacteria==null).ToList();
            if (emptyPoints.Count > 0)
            {
                int resultIndex = _random.Next(0, emptyPoints.Count);
                result = emptyPoints[resultIndex];
            }
            else
            {
                int resultIndex = _random.Next(0, availablePoints.Count);
                result = availablePoints[resultIndex];
            }

            return result;

        }
    }
}