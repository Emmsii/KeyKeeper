using KeyKeeper.Entities;
using KeyKeeper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fNbt;
using System.Diagnostics;

namespace KeyKeeper.Managers
{
    public class ReplayManager
    {
        private const int DEFAULT_QUEUE_LENGTH = 50;

        private readonly GameManager _gameManager;

        private readonly Queue<ReplayEvent> _replayEvents = new Queue<ReplayEvent>(DEFAULT_QUEUE_LENGTH);
        private readonly int _queueMaxLength;

        private int _currentBatchStartTick;
        private int _currentBatch;

        /**
         *
         * Replay files + replay header file both need to go in a new folder.
         * The folder will be inside the save game folder.
         * 
         */

        public ReplayManager(GameManager gameManager, int queueMaxLength = DEFAULT_QUEUE_LENGTH)
        {
            _gameManager = gameManager;
            _queueMaxLength = queueMaxLength;
        }

        public void StartNewReplay()
        {
            NbtCompound root = GetNewReplayHeader();
            var replayHeaderFile = new NbtFile(root);
            replayHeaderFile.SaveToFile("replay.dat", NbtCompression.None);
        }

        public void AddReplayEvent(IAction action)
        {
            _replayEvents.Enqueue(new ReplayEvent(_gameManager.Tick, action));

            if (_replayEvents.Count >= _queueMaxLength)
            {
                SaveEvents();
                _currentBatchStartTick = _gameManager.Tick;
                _currentBatch++;
            }
        }

        private void SaveEvents()
        {
            var watch = Stopwatch.StartNew();

            NbtCompound root = new NbtCompound("root")
            {
                GetEventHeaderTag()
            };

            NbtList eventsTag = new NbtList("events");

            while(_replayEvents.Count != 0)
            {
                ReplayEvent next = _replayEvents.Dequeue();
                eventsTag.Add(next.SaveTag());
            }

            root.Add(eventsTag);

            var eventFile = new NbtFile(root);
            eventFile.SaveToFile("r_" + _currentBatch + ".dat", NbtCompression.None);

            watch.Stop();
            Console.WriteLine("Events saved in " + watch.Elapsed);
        }

        private NbtCompound GetNewReplayHeader()
        {
            return new NbtCompound("replay_header")
            {
                new NbtInt("game_seed", _gameManager.GameSeed),
                new NbtInt("world_seed", _gameManager.WorldSeed)
                // TODO: Other required metadata that the game will need to recreate the game state.
            };
        }

        private NbtCompound GetEventHeaderTag()
        {
            return new NbtCompound("event_header")
            {
                new NbtInt("start_tick", _currentBatchStartTick),
                new NbtInt("end_tick", _gameManager.Tick),
                new NbtInt("batch", _currentBatch)
            };
        }

    }

    internal class ReplayEvent
    {
        public int Tick { get; private set; }
        public IAction Action { get; private set; }

        public ReplayEvent(int tick, IAction action)
        {
            Tick = tick;
            Action = action;
        }

        public NbtCompound SaveTag()
        {
            return new NbtCompound()
            {
                new NbtInt("tick", Tick),
                Action.WriteDataTo()
            };
        }

        public void LoadTag(NbtCompound tag, Dictionary<int, Type> types)
        {
            Tick = tag.Get<NbtInt>("tick")?.Value ?? -1;
        }
    }
}
