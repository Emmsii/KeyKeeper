using KeyKeeper.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Interfaces
{
    internal interface IGeneratorStrategy
    {
        Type RequiredStrategy();
        void SetRequiredStrategy(IGeneratorStrategy strategy);

        void RunStrategy(CellMap map);
        void Reset();

        bool HasCompleted();
    }
}
