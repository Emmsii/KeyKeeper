using KeyKeeper.Content;
using KeyKeeper.Graphics;
using KeyKeeper.Helpers;
using KeyKeeper.Screen.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Screen
{
    public class StatScreen : BaseScreen
    {

        private const int PADDING = 0;
        private const int FONT_SCALE = 1;

        private readonly Font _font;
        
        public StatScreen(int x, int y, int width, int height, bool hasBorder) : base(x, y, width, height, hasBorder)
        {
            _font = Assets.GetFont("font");

            AddComponent(new Label(0, 0, "Hello World", Color.Lime));

            AddComponent(new Button(0, 1, 5, new Label(1, 0, "Hello", Color.Red), Color.Gray, Color.LightGray, Color.White));
        }


        public override void Draw(SpriteBatch batch)
        {
            int offset = 0;

            //Renderer.DrawString(batch, _font, "hello world", X + PADDING, Y + PADDING + offset++, Scale, Scale / (2 / FONT_SCALE), FONT_SCALE, Color.White);
            //Renderer.DrawString(batch, _font, "By name is bill", X + PADDING, Y + PADDING + offset++, Scale, Scale / (2 / FONT_SCALE), FONT_SCALE, Color.White);
            //Renderer.DrawString(batch, _font, "And numbers", X + PADDING, Y + PADDING + offset++, Scale, Scale / (2 / FONT_SCALE), FONT_SCALE, Color.White);

            base.Draw(batch);
        }

    }
}
