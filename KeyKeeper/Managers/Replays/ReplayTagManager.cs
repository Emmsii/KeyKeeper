using fNbt;
using KeyKeeper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Managers.Replays
{
    internal static class ReplayTagManager
    {
        // Shared Tags
        public const string ROOT_TAG = "root";
        public const string HEADER_TAG = "header";
        public const string EVENTS_TAG = "events";

        // Main Header File Tags
        public const string GAME_SEED_TAG = "game_seed";
        public const string WORLD_SEED_TAG = "world_seed";
        public const string LAST_BATCH_TAG = "last_batch";

        // Replay File Tags
        public const string TICK_TAG = "tick";
        public const string TYPE_TAG = "type";
        public const string START_TICK_TAG = "start_tick";
        public const string END_TICK_TAG = "end_tick";
        public const string CURRENT_BATCH_TAG = "current_batch";

        // Compiled Replay File Tags
        public const string TOTAL_BATCHES_TAG = "total_batches";
        public const string BATCH_SIZE_TAG = "batch_size";
        public const string TOTAL_TICKS_TAG = "total_ticks";

        public static NbtCompound GenerateNewReplayHeaderTag(GameManager gameManager)
        {
            return new NbtCompound(ROOT_TAG)
            {
                new NbtCompound(HEADER_TAG)
                {
                    new NbtInt(GAME_SEED_TAG, gameManager.GameSeed),
                    new NbtInt(WORLD_SEED_TAG, gameManager.WorldSeed),
                }
            };
        }

        public static NbtCompound GenerateReplayFileTag<T>(Queue<ReplayEvent<T>> replayEvents, int startTick, int endTick, int currentBatch) where T : INbtSerializable
        {
            return new NbtCompound(ROOT_TAG)
            {
                GenerateReplayFileHeaderTag(startTick, endTick, currentBatch),
                GenerateEventsListTag(replayEvents)
            };
        }

        private static NbtCompound GenerateReplayFileHeaderTag(int startTick, int endTick, int currentBatch)
        {
            return new NbtCompound(HEADER_TAG)
            {
                new NbtInt(START_TICK_TAG, startTick),
                new NbtInt(END_TICK_TAG, endTick),
                new NbtInt(CURRENT_BATCH_TAG, currentBatch)
            };
        }

        private static NbtList GenerateEventsListTag<T>(Queue<ReplayEvent<T>> replayEvents) where T : INbtSerializable
        {
            NbtList eventsTag = new NbtList(EVENTS_TAG);

            while (replayEvents.Count != 0)
            {
                ReplayEvent<T> next = replayEvents.Dequeue();
                eventsTag.Add(next.SaveTag());
            }

            return eventsTag;
        }

    }
}
