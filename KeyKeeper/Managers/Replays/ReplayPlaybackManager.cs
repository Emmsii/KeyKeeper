using fNbt;
using KeyKeeper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Managers.Replays
{
    internal class ReplayPlaybackManager<T> : BaseReplayManager<T> where T : INbtSerializable
    {
        private Queue<T> _actions = new Queue<T>();
        
        public ReplayPlaybackManager(GameManager gameManager, string replayFolder, int queueMaxLength = DEFAULT_QUEUE_LENGTH, NbtCompression compressionMethod = DEFAULT_COMPRESSION) 
            : base(gameManager, replayFolder, queueMaxLength, compressionMethod)
        {

        }

        public void LoadReplay()
        {
            NbtCompound rootTag = ReplayFileManager.LoadRootTagFromFile(_replayFolderPath, _compressionMethod);

            NbtList eventsTag = rootTag.Get<NbtList>(ReplayTagManager.EVENTS_TAG);

            foreach (NbtTag tag in eventsTag)
            {
                ReplayEvent<T> replayEvent = new ReplayEvent<T>().LoadTag(tag as NbtCompound, _typeById);
                _replayEvents.Enqueue(replayEvent);
                _actions.Enqueue(replayEvent.Action);
            }
        }

        public T GetActionAtTick(int tick)
        {
            if(_replayEvents.Peek().Tick == tick)
            {
                _replayEvents.Dequeue();
                return _actions.Dequeue();
            }

            return default(T);
        }

        public IEnumerable<T> GetActions()
        {
            foreach (ReplayEvent<T> replayEvent in _replayEvents)
            {
                yield return replayEvent.Action;
            }
        }
    }
}
