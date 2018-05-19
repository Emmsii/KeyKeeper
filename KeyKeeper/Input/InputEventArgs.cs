using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Input
{
    internal class InputEventArgs : EventArgs
    {
        public Keys Key { get; set; }

        public InputEventArgs(Keys key)
        {
            Keys = key;
        }
    }
}
