using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Input
{
    public class MouseEventArgs : EventArgs
    {
        public int X { get; set; }
        public int Y { get; set; }
        public ButtonState LeftButton { get; set; }
        public ButtonState RightButton { get; set; }        
    }
}
