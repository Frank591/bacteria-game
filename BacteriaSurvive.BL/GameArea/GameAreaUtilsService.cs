
using System;
using System.Collections.Generic;
using System.Drawing;

namespace BacteriaSurvive.BL.GameArea
{
    public static class GameAreaUtilsService
    {
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
                for (int x = minX + 1; x < maxX; x++)
                {
                    for (int y = minY + 1; y < maxY; y++)
                    {
                        if (y == Math.Round(k * x + b))
                        {
                            linePoints.Add(new Point(x, y));
                        }
                    }
                }
            }

            return linePoints;
        }


        public static bool IsContainsInPoints(Point target, IList<Point> points)
        {
            return points.Contains(target);
        }
    }
}
