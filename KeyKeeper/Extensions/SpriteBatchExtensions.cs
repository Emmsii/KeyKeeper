using KeyKeeper.Content;
using KeyKeeper.Graphics;
using KeyKeeper.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Extensions
{
    public static class SpriteBatchExtensions
    {
        private const float DEFAULT_SCALE = 1f;

        private static Rectangle _destination = new Rectangle();

        public static void DrawSprite(this SpriteBatch batch, Sprite sprite, int x, int y, Color color)
        {
            DrawSprite(batch, sprite, x, y, DEFAULT_SCALE, color);
        }

        public static void DrawSprite(this SpriteBatch batch, Sprite sprite, int x, int y, float scale, Color color)
        {
            _destination.X = (int) (x * sprite.Width * scale);
            _destination.Y = (int) (y * sprite.Height * scale);
            _destination.Width = (int) (sprite.Width * scale);
            _destination.Height = (int) (sprite.Height * scale);

            batch.Draw(sprite.Texture, _destination, sprite.Bounds, color);
        }

        public static void DrawFont(this SpriteBatch batch, string text, Font font, int x, int y, int windowScale, float fontScale, Color color)
        {
            float offset = 0;
            Sprite fontSprite;
            Rectangle destination = new Rectangle();
            foreach (char character in text)
            {
                fontSprite = font.GetCharacterSprite(character);

                destination.X = (int) ((x + (offset * fontScale)) * font.SpriteWidth * windowScale);
                destination.Y = y / 2 * font.SpriteHeight * windowScale;
                destination.Width = (int)(fontSprite.Width * fontScale * windowScale);
                destination.Height = (int)(fontSprite.Height * fontScale * windowScale);

                batch.Draw(fontSprite.Texture, destination, fontSprite.Bounds, color);
                offset++;
            }
        }

        public static void DrawUiPanel(this SpriteBatch batch, int x, int y, int width, int height, int scale, Color borderColor, string title = null, float fontScale = 1f, Sprite fillWithSprite = null, Color? fillColor = null )
        {
            if(fillWithSprite != null)
            {
                for(int ya = 1; ya < height; ya++)
                {
                    int yp = ya + y;
                    for(int xa = 1; xa < width; xa++)
                    {
                        int xp = xa + x;
                        DrawSprite(batch, fillWithSprite, xp, yp, fillColor ?? default(Color));
                    }
                }
            }

            DrawSprite(batch, Assets.GetSprite("border_top_right"), x, y, scale, borderColor);
            DrawSprite(batch, Assets.GetSprite("border_top_left"), x + width, y, scale, borderColor);
            DrawSprite(batch, Assets.GetSprite("border_bottom_right"), x, y + height, scale, borderColor);
            DrawSprite(batch, Assets.GetSprite("border_bottom_left"), x + width, y + height, scale, borderColor);

            DrawLineOfSprites(batch, Assets.GetSprite("border_vertical"), x, y + 1, x, y + height - 1, scale, borderColor);
            DrawLineOfSprites(batch, Assets.GetSprite("border_vertical"), x + width, y + 1, x + width, y + height - 1, scale, borderColor);
            DrawLineOfSprites(batch, Assets.GetSprite("border_horizontal"), x + 1, y, x + width - 1, y, scale, borderColor);
            DrawLineOfSprites(batch, Assets.GetSprite("border_horizontal"), x + 1, y + height, x + width - 1, y + height, scale, borderColor);

            if (!string.IsNullOrEmpty(title))
            {
                DrawFont(batch, title, Assets.GetFont("font"), x, y, scale, fontScale, borderColor);
            }
        }


        public static void DrawLineOfSprites(this SpriteBatch batch, Sprite sprite, int x0, int y0, int x1, int y1, float scale, Color color)
        {
            foreach(Point point in new Line(x0, y0, x1, y1))
            {
                DrawSprite(batch, sprite, point.X, point.Y, scale, color);
            }
        }
        
    }
    
}
