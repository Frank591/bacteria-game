using System;
using System.Collections.Generic;
using System.Drawing;

namespace BacteriaSurvive.BL.GameArea
{
    public class TestGameArea<T>:FlatGameArea<T> where T:class 
    {
        private readonly IList<Point> _nodePoints;
        private readonly IList<Point> _boundaryPoints;

        public TestGameArea(int width, int height, IList<Point> nodePoints)
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
            if ((x<0)||(x>Width))
                throw new ArgumentOutOfRangeException("071B2775-8F78-40EB-816F-F6CB7BDBD10E: x<0 or x>area width");
            if ((y < 0) || (y > Height))
                throw new ArgumentOutOfRangeException("071B2775-8F78-40EB-816F-F6CB7BDBD11E: y<0 or y>area height");

         

            // our point is boundary point
            if (GameAreaUtilsService.IsContainsInPoints(new Point(x,y),_boundaryPoints))
                return true;

            
            
            int topY = y + 1;
            IList<Point> topCrossPoints = new List<Point>();
            
            IList<Point> crossPoints = new List<Point>();

            int botY = y - 1;
            IList<Point> botCrossPoints = new List<Point>();

            for (int xIterator = x; xIterator <= Width; xIterator++)
            {
                Point currTopPoint = new Point(xIterator, topY);

                if (_boundaryPoints.Contains(currTopPoint))
                {
                    topCrossPoints.Add(currTopPoint);
                }

                Point currRayPoint = new Point(xIterator, y);
                if (_boundaryPoints.Contains(currRayPoint))
                {
                    crossPoints.Add(currRayPoint);
                }

                Point currBotPoint = new Point(xIterator, botY);
                if (_boundaryPoints.Contains(currBotPoint))
                {
                    botCrossPoints.Add(currBotPoint);
                }
            }
            if (crossPoints.Count%2 == 0)
                return false;

            if ((topCrossPoints.Count>0) && (botCrossPoints.Count>0))
                return true;
            return false;
        }
    }
}
