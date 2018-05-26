using fNbt;
using KeyKeeper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Managers.Replays
{
    internal class ReplayEvent<T> where T : INbtSerializable
    {
        public int Tick { get; private set; }
        public int TypeId { get; private set; }
        public T Action { get; private set; }

        public ReplayEvent() { }

        public ReplayEvent(int tick, int typeId, T action)
        {
            Tick = tick;
            TypeId = typeId;
            Action = action;
        }

        public NbtCompound SaveTag()
        {
            return new NbtCompound()
            {
                new NbtInt("tick", Tick),
                new NbtInt("type", TypeId),
                Action.WriteDataToTag("action")
            };
        }

        public ReplayEvent<T> LoadTag(NbtCompound tag, Dictionary<int, Type> types)
        {
            Tick = tag.Get<NbtInt>("tick")?.Value ?? -1;
            TypeId = tag.Get<NbtInt>("type")?.Value ?? -1;
            Action = LoadAction(tag, TypeId, types);
            return this;
        }

        private T LoadAction(NbtCompound tag, int typeId, Dictionary<int, Type> types)
        {
            if (!types.TryGetValue(TypeId, out Type type))
            {
                throw new ApplicationException($"Unknown type: {TypeId}.");
            }
            object obj = FormatterServices.GetUninitializedObject(type);
            INbtSerializable deserialized = (INbtSerializable)obj;
            deserialized.SetDataFromTag(tag.Get<NbtCompound>("action"));
            return (T)deserialized;
        }
    }
}
