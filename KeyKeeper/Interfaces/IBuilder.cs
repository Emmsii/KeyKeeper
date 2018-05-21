using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Interfaces
{
    public interface IBuilder<T>
    {
        T Build();
        IBuilder<T> Generate();
    }
}
