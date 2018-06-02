using KeyKeeper.Content;
using KeyKeeper.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Graphics
{
    public static class Renderer
    {
        private static Rectangle _destination = new Rectangle();

        public static void DrawSprite(SpriteBatch batch, Sprite sprite, int x, int y, int scale, Color color)
        {
            _destination.X = x * sprite.Width * scale;
            _destination.Y = y * sprite.Width * scale;
            _destination.Width = sprite.Width * scale;
            _destination.Height = sprite.Height * scale;

            batch.Draw(sprite.Texture, _destination, sprite.Bounds, color);
        }

        public static void DrawBox(SpriteBatch batch, int x, int y, int width, int height, int scale, Color borderColor)
        {
            DrawSprite(batch, Assets.GetSprite("border_top_right"), x, y, scale, borderColor);
            DrawSprite(batch, Assets.GetSprite("border_top_left"), x + width, y, scale, borderColor);
            DrawSprite(batch, Assets.GetSprite("border_bottom_right"), x, y + height, scale, borderColor);
            DrawSprite(batch, Assets.GetSprite("border_bottom_left"), x + width, y + height, scale, borderColor);

            DrawLineOfSprites(batch, Assets.GetSprite("border_vertical"), x, y + 1, x, y + height - 1, scale, borderColor);
            DrawLineOfSprites(batch, Assets.GetSprite("border_vertical"), x + width, y + 1, x + width, y + height - 1, scale, borderColor);
            DrawLineOfSprites(batch, Assets.GetSprite("border_horizontal"), x + 1, y, x + width - 1, y, scale, borderColor);
            DrawLineOfSprites(batch, Assets.GetSprite("border_horizontal"), x + 1, y + height, x + width - 1, y + height, scale, borderColor);
        }

        public static void DrawLineOfSprites(SpriteBatch batch, Sprite sprite, int x0, int y0, int x1, int y1, int scale, Color color)
        {
            foreach(Point point in new Line(x0, y0, x1, y1))
            {
                DrawSprite(batch, sprite, point.X, point.Y, scale, color);
            }
        }
    }
}
