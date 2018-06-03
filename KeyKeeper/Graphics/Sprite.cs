using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Graphics
{
    public class Sprite
    {
        public string Name { get; }
        public int Width { get; }
        public int Height { get; }
        public int Scale { get; }

        public Texture2D Texture { get; }
        public Rectangle Bounds;

        public Sprite(Texture2D texture, Rectangle bounds, int width, int height, int scale, string name)
        {
            Texture = texture;
            Width = width;
            Height = height;
            Bounds = bounds;
            Scale = scale;
            Name = name;
        }
    }
}
