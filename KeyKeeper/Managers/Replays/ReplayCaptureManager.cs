using KeyKeeper.Entities;
using KeyKeeper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fNbt;
using System.Diagnostics;
using System.IO;

namespace KeyKeeper.Managers.Replays
{
    internal class ReplayCaptureManager<T> : BaseReplayManager<T> where T : INbtSerializable
    {

        private int _currentBatchStartTick;
        private int _currentBatch;

        /**
         *
         * Replay files + replay header file both need to go in a new folder.
         * The folder will be inside the save game folder.
         * 
         */

        public ReplayCaptureManager(GameManager gameManager, string replayFolderPath, int queueMaxLength = DEFAULT_QUEUE_LENGTH, NbtCompression compressionMethod = DEFAULT_COMPRESSION)
            : base(gameManager, replayFolderPath, queueMaxLength, compressionMethod)
        {

        }

        public void AddReplayEvent(T action)
        {
            if (!_idByType.TryGetValue(action.GetType(), out int typeId))
            {
                throw new ReplayException($"Unknown type: {action.GetType()}. Type not registered.");
            }

            _replayEvents.Enqueue(new ReplayEvent<T>(_gameManager.Tick, typeId, action));

            if (_replayEvents.Count >= _queueMaxLength)
            {
                SaveEvents();
                _currentBatchStartTick = _gameManager.Tick;
                _currentBatch++;
            }
        }

        public void StartNewReplay()
        {
            ReplayFileManager.CheckAndCreateReplayFolder(_replayFolderPath);

            NbtCompound headerFileTag = ReplayTagManager.GenerateNewReplayHeaderTag(_gameManager);
            ReplayFileManager.SaveTagToFile(headerFileTag, _replayFolderPath + "replay.dat", _compressionMethod);
        }

        private void SaveEvents()
        {
            var watch = Stopwatch.StartNew();

            NbtCompound replayFileTag = ReplayTagManager.GenerateReplayFileTag(_replayEvents, _currentBatchStartTick, _gameManager.Tick, _currentBatch);
            ReplayFileManager.SaveTagToFile(replayFileTag, _replayFolderPath + "r_" + _currentBatch + ".dat", _compressionMethod);

            watch.Stop();
            Console.WriteLine("Events saved in " + watch.Elapsed.TotalMilliseconds);
        }

    }

}
