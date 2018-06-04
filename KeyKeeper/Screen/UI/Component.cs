using KeyKeeper.Helpers;
using KeyKeeper.Input;
using KeyKeeper.Managers;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Screen.UI
{
    public abstract class Component
    {
        private int _x;
        private int _y;

        protected int X {
            get {
                int x = _x;

                if (ParentComponent != null)
                {
                    x += ParentComponent.X;
                }

                if (ParentScreen != null)
                {
                    x += ParentScreen.X;
                }

                return x;
            }
            set {
                _x = value;
            }
        }

        protected int Y {
            get {
                int y = _y;

                if (ParentComponent != null)
                {
                    y += ParentComponent.Y;
                }

                if(ParentScreen != null)
                {
                    y += ParentScreen.Y;
                }

                return y;
                //return ParentComponent?.X + ParentScreen?.Y + _y ?? _y;
            }
            set {
                _y = value;
            }
        }

        public BaseScreen ParentScreen { get; set; }
        public Component ParentComponent { get; set; }

        public Component(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void RegisterEvents(MouseInputHandler mouseInput)
        {
            mouseInput.OnMouseMove += OnMouseMove;
            mouseInput.OnMouseDown += OnMouseDown;
            mouseInput.OnMouseReleased += OnMouseReleased;
        }

        public abstract void Draw(SpriteBatch batch);

        public abstract void OnMouseMove(object sender, MouseEventArgs args);
        public abstract void OnMouseDown(object sender, MouseEventArgs args);
        public abstract void OnMouseReleased(object sender, MouseEventArgs args);

    }
}
