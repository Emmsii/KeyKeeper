using fNbt;
using KeyKeeper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Managers.Replays
{
    internal abstract class BaseReplayManager<T> where T : INbtSerializable
    {
        protected const int DEFAULT_QUEUE_LENGTH = 50;
        protected const NbtCompression DEFAULT_COMPRESSION = NbtCompression.None;

        protected readonly GameManager _gameManager;
        protected readonly NbtCompression _compressionMethod;

        protected readonly string _replayFolderPath;

        protected readonly Queue<ReplayEvent<T>> _replayEvents;
        protected readonly int _queueMaxLength;

        protected readonly Dictionary<int, Type> _typeById = new Dictionary<int, Type>();
        protected readonly Dictionary<Type, int> _idByType = new Dictionary<Type, int>();
        
        public BaseReplayManager(GameManager gameManager, string replayFolder, int queueMaxLength, NbtCompression compressionMethod)
        {
            _gameManager = gameManager;
            _replayFolderPath = replayFolder;
            _compressionMethod = compressionMethod;
            _queueMaxLength = queueMaxLength;
            _replayEvents = new Queue<ReplayEvent<T>>(_queueMaxLength);
        }

        public void RegisterType<U>(int typeId) where U : INbtSerializable
        {
            if (_typeById.ContainsKey(typeId))
            {
                throw new ReplayException($"The type id: {typeId} has already been registered.");
            }

            if (_idByType.ContainsKey(typeof(U)))
            {
                throw new ReplayException($"The type: {typeof(U)} has already been registered.");
            }

            _typeById.Add(typeId, typeof(U));
            _idByType.Add(typeof(U), typeId);
        }

        public int GetIdByType(Type type)
        {
            if(!_idByType.TryGetValue(type, out int value))
            {
                throw new ReplayException($"Unknown type: {type}.");
            }

            return value;
        }

        public Type GetTypeById(int typeId)
        {
            if(!_typeById.TryGetValue(typeId, out Type type))
            {
                throw new ReplayException($"Unknown type id: {typeId}.");
            }

            return type;
        }
    }
}
