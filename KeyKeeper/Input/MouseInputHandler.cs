using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Input
{
    public sealed class MouseInputHandler
    {
        public delegate void MouseEventHandler(object sender, MouseEventArgs args);

        public event MouseEventHandler OnMouseMove;
        public event MouseEventHandler OnMouseDown;
        public event MouseEventHandler OnMouseReleased;

        private ButtonState _currentLeftButtonState;
        private ButtonState _currentRightButtonState;
        private ButtonState _oldLeftButtonState;
        private ButtonState _oldRightButtonState;

        private int _currentX;
        private int _currentY;
        private int _oldX;
        private int _oldY;

        private bool _mouseWasDown;

        private MouseState State => Mouse.GetState();

        public void Update(Viewport windowViewport)
        {
            _currentX = State.X;
            _currentY = State.Y;

            if (!windowViewport.Bounds.Contains(State.Position))
            {
                return;
            }

            _currentLeftButtonState = State.LeftButton;
            _currentRightButtonState = State.RightButton;

            DetectMouseMove();
            DetectMouseButtons();
            DetectMouseDrag();

            _oldX = _currentX;
            _oldY = _currentY;
            _oldLeftButtonState = _currentLeftButtonState;
            _oldRightButtonState = _currentRightButtonState;
        }

        private void DetectMouseMove()
        {
            if (_currentX != _oldX || _currentY != _oldY)
            {
                OnMouseMove?.Invoke(this, GetMouseEventArgs());
            }
        }

        private void DetectMouseButtons()
        {
            if(State.LeftButton == ButtonState.Pressed && !_mouseWasDown)
            {
                OnMouseDown?.Invoke(this, GetMouseEventArgs());
                _mouseWasDown = true;
            }
            else if(State.LeftButton == ButtonState.Released && _mouseWasDown)
            {
                OnMouseReleased?.Invoke(this, GetMouseEventArgs());
                _mouseWasDown = false;
            }
        }

        private void DetectMouseDrag()
        {

        }


        private MouseEventArgs GetMouseEventArgs()
        {
            return new MouseEventArgs
            {
                X = _currentX,
                Y = _currentY,
                LeftButton = State.LeftButton
            };
        }

    }
}
