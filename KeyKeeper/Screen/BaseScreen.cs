using KeyKeeper.Content;
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
    public class BaseScreen
    {
        private int _x;
        private int _y;
        private int _width;
        private int _height;

        protected int X {
            get {
                return Parent?.X + _x ?? _x;
            }
            private set {
                _x = value;
            }
        }
        protected int Y {
            get {
                return Parent?.Y + _y ?? _y;
            }
            private set {
                _y = value;
            }
        }

        protected int Width {
            get {
                return Math.Min(_width, Parent?.Width ?? int.MaxValue);
            }
            private set {
                _width = value;
            }
        }

        protected int Height {
            get {
                return Math.Min(_height, Parent?.Height ?? int.MaxValue);
            }
            private set {
                _height = value;
            }
        }
        
        protected int Scale { get; }
        protected bool HasBorder { get; }

        protected BaseScreen Parent { get; private set; }

        private List<BaseScreen> _screens = new List<BaseScreen>();

        public BaseScreen(int x, int y, int width, int height, int scale, bool hasBorder)
        {
            X = hasBorder ? x + 1 : x;
            Y = hasBorder ? y + 1 : y;
            Width = hasBorder ? width - 2 : width;
            Height = hasBorder ? height - 2 : height;
            Scale = scale;
            HasBorder = hasBorder;
        }

        public virtual void Draw(SpriteBatch batch)
        {
            foreach (BaseScreen screen in _screens)
            {
                screen.Draw(batch);
            }
            if (HasBorder)
            {
                DrawBorder(batch);
            }
        }

        private void DrawBorder(SpriteBatch batch)
        {
            Renderer.DrawBox(batch, X - 1, Y - 1, Width + 1, Height + 1, Scale, Color.White);
        }

        public void AddScreen(BaseScreen screen)
        {
            _screens.Add(screen);
            screen.Parent = this;
        }

        //protected void DrawSprite(Sprite sprite, int xp, int yp, Color foregroundColor, Color backgroundColor, SpriteBatch batch)
        //{
        //    Rectangle destination = new Rectangle(xp * sprite.Width * Scale,
        //                                          yp * sprite.Height * Scale,
        //                                          sprite.Width * Scale,
        //                                          sprite.Height * Scale);

        //    // TODO: Draw background color

        //    batch.Draw(sprite.Texture, destination, sprite.Bounds, foregroundColor);
        //}
    }
}
