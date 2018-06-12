using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyKeeper.Content;
using KeyKeeper.Generators.Strategies;
using KeyKeeper.Interfaces;
using KeyKeeper.World;

namespace KeyKeeper.Generators
{
    internal class RoomLevelGenerator : BaseLevelGenerator
    {

        private const float DEFAULT_MIN_WALKABLE_PERCENT = 0.4f;

        private float _minWalkablePercent = DEFAULT_MIN_WALKABLE_PERCENT;

        public float MinWalkablePercent {
            get { return _minWalkablePercent; }
            set {
                if (value < 0) value = 0;
                if (value > 1) value = 1;
                _minWalkablePercent = value;
            }
        }

        private readonly TileType _wallTile;
        private readonly TileType[] _roomFloorTiles;
        private readonly TileType[] _corridorTiles;
        private readonly TileType[] _wallTiles;
        private readonly TileType[] _rockTiles;

        private readonly TileType _doorTile;

        private TileType[] EmptyTiles => _roomFloorTiles.Union(_corridorTiles).ToArray();
        private TileType[] SolidTiles => _wallTiles;

        public RoomLevelGenerator(int width, int height, int depth, int seed) : base(width, height, depth, seed)
        {
            FillTile = Assets.GetTileType("wall_all");

            _wallTile = FillTile;

            _roomFloorTiles = new TileType[]
            {
                Assets.GetTileType("bricks_1"),
                Assets.GetTileType("bricks_2"),
                Assets.GetTileType("bricks_3"),
            };

            _corridorTiles = new TileType[]
            {
                Assets.GetTileType("bricks_1"),
                Assets.GetTileType("bricks_2"),
                Assets.GetTileType("bricks_3"),
            };

            _wallTiles = new TileType[]
            {
                Assets.GetTileType("wall_hor"),
                Assets.GetTileType("wall_ver_end"),
                Assets.GetTileType("wall_hor"),
                Assets.GetTileType("wall_bot_lef"),
                Assets.GetTileType("wall_hor"),
                Assets.GetTileType("wall_bot_rig"),
                Assets.GetTileType("wall_hor"),
                Assets.GetTileType("wall_hor_top"),
                Assets.GetTileType("wall_ver"),
                Assets.GetTileType("wall_ver"),
                Assets.GetTileType("wall_top_lef"),
                Assets.GetTileType("wall_ver_lef"),
                Assets.GetTileType("wall_top_rig"),
                Assets.GetTileType("wall_ver_rig"),
                Assets.GetTileType("wall_hor_bot"),
                Assets.GetTileType("wall_all")
            };

            _rockTiles = new TileType[]
            {
                Assets.GetTileType("rocks_1"),
                Assets.GetTileType("rocks_2"),
                Assets.GetTileType("rocks_3")
            };

            _doorTile = Assets.GetTileType("door_closed");
        }

        protected override void InitializeStrategies()
        {
            AddGeneratorStrategy(new StandardRoomStrategy(_random, _roomFloorTiles)
            {
                MaxRoomCount = 10,
                MinRoomSize = 5,
                MaxRoomSize = 7
            });

            AddGeneratorStrategy(new CorridorPlacementStrategy(_random,
                                                               _wallTile,
                                                               _corridorTiles)
            {
                WindingChance = 0.375f
            });

            AddGeneratorStrategy(new RegionCreationStrategy(_random,
                                                    _map,
                                                    EmptyTiles,
                                                    new TileType[] { _wallTile },
                                                    _corridorTiles));

            AddGeneratorStrategy(new RegionConnectionStrategy(_random,
                                                    _map,
                                                    EmptyTiles,
                                                    new TileType[] { _wallTile },
                                                    _corridorTiles));

            AddGeneratorStrategy(new DeadEndRemovalStrategy(_random,
                                                            _wallTile,
                                                            _corridorTiles)
            {
                //DeadEndRemovalIterations = 5
            });

            // After map is valid

            AddPostValidStrategy(new DoorPlacementStrategy(_random,
                                                           _corridorTiles,
                                                           _roomFloorTiles,
                                                           _wallTile,
                                                           _doorTile));

            AddPostValidStrategy(new WallPlacementStrategy(_random,
                                                           _wallTile,
                                                           _wallTiles,
                                                           new TileType[] { _doorTile },
                                                           EmptyTiles,
                                                           _rockTiles));
        }

        protected override void Reset()
        {

            base.Reset();
        }

        protected override bool IsMapValid()
        {
            int empty = 0;
            int total = _width * _height;

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    if (!_map.GetTileType(x, y).IsSolid)
                    {
                        empty++;
                    }
                }
            }

            return ((float)empty / total) >= MinWalkablePercent;
        }


    }
}
