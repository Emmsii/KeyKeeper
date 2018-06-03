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

namespace KeyKeeper.Screen
{
    public class BaseScreen
    {

        public Alignment ParentAnchorPosition { get; set; } = Alignment.None;
        public Alignment SelfAnchorPosition { get; set; } = Alignment.None;

        private int _x;
        private int _y;
        private int _width;
        private int _height;

        protected int X {
            get {

                if (Parent != null)
                {
                    int x;
                    switch (ParentAnchorPosition)
                    {
                        case Alignment.Center:
                            x = Parent.X + (Parent.Width / 2) + SelfX;
                            break;
                        case Alignment.Left:
                        case Alignment.BottomLeft:
                        case Alignment.TopLeft:
                            x = Parent.X;
                            break;
                        case Alignment.Right:
                        case Alignment.TopRight:
                        case Alignment.BottomRight:
                            x = Parent.X + Parent.Width;
                            break;
                        case Alignment.None:
                        default:
                            x = Parent.X + _x;
                            break;
                    }

                    return HasBorder ? x + 1 : x;
                }
                return _x;
            }
            private set {
                _x = value;
            }
        }
        protected int Y {
            get {

                if (Parent != null)
                {
                    int y;
                    switch (ParentAnchorPosition)
                    {
                        case Alignment.Center:
                            y = Parent.Y + (Parent.Height / 2) + SelfY;
                            break;
                        case Alignment.Top:
                        case Alignment.TopLeft:
                        case Alignment.TopRight:
                            y = Parent.Y;
                            break;
                        case Alignment.Bottom:
                        case Alignment.BottomLeft:
                        case Alignment.BottomRight:
                            y = Parent.Y + Parent.Height;
                            break;
                        case Alignment.None:
                        default:
                            y = Parent.Y + _y;
                            break;
                    }

                    return HasBorder ? y + 1 : y;
                }
                return _x;
            }
            private set {
                _y = value;
            }
        }

        private int SelfX {
            get {
                int x;
                switch (SelfAnchorPosition)
                {
                    case Alignment.Center:
                        x = Width / 2;
                        break;
                    case Alignment.BottomRight:
                        x = Width;
                        break;
                    case Alignment.TopRight:
                        x = Width;
                        break;
                    case Alignment.TopLeft:
                    case Alignment.None:
                    default:
                        return 0;
                }
                return -(x + 1);
            }
        }

        private int SelfY {
            get {
                int y;
                switch (SelfAnchorPosition)
                {
                    case Alignment.Center:
                        y = Height / 2;
                        break;
                    case Alignment.BottomLeft:
                    case Alignment.BottomRight:
                        y = Height;
                        break;
                    case Alignment.TopRight:
                    case Alignment.TopLeft:
                    case Alignment.None:
                    default:
                        return 0;
                }

                return -(y + 1);
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

        protected bool HasBorder { get; }

        protected BaseScreen Parent { get; private set; }

        private List<BaseScreen> _screens = new List<BaseScreen>();

        public BaseScreen(int x, int y, int width, int height, bool hasBorder)
        {
            X = x;
            Y = y;
            Width = hasBorder ? width - 2 : width;
            Height = hasBorder ? height - 2 : height;
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

            //Renderer.DrawSprite(batch, Assets.GetSprite("dot"), X - 1 - SelfX, Y - 1 - SelfY, Color.LimeGreen * 0.75f);
        }

        private void DrawBorder(SpriteBatch batch)
        {
            Renderer.DrawBox(batch, X - 1, Y - 1, Width  + 1, Height  + 1, Color.White);
        }

        public void AddScreen(BaseScreen screen)
        {
            _screens.Add(screen);
            screen.Parent = this;
        }

        protected bool InScreenBounds(int x, int y)
        {
            return x >= X && y >= Y && x < X + Width && y < Y + Height;
        }

    }
}
