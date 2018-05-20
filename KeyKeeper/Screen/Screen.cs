using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Screen
{
    public abstract class Screen
    {
        protected int X { get; }
        protected int Y { get; }
        protected int Width { get; }
        protected int Height { get; }

        public Screen(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public abstract void Draw(SpriteBatch batch);
    }
}
