using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Input
{
    internal class KeyEventArgs : EventArgs
    {
        public Keys Key { get; set; }

        public KeyEventArgs(Keys key)
        {
            Key = key;
        }
    }
}
