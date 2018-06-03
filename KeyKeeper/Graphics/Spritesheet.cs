using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Graphics
{
    public class Spritesheet
    {
        public int SpriteWidth { get; }
        public int SpriteHeight { get; }
        public Texture2D Texture { get; }

        public Spritesheet(int spriteWidth, int spriteHeight, Texture2D texture)
        {
            SpriteWidth = spriteWidth;
            SpriteHeight = spriteHeight;
            Texture = texture;
        }

        public Sprite CutSprite(int x, int y, int scale, string name)
        {
            return new Sprite(Texture,
                              new Rectangle(x * SpriteWidth, y * SpriteHeight, SpriteWidth, SpriteHeight),
                              SpriteWidth,
                              SpriteHeight,
                              scale,
                              name);
        }
    }
}
