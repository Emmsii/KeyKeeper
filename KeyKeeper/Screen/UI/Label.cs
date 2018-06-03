using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyKeeper.Content;
using KeyKeeper.Graphics;
using KeyKeeper.Helpers;
using KeyKeeper.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KeyKeeper.Screen.UI
{
    public class Label : Component
    {
        private readonly Font _font;

        private string _text;
        private Color _color;
        
        public Label(int x, int y, string text, Color color, Font font = null) : base(x, y)
        {
            _text = text;
            _color = color;
            _font = font ?? Assets.GetFont("font");
        }

        public override void Draw(SpriteBatch batch)
        {
            Renderer.DrawString(batch, _font, _text, X, Y, _color);
        }

        public override void OnMouseDown(object sender, MouseEventArgs args)
        {

        }

        public override void OnMouseMove(object sender, MouseEventArgs args)
        {
        }

        public override void OnMouseReleased(object sender, MouseEventArgs args)
        {
        }
    }
}
