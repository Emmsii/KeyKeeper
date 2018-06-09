using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Helpers
{
    public abstract class Direction
    {
        public static readonly Direction[] All = { new Up(), new Down(), new Left(), new Right() };

        public abstract void Next(ref Point point);

        public int NextX(int x)
        {
            return NextX(x, 1);
        }

        public int NextY(int y)
        {
            return NextY(y, 1);
        }

        public virtual int NextX(int x, int amount)
        {
            return x;
        }

        public virtual int NextY(int y, int amount)
        {
            return y;
        }
    }

    public class Up : Direction
    {
        public override void Next(ref Point point)
        {
            point.Y++;
        }

        public override int NextY(int y, int amount)
        {
            return y + amount;
        }
    }

    public class Down : Direction
    {
        public override void Next(ref Point point)
        {
            point.Y--;
        }

        public override int NextY(int y, int amount)
        {
            return y - amount;
        }
    }

    public class Left : Direction
    {
        public override void Next(ref Point point)
        {
            point.X--;
        }

        public override int NextX(int x, int amount)
        {
            return x - amount;
        }
    }

    public class Right : Direction
    {
        public override void Next(ref Point point)
        {
            point.X++;
        }

        public override int NextX(int x, int amount)
        {
            return x + amount;
        }
    }
}
