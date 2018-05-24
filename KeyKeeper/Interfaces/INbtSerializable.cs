using fNbt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Interfaces
{
    public interface INbtSerializable
    {
        NbtTag WriteDataTo();
        void SetDataFrom(NbtCompound tag);

        INbtSerializable NewInstance();
    }
}
