using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Helpers.Game
{
    public sealed class GameEvent
    {
        public GameEffect Effect { get; }
        public int X { get; }
        public int Y { get; }

        public GameEvent(GameEffect effect, int x, int y)
        {
            Effect = effect;
            X = X;
            Y = y;
        }
    }
}
