using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyKeeper.Content;
using KeyKeeper.Graphics;
using KeyKeeper.Input;
using KeyKeeper.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace KeyKeeper.Screen.UI
{
    public class Button : Component
    {
        private readonly int _width;

        private Label _label;
        private readonly Color _idleColor;
        private readonly Color _hoverColor;
        private readonly Color _clickColor;

        private Color _color;

        private bool _mouseDown;

        public Button(int x, int y, int width, Label label, Color idleColor, Color hoverColor, Color clickColor) : base(x, y)
        {
            _label = label;
            _width = width;
            _idleColor = idleColor;
            _hoverColor = hoverColor;
            _clickColor = clickColor;

            _label.ParentComponent = this;

            _color = _idleColor;
        }

        public override void Draw(SpriteBatch batch)
        {
            for(int i = 0; i < _width; i++)
            {
                Renderer.DrawSprite(batch, Assets.GetSprite("fill"), X + i, Y, _color);
            }
            
            _label.Draw(batch);
        }

        public override void OnMouseMove(object sender, MouseEventArgs args)
        {
            _color = _idleColor;
            if(PositionIntersectsButton(args.X / Assets.DEFAULT_TEXTURE_WIDTH, args.Y / Assets.DEFAULT_TEXTURE_HEIGHT))
            {
                _color = _hoverColor;
            }
        }

        public override void OnMouseDown(object sender, MouseEventArgs args)
        {
            bool mouseIntersectsButton = PositionIntersectsButton(args.X / Assets.DEFAULT_TEXTURE_WIDTH, args.Y / Assets.DEFAULT_TEXTURE_HEIGHT);

            if (mouseIntersectsButton)
            {
                _mouseDown = true;
                _color = _clickColor;
            }

            if(args.LeftButton == ButtonState.Released)
            {
                _mouseDown = false;
                if (mouseIntersectsButton)
                {
                    _color = _idleColor;
                }
            }
        }

        public override void OnMouseReleased(object sender, MouseEventArgs args)
        {
            _color = _idleColor;
        }

        private bool PositionIntersectsButton(int x, int y)
        {
            return x >= X && y == Y && x < X + _width;
        }
    }
}
