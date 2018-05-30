using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Input
{
    public class MouseInputHandler
    {
        private MouseState _previousMouseState;

        private int _oldX;
        private int _oldY;
        private int _newX;
        private int _newY;

        public bool HasMoved { get; private set; }
        public bool IsDragging { get; private set; }

        public void Update(MouseState currentState)
        {
            _newX = currentState.Position.X;
            _newY = currentState.Position.Y;

            HasMoved = _newX != _oldX || _newY == _oldY;
            IsDragging = HasMoved && currentState.LeftButton == ButtonState.Pressed;

            _oldX = _newX;
            _oldY = _newY;

            _previousMouseState = currentState;
        }
    }
}
