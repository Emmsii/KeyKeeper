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

        private readonly TileType _floorTile;
        private readonly TileType[] _wallTiles;
        private readonly TileType[] _corridorTiles;

        public RoomLevelGenerator(int width, int height, int depth, int seed) : base(width, height, depth, seed)
        {
            FillTile = Assets.GetTileType("wall_all");

            _floorTile = Assets.GetTileType("floor_dot");

            _wallTiles = new TileType[]{
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

            _corridorTiles = new TileType[]
            {
                Assets.GetTileType("bricks_1"),
                Assets.GetTileType("bricks_2"),
                Assets.GetTileType("bricks_3"),
            };
        }

        protected override void InitializeStrategies()
        {
            AddGeneratorStrategy(new StandardRoomStrategy(_random, _floorTile));
            AddGeneratorStrategy(new CorridorPlacementStrategy(_random,
                                                               Assets.GetTileType("wall_all"),
                                                               _corridorTiles)
                                 {
                                     WindingChance = 0.45f
                                 });
            AddGeneratorStrategy(new RegionCreationStrategy(_random,
                                                    _map,
                                                    new TileType[] { Assets.GetTileType("floor_dot") }.Union(_corridorTiles).ToArray(),
                                                    new TileType[] { Assets.GetTileType("wall_all") },
                                                    _corridorTiles));
            AddGeneratorStrategy(new RegionConnectionStrategy(_random,
                                                    _map,
                                                    new TileType[] { Assets.GetTileType("floor_dot") }.Union(_corridorTiles).ToArray(),
                                                    new TileType[] { Assets.GetTileType("wall_all") },
                                                    _corridorTiles));
            AddGeneratorStrategy(new DeadEndRemovalStrategy(_random,
                                                            Assets.GetTileType("wall_all"),
                                                            Assets.GetTileType("rocks_1"),
                                                            _corridorTiles));

            AddGeneratorStrategy(new WallPlacementStrategy(_random,
                                                           _wallTiles[_wallTiles.Length - 1],
                                                           _wallTiles,
                                                           new TileType[0],
                                                           new TileType[] {
                                                               Assets.GetTileType("floor_dot"),
                                                               Assets.GetTileType("bricks_1"),
                                                               Assets.GetTileType("bricks_2"),
                                                               Assets.GetTileType("bricks_3"),
                                                           },
                                                           new TileType[]{
                                                               Assets.GetTileType("rocks_1"),
                                                               Assets.GetTileType("rocks_2"),
                                                               Assets.GetTileType("rocks_3"),
                                                           }));
        }

        protected override void Reset()
        {

            base.Reset();
        }

        protected override bool IsMapValid()
        {
            return true;
        }


    }
}
