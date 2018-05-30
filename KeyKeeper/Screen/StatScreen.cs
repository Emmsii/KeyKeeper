using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyKeeper.Content;
using KeyKeeper.Extensions;
using KeyKeeper.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KeyKeeper.Screen
{
    public class StatScreen : Screen
    {
        private const int PADDING = 1;
        private const float FONT_SCALE = 0.5f;
        private readonly Color FONT_COLOR = Color.White;

        private readonly Font _font;

        public StatScreen(int x, int y, int width, int height, int scale) : base(x, y, width, height, scale, true)
        {
            _font = Assets.GetFont("font");
        }

        public override void Draw(SpriteBatch batch)
        {

            batch.DrawUiPanel(X, Y, Width, Height, Scale, Color.White);

            //int yp = 1;
            //batch.DrawFont("My Name the Slayer", _font, X * Scale + (PADDING * 2), Y + yp++ * Scale + (PADDING * 2), Scale, FONT_SCALE, FONT_COLOR);
            //batch.DrawFont("Level 4 - Dungeons", _font, X * Scale + (PADDING * 2), Y + yp++ * Scale + (PADDING * 2), Scale, FONT_SCALE, FONT_COLOR);

            //batch.DrawFont("Health: 4/22", _font, X * Scale + (PADDING * 2), (int)(Y + yp++ * Scale + (PADDING * 2)* FONT_SCALE), Scale, FONT_SCALE, FONT_COLOR);
            //batch.DrawFont("Health: 4/22", _font, X * Scale + (PADDING * 2), (int)(Y + yp++ * Scale + (PADDING * 2) * FONT_SCALE), Scale, FONT_SCALE, FONT_COLOR);
        }
    }
}
