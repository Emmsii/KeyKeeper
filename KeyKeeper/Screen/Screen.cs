using KeyKeeper.Graphics;
using Microsoft.Xna.Framework;
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
        protected int Scale { get; }

        public Screen(int x, int y, int width, int height, int scale)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Scale = scale;
        }

        public abstract void Draw(SpriteBatch batch);

        protected void DrawSprite(Sprite sprite, int xp, int yp, Color foregroundColor, Color backgroundColor, SpriteBatch batch)
        {
            Rectangle destination = new Rectangle(xp * sprite.Width * Scale,
                                                  yp * sprite.Height * Scale,
                                                  sprite.Width * Scale,
                                                  sprite.Height * Scale);

            // TODO: Draw background color

            batch.Draw(sprite.Texture, destination, sprite.Bounds, foregroundColor);
        }
    }
}
