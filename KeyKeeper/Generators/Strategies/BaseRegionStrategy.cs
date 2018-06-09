using KeyKeeper.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Generators.Strategies
{
    internal abstract class BaseRegionStrategy : BaseGeneratorStrategy
    {
        protected readonly CellMap _map;

        protected readonly TileType[] _emptyTiles;
        protected readonly TileType[] _solidTiles;
        protected readonly TileType[] _connectionTiles;

        protected bool IsEmptyTile(CellMap map, int x, int y) => _emptyTiles.Any(t => t.Equals(map.GetTileType(x, y)));
        protected bool IsSolidTile(CellMap map, int x, int y) => _solidTiles.Any(t => t.Equals(map.GetTileType(x, y)));
        protected bool IsConnectionTile(CellMap map, int x, int y) => _connectionTiles.Any(t => t.Equals(map.GetTileType(x, y)));

        public BaseRegionStrategy(Random random, CellMap map, TileType[] emptyTiles, TileType[] solidTiles, TileType[] connectionTiles) : base(random)
        {
            _map = map;
            _emptyTiles = emptyTiles;
            _solidTiles = solidTiles;
            _connectionTiles = connectionTiles;
        }
    }
}
