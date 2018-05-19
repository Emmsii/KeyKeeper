using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Input
{
    internal class DelayedInputHandler
    {
        public event EventHandler<InputEventArgs> InputFireEvent;

        private int _firstWaitTime = 22;
        private int _continuousWaitTime = 2;

        private int _timer;

        private KeyboardState _lastState;
        private Keys _lastKey = Keys.None;

        private Keys[] _currentKeys = new Keys[0];

        private bool CanInvokeEvent => _timer <= 0;

        public DelayedInputHandler(int firstWaitTime, int continuousWaitTime)
        {
            _firstWaitTime = firstWaitTime;
            _continuousWaitTime = continuousWaitTime;
        }

        public void Update(KeyboardState keyboardState)
        {
            DecrementTimer();

            _currentKeys = keyboardState.GetPressedKeys();

            Keys currentKey = _currentKeys.Length > 1 ? DifferenceBetween(_lastState.GetPressedKeys(), _currentKeys) : _currentKeys.Length > 0 ? _currentKeys[0] : Keys.None;

            if (_lastKey == Keys.None && currentKey != Keys.None && CanInvokeEvent)
            {
                _timer = _firstWaitTime;
                FireEvent(currentKey);
            }
            else if (_lastKey != Keys.None && _lastKey == currentKey && CanInvokeEvent)
            {
                _timer = _continuousWaitTime;
                FireEvent(currentKey);
            }
            else if (_lastKey != Keys.None && currentKey == Keys.None)
            {
                _timer = 0;
            }else if(_lastKey != Keys.None && _lastKey != currentKey)
            {
                _timer = _firstWaitTime;
                FireEvent(currentKey);
            }

            _lastKey = currentKey;
            _lastState = keyboardState;
        }

        private Keys DifferenceBetween(Keys[] oldKeys, Keys[] newKeys)
        {
            Keys diff = newKeys.Except(oldKeys).FirstOrDefault();
            return diff == Keys.None ? _lastKey : diff;
        }

        private void FireEvent(Keys key)
        {
            InputFireEvent?.Invoke(this, new InputEventArgs(key));
        }

        private void DecrementTimer()
        {
            if (_timer > 0) _timer--;
        }
    }
}
