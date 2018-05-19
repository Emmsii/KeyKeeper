using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Helpers
{
    public class Line : IEnumerable<Point>
    {

        private int _x0, _y0;
        private int _x1, _y1;

        public Line(int x0, int y0, int x1, int y1)
        {
            _x0 = x0;
            _y0 = y0;
            _x1 = x1;
            _y1 = y1;
        }

        public IEnumerator<Point> GetEnumerator()
        {
            int dx = Math.Abs(_x1 - _x0);
            int dy = Math.Abs(_y1 - _y0);

            int sx = _x0 < _x1 ? 1 : -1;
            int sy = _y0 < _y1 ? 1 : -1;
            int err = dx - dy;

            while (true)
            {
                yield return new Point(_x0, _y0);

                if (_x0 == _x1 && _y0 == _y1) yield break;

                int e2 = err * 2;
                if(e2 > -dx)
                {
                    err -= dy;
                    _x0 += sx;
                }
                if(e2 < dx)
                {
                    err += dx;
                    _y0 += sy;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
