using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.World
{
    public class Room
    {
        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }

        public bool IsBorder(int x, int y) => X == x || Y == y || X + Width - 1 == X || Y + Height - 1 == y;
        public bool Contains(int x, int y) => x >= X && y >= Y && x < X + Width && y < Y + Height;

        public Room(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public bool Overlaps(Room other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            return X < other.X + other.Width && X + Width > other.X && Y < other.Y + other.Height && Y + Height > other.Y;
        }

        public void Fill(CellMap map, Random random, TileType[] tileTypes)
        {
            for (int y = Y; y < Y + Height; y++)
            {
                for (int x = X; x < X + Width; x++)
                {
                    map.SetTileType(x, y, tileTypes[random.Next(tileTypes.Length)]);
                }
            }
        }

        public void Fill(CellMap map, TileType tileType)
        {
            for(int y = Y; y < Y + Height; y++)
            {
                for(int x = X; x < X + Width; x++)
                {
                    map.SetTileType(x, y, tileType);
                }
            }
        }
    }
}
