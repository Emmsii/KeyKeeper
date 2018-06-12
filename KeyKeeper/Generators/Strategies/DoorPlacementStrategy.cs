using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyKeeper.World;

namespace KeyKeeper.Generators.Strategies
{
    internal class DoorPlacementStrategy : BaseGeneratorStrategy
    {

        private const float DEFAULT_DOOR_CHANCE = 1f;

        private float _doorChance = DEFAULT_DOOR_CHANCE;

        public float DoorChance {
            get { return _doorChance; }
            set {
                if (value < 0) value = 0;
                if (value > 1) value = 1;
                _doorChance = value;
            }
        }

        private readonly TileType[] _corridorTiles;
        private readonly TileType[] _roomTiles;
        private readonly TileType _wallTile;
        private readonly TileType _doorTile;

        private bool TileIsCorridor(CellMap map, int x, int y) => _corridorTiles.Any(t => t.Equals(map.GetTileType(x, y)));
        private bool TileIsRoom(CellMap map, int x, int y) => _roomTiles.Any(t => t.Equals(map.GetTileType(x, y))) && GetRegionCreationStrategy().IsPointInRoom(x, y);
        private bool TileIsWall(CellMap map, int x, int y) => _wallTile.Equals(map.GetTileType(x, y));

        public DoorPlacementStrategy(Random random, TileType[] corridorTiles, TileType[] roomTiles, TileType wallTile, TileType doorTile) : base(random)
        {
            _corridorTiles = corridorTiles;
            _roomTiles = roomTiles;
            _wallTile = wallTile;
            _doorTile = doorTile;
        }

        public override Type RequiredStrategy()
        {
            return typeof(StandardRoomStrategy);
        }

        protected override void Generate(CellMap map)
        {
            PlaceDoors(map);
        }

        private void PlaceDoors(CellMap map)
        {
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    if(!TileIsCorridor(map, x, y) || _random.NextDouble() > DoorChance)
                    {
                        continue;
                    }

                    if (TileIsWall(map, x - 1, y) && TileIsWall(map, x + 1, y))
                    {
                        if ((TileIsCorridor(map, x, y - 1) && TileIsRoom(map, x, y + 1)) || (TileIsRoom(map, x, y - 1) && TileIsCorridor(map, x, y + 1)) || (TileIsRoom(map, x, y - 1) && TileIsRoom(map, x, y + 1)))
                        {
                            map.SetTileType(x, y, _doorTile);
                        }
                    }
                    else if (TileIsWall(map, x, y - 1) && TileIsWall(map, x, y + 1))
                    {
                        if ((TileIsCorridor(map, x - 1, y) && TileIsRoom(map, x + 1, y)) || (TileIsRoom(map, x - 1, y) && TileIsCorridor(map, x + 1, y)) || (TileIsRoom(map, x - 1, y) && TileIsRoom(map, x + 1, y)))
                        {
                            map.SetTileType(x, y, _doorTile);
                        }
                    }
                }
            }
        }

        private StandardRoomStrategy GetRegionCreationStrategy()
        {
            if (_requiresStrategy == null)
            {
                throw new InvalidOperationException("Missing required strategy.");
            }

            if (!(_requiresStrategy is StandardRoomStrategy))
            {
                throw new InvalidCastException("Invalid required strategy type.");
            }

            return _requiresStrategy as StandardRoomStrategy;
        }
    }
}
