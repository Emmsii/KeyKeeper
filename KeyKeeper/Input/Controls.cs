using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Input
{
    public static class Controls
    {
        public static readonly Control NORTH = new Control().Bind(Keys.Up).Bind(Keys.NumPad8).Bind(Keys.K);
        public static readonly Control SOUTH = new Control().Bind(Keys.Down).Bind(Keys.NumPad2).Bind(Keys.J);
        public static readonly Control EAST = new Control().Bind(Keys.Left).Bind(Keys.NumPad6).Bind(Keys.H);
        public static readonly Control WEST = new Control().Bind(Keys.Right).Bind(Keys.NumPad4).Bind(Keys.L);
        public static readonly Control NORTH_EAST = new Control().Bind(Keys.NumPad9).Bind(Keys.U);
        public static readonly Control NORTH_WEST = new Control().Bind(Keys.NumPad7).Bind(Keys.N);
        public static readonly Control SOUTH_EAST = new Control().Bind(Keys.NumPad3).Bind(Keys.B);
        public static readonly Control SOUTH_WEST = new Control().Bind(Keys.NumPad1).Bind(Keys.Y);

        public static readonly Control WAIT = new Control().Bind(Keys.NumPad5).Bind(Keys.OemPeriod);

    }

    public class Control
    {
        protected HashSet<Keys> _keys = new HashSet<Keys>();

        public bool IsPressed(Keys key) => _keys.Contains(key);

        public Control() { }

        public Control Bind(Keys key)
        {
            _keys.Add(key);
            return this;
        }
    }
}
