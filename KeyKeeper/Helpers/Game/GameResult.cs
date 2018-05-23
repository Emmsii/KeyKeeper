using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Helpers.Game
{
    public sealed class GameResult
    {
        public List<GameEvent> Events { get; } = new List<GameEvent>();
        public bool MadeProgress { private get; set; }
        public bool NeedsRefresh => MadeProgress || Events.Count > 0;

        public void AddEvent(GameEvent gameEvent)
        {
            Events.Add(gameEvent);
        }
    }
}
