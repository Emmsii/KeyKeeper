using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Extensions
{
    public static class PointExtensions
    {

        public static Point Normalize(this Point point)
        {
            Point p = new Point();
            double length = Math.Sqrt(point.X * point.X + point.Y * point.Y);
            if(length != 0)
            {
                p.X = (int)Math.Round(point.X / length);
                p.Y = (int)Math.Round(point.Y / length);
            }

            return p;
        }

        public static Point DirectionTo(this Point point, Point target)
        {
            return new Point(target.X - point.X, target.Y - point.Y);
        }

        public static float DistanceTo(this Point point1, Point point2)
        {
            float sub1 = point1.X - point2.X, sub2 = point1.Y - point2.Y;
            return (float)Math.Sqrt(sub1 * sub1 + sub2 * sub2);
        }

        public static IEnumerable<Point> NeighboursCardinal(this Point point)
        {
            yield return new Point(point.X - 1, point.Y);
            yield return new Point(point.X + 1, point.Y);
            yield return new Point(point.X, point.Y + 1);
            yield return new Point(point.X, point.Y - 1);
        }

        public static IEnumerable<Point> NeighboursDiagonal(this Point point)
        {
            yield return new Point(point.X - 1, point.Y - 1);
            yield return new Point(point.X + 1, point.Y + 1);
            yield return new Point(point.X + 1, point.Y - 1);
            yield return new Point(point.X - 1, point.Y + 1);
        }

        public static IEnumerable<Point> NeighboursAll(this Point point)
        {
            return NeighboursCardinal(point).Concat(NeighboursDiagonal(point));
        }

        public static Point Invert(this Point point)
        {
            return new Point(-point.X, -point.Y);
        }
    }
}
