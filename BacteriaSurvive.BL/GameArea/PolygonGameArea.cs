using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace BacteriaSurvive.BL.GameArea
{
    public class PolygonGameArea<T>:FlatGameArea<T> where T:class 
    {
        private readonly IList<Point> _nodePoints;
        private readonly IList<Point> _boundaryPoints;
        private readonly Point _originPoint=new Point(0,0);
        private const int MIN_OUTER_POINTS_WITH_SAME_COORDINATE_FOR_INNER_POINT = 2; 

        public PolygonGameArea(int width, int height, IList<Point> nodePoints)
            : base(width, height)
        {
            _nodePoints = nodePoints;
            _boundaryPoints=GetBoundaryPoints(_nodePoints);
        }


        private IList<Point> GetBoundaryPoints(IList<Point> nodePoints)
        {
            IList<Point> result=new List<Point>();

            for (int i = 0; i < nodePoints.Count; i++)
            {
                if (i > nodePoints.Count - 2)
                    continue;
                Point currPoint = nodePoints[i];
                Point nextPoint = nodePoints[i + 1];

                result.Add(currPoint);
                IList<Point> currLinePoints = GameAreaUtilsService.GetLinePoints(currPoint, nextPoint);
                foreach (var currLinePoint in currLinePoints)
                {
                    result.Add(currLinePoint);
                }
            }

            Point lastPoint = nodePoints[nodePoints.Count - 1];
            Point firstPoint = nodePoints[0];

            result.Add(lastPoint);
            IList<Point> lastLinePoints = GameAreaUtilsService.GetLinePoints(lastPoint, firstPoint);
            foreach (var currLinePoint in lastLinePoints)
            {
                result.Add(currLinePoint);
            }
            return result;
        }

        public override bool IsInArea(int x, int y)
        { 
            if ((x<_originPoint.X)||(x>Width))
                throw new ArgumentOutOfRangeException("071B2775-8F78-40EB-816F-F6CB7BDBD10E: x<0 or x>area width");
            if ((y < _originPoint.Y) || (y > Height))
                throw new ArgumentOutOfRangeException("071B2775-8F78-40EB-816F-F6CB7BDBD11E: y<0 or y>area height");

         

            // our point is boundary point
            if (GameAreaUtilsService.IsContainsInPoints(new Point(x,y),_boundaryPoints))
                return true;


            IList<Point> boundaryPointsWithSameX = GameAreaUtilsService.GetPointsByX(_boundaryPoints, x);
            if (boundaryPointsWithSameX.Count < MIN_OUTER_POINTS_WITH_SAME_COORDINATE_FOR_INNER_POINT)
                return false;
            int minY = boundaryPointsWithSameX.Min(p => p.Y);
            int maxY = boundaryPointsWithSameX.Max(p => p.Y);
            bool isYBetween = (minY < y) && (y < maxY);

            IList<Point> boundaryPointsWithSameY = GameAreaUtilsService.GetPointsByY(_boundaryPoints, y);
            if (boundaryPointsWithSameY.Count < MIN_OUTER_POINTS_WITH_SAME_COORDINATE_FOR_INNER_POINT)
                return false;
            int minX = boundaryPointsWithSameY.Min(p => p.X);
            int maxX = boundaryPointsWithSameY.Max(p => p.X);
            bool isXBetween = (minX < x) && (x < maxX);


            return isXBetween&&isYBetween;
        }
    }
}
