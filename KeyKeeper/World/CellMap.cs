using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.World
{
    public class CellMap : BaseMap<Cell>
    {
        public CellMap(int width, int height, Cell nullTile) : base(width, height, nullTile)
        {
            InitializeMap();
        }

        private void InitializeMap()
        {
            for(int y = 0; y < Height; y++)
            {
                for(int x = 0; x < Width; x++)
                {
                    SetTile(x, y, new Cell(NullTile.TileType));
                }
            }
        }

        public void SetTileType(int x, int y, TileType tileType)
        {
            GetTile(x, y).TileType = tileType;
        }
    }
}
