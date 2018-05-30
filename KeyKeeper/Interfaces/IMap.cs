using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Interfaces
{
    public interface IMap<T> 
    {
        T NullTile { get; }

        int Width { get; }
        int Height { get; }
        T[,] Tiles { get; }

        T GetTile(int x, int y);
        void SetTile(int x, int y, T tile);
        bool InBounds(int x, int y);
    }
}
