
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace BacteriaSurvive.BL.GameArea
{
    public static class GameAreaUtilsService
    {
        //distance from point to line can be less or equal than 0.5 (distanceToPoine<=0.5)
        private  const double MAX_DISTANCE_TO_LINE = 0.5;

        public static IList<Point> GetLinePoints(Point point1, Point point2)
        {

            if (point1.Equals(point2))
                throw new ArgumentException("73C3B856-C2C8-404E-AED1-55AF69F0C1BF: equal node points");

            double yCurr = point1.Y;
            double yNext = point2.Y;
            double xCurr = point1.X;
            double xNext = point2.X;

            double k = (yCurr - yNext) / (xCurr - xNext);
            double b = yNext - xNext * k;

            int minX = Math.Min((int)xCurr, (int)xNext);
            int maxX = Math.Max((int)xCurr, (int)xNext);

            int minY = Math.Min((int)yCurr, (int)yNext);
            int maxY = Math.Max((int)yCurr, (int)yNext);


            IList<Point> linePoints = new List<Point>();

            if (minX == maxX)
            {
                for (int y = minY + 1; y < maxY; y++)
                {
                     linePoints.Add(new Point(minX, y));
                }
            }
            else if (minY==maxY)
            {
                for (int x = minX + 1; x < maxX; x++)
                {
                    linePoints.Add(new Point(x, minY));
                }
            }
            else
            {
                for (int x = minX; x <= maxX; x++)
                {
                    for (int y = minY; y <= maxY; y++)
                    {

                        double distanceToTheLine = (Math.Abs(k*x - y + b))/(Math.Sqrt(k*k + 1));

                        if (distanceToTheLine<MAX_DISTANCE_TO_LINE)
                        {
                            linePoints.Add(new Point(x, y));
                        }
                    }
                }
            }

            linePoints.Remove(point1);
            linePoints.Remove(point2);
            return linePoints;
        }


        public static bool IsContainsInPoints(Point target, IList<Point> points)
        {
            return points.Contains(target);
        }

        public static IList<Point> GetPointsByX(IList<Point> source, int x)
        {
            return source.Where(p => p.X == x).ToList();
        }

        public static IList<Point> GetPointsByY(IList<Point> source, int y)
        {
            return source.Where(p => p.Y == y).ToList();
        } 

     

        public static int GetMaxXStep(IList<Point> points)
        {
            int maxXStep = 0;
            if (points.Count<2)
                return maxXStep;

            IList<Point> orderedPoints=points.OrderBy(p => p.X).ToList();
            

            for (int i = 0; i < orderedPoints.Count-1; i++)
            {
                Point currPoint = orderedPoints[i];
                if (currPoint.X<0)
                    throw new ArgumentOutOfRangeException("E71BC87E-D17E-41CB-939E-26E3695939A5: x<0");
                Point nextPoint = orderedPoints[i + 1];
                if (nextPoint.X < 0)
                    throw new ArgumentOutOfRangeException("E71BC87E-D17E-41CB-939E-26E3695939A6: x<0");

                int currXStep = Math.Abs(currPoint.X - nextPoint.X );
                maxXStep = Math.Max(currXStep, maxXStep);
            }

            return maxXStep;
        }

    }
}
