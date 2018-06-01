using fNbt;
using KeyKeeper.Content;
using KeyKeeper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.World
{
    public class BaseMap<T> : IMap<T>
    {
        public T NullTile { get; }

        public int Width { get; }
        public int Height { get; }

        public T[,] Tiles { get; }
        
        public BaseMap(int width, int height, T nullTile)
        {
            Width = width;
            Height = height;
            Tiles = new T[Width, Height];
            NullTile = nullTile;
        }

        public T GetTile(int x, int y)
        {
            if (!InBounds(x, y)) return NullTile;
            return Tiles[x, y];
        }

        public bool InBounds(int x, int y)
        {
            return x >= 0 && y >= 0 && x < Width && y < Height;
        }

        public void SetTile(int x, int y, T tile)
        {
            if (InBounds(x, y)) Tiles[x, y] = tile;
        }


    }
}
