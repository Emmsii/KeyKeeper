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

        public static void DrawSprite(SpriteBatch batch, Sprite sprite, int x, int y, Color foregroundColor, Color? backgroundColor = null)
        {
            DrawSprite(batch, sprite, x, y, 0, 0, foregroundColor, backgroundColor);
        }

        public static void DrawSprite(SpriteBatch batch, Sprite sprite, int x, int y, int offsetX, int offsetY, Color foregroundColor, Color? backgroundColor = null)
        {
            _destination.X = (x * sprite.Width * sprite.Scale) + offsetX;
            _destination.Y = (y * sprite.Height * sprite.Scale) + offsetY;
            _destination.Width = sprite.Width * sprite.Scale;
            _destination.Height = sprite.Height * sprite.Scale;

            //if (backgroundColor != null)
            //{
            //    Sprite fillSprite = Assets.GetSprite("fill");
            //    batch.Draw(fillSprite.Texture, _destination, fillSprite.Bounds, backgroundColor ?? Color.HotPink);
            //}

            batch.Draw(sprite.Texture, _destination, sprite.Bounds, foregroundColor);
        }

        //public static void DrawSpritePrecise(SpriteBatch batch, Sprite sprite, int x, int y, int offsetX, int offsetY, Color foregroundColor, Color? backgroundColor = null)
        //{
        //    _destination.X = x + offsetX;
        //    _destination.Y = y + offsetY;
        //    _destination.Width = sprite.Width * sprite.Scale;
        //    _destination.Height = sprite.Height* sprite.Scale;

        //    batch.Draw(sprite.Texture, _destination, sprite.Bounds, foregroundColor);
        //}

        public static void DrawString(SpriteBatch batch, Font font, string text, int x, int y, Color foregroundColor, Color? backgroundColor = null)
        {
            if(font == null)
            {
                throw new ArgumentNullException(nameof(text), "Cannot draw string with null font.");
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }

            int offset = 0;

            foreach(char character in text)
            {
                Sprite charSprite = font.GetCharacterSprite(character);

                _destination.X = x * charSprite.Width * 2 + (charSprite.Width / 2) + (offset++ * charSprite.Width);
                _destination.Y = y * charSprite.Height;
                _destination.Width = charSprite.Width;
                _destination.Height = charSprite.Height;

                if (backgroundColor != null)
                {
                    Sprite fillSprite = Assets.GetSprite("fill");
                    batch.Draw(fillSprite.Texture, _destination, fillSprite.Bounds, backgroundColor ?? KeyKeeper.BackgroundColor);
                }

                batch.Draw(charSprite.Texture, _destination, charSprite.Bounds, foregroundColor);
            }
        }      

        public static void DrawBox(SpriteBatch batch, int x, int y, int width, int height, Color borderColor)
        {
            DrawSprite(batch, Assets.GetSprite("border_top_right"), x, y, borderColor);
            DrawSprite(batch, Assets.GetSprite("border_top_left"), x + width, y, borderColor);
            DrawSprite(batch, Assets.GetSprite("border_bottom_right"), x, y + height, borderColor);
            DrawSprite(batch, Assets.GetSprite("border_bottom_left"), x + width, y + height, borderColor);

            DrawLineOfSprites(batch, Assets.GetSprite("border_vertical"), x, y + 1, x, y + height - 1, borderColor);
            DrawLineOfSprites(batch, Assets.GetSprite("border_vertical"), x + width, y + 1, x + width, y + height - 1, borderColor);
            DrawLineOfSprites(batch, Assets.GetSprite("border_horizontal"), x + 1, y, x + width - 1, y, borderColor);
            DrawLineOfSprites(batch, Assets.GetSprite("border_horizontal"), x + 1, y + height, x + width - 1, y + height, borderColor);
        }

        public static void DrawLineOfSprites(SpriteBatch batch, Sprite sprite, int x0, int y0, int x1, int y1, Color foregroundColor, Color? backgroundColor = null)
        {
            foreach(Point point in new Line(x0, y0, x1, y1))
            {
                DrawSprite(batch, sprite, point.X, point.Y, foregroundColor, backgroundColor);
            }
        }
    }
}
