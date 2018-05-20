using KeyKeeper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Managers
{
    public class ReplayManager
    {
        private const int DEFAULT_MAX_QUEUE_LENGTH = 100;

        private readonly Queue<ReplayEvent> _replayEvents = new Queue<ReplayEvent>();
        private readonly int _queueMaxLength;

        

        public ReplayManager() : this(DEFAULT_MAX_QUEUE_LENGTH) { }

        public ReplayManager(int queueMaxLength)
        {
            _queueMaxLength = queueMaxLength;
        }

    }

    internal class ReplayEvent
    {

    }
}
