using KeyKeeper.World;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Helpers
{
    public enum FovType
    {
        Shadowcast,
        Linecast
    }
    
    internal class FovDirection
    {
        public static readonly FovDirection Up = new FovDirection(0, -1);
        public static readonly FovDirection Down = new FovDirection(0, 1);
        public static readonly FovDirection Left = new FovDirection(-1, 0);
        public static readonly FovDirection Right = new FovDirection(1, 0);

        public static readonly FovDirection UpLeft = new FovDirection(-1, -1);
        public static readonly FovDirection UpRight = new FovDirection(1, -1);
        public static readonly FovDirection DownLeft = new FovDirection(-1, 1);
        public static readonly FovDirection DownRight = new FovDirection(1, 1);

        public static readonly FovDirection[] Cardinals = { Up, Down, Left, Right };
        public static readonly FovDirection[] Diagonals = { UpLeft, UpRight, DownLeft, DownRight };
        public static readonly FovDirection[] All = { Up, Down, Left, Right, UpLeft, UpRight, DownLeft, DownRight };

        public int X { get; }
        public int Y { get; }

        public FovDirection(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class FieldOfView
    {
        private readonly GameWorld _world;
        private readonly HashSet<Point> _inFov = new HashSet<Point>();

        public bool IsInFov(int x, int y) => _inFov.Contains(new Point(x, y));
        public void ClearFov() => _inFov.Clear();

        private double Radius(int dx, int dy) => Math.Sqrt(dx * dx + dx * dy);

        public FieldOfView(GameWorld world)
        {
            _world = world;
        }

        public void Compute(int x, int y, int depth, int radius, FovType type)
        {
            if (radius < 1) throw new ArgumentOutOfRangeException("Cannot compute fov with a radius less than 1.");
            SetInFov(x, y, depth);
            if (type == FovType.Shadowcast) ShadowCast(x, y, depth, radius);
            else if (type == FovType.Linecast) LineCast(x, y, depth, radius);
        }

        private void LineCast(int x, int y, int depth, int radius)
        {
            int radiusSquared = radius * radius;
            for(int ya = -radius; ya < radius; ya++)
            {
                int yaSquared = ya * ya;
                for(int xa = -radius; xa < radius; xa++)
                {
                    if (xa * xa + yaSquared > radiusSquared) continue;
                    if (!_world.InBounds(xa + x, ya + y, depth)) continue;

                    foreach(Point p in new Line(x, y, x + xa, y + ya))
                    {
                        SetInFov(p.X, p.Y, depth);
                        _world.SetExplored(p.X, p.Y, depth, true);
                        if (!_world.IsTransparent(p.X, p.Y, depth)) break;
                    }
                }
            }
        }

        private void ShadowCast(int x, int y, int depth, int radius)
        {
            foreach(FovDirection dir in FovDirection.Diagonals)
            {
                CastLight(1, 1.0f, 0.0f, x, y, 0, dir.X, dir.Y, 0, depth, radius);
                CastLight(1, 1.0f, 0.0f, x, y, dir.X, 0, 0, dir.Y, depth, radius);
            }
        }

        private void CastLight(int row, float start, float end, int startX, int startY, int xx, int xy, int yx, int yy, int depth, int radius)
        {
            float newStart = 0.0f;
            if (start < end) return;
            bool blocked = false;

            for(int distance = row; distance <= radius && !blocked; distance++)
            {
                int deltaY = -distance;
                for(int deltaX = -distance; deltaX <= 0; deltaX++)
                {
                    int currentX = startX + deltaX * xx + deltaY * xy;
                    int currentY = startY + deltaX * yx + deltaY * yy;
                    float leftSlope = (deltaX - 0.5f) / (deltaY + 0.5f);
                    float rightSlope = (deltaX + 0.5f) / (deltaY - 0.5f);

                    if(!(currentX >= 0 && currentY >= 0 && currentX < _world.CurrentLevelWidth(depth) && currentY < _world.CurrentLevelHeight(depth)) || start < rightSlope)
                    {
                        continue;
                    }
                    else if(end > leftSlope)
                    {
                        break;
                    }

                    if(Radius(deltaX, deltaY) <= radius)
                    {
                        SetInFov(currentX, currentY, depth);
                    }

                    if (blocked)
                    {
                        if(!_world.IsTransparent(currentX, currentY, depth))
                        {
                            newStart = rightSlope;
                            continue;
                        }
                        else
                        {
                            blocked = false;
                            start = newStart;
                        }
                    }
                    else
                    {
                        if(!_world.IsTransparent(currentX, currentY, depth) && distance < radius)
                        {
                            blocked = true;
                            CastLight(distance + 1, start, leftSlope, startX, startY, xx, xy, yx, yy, depth, radius);
                            newStart = rightSlope;
                        }
                    }
                }
            }
        }

        private void SetInFov(int x, int y, int depth)
        {
            _world.SetExplored(x, y, depth, true);
            _inFov.Add(new Point(x, y));
        }
    }
}
