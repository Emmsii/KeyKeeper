using KeyKeeper.Extensions;
using KeyKeeper.Interfaces;
using KeyKeeper.World;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Generators.Strategies
{
    internal class WallPlacementStrategy : BaseGeneratorStrategy
    {

        private readonly TileType _wallTile;
        private readonly TileType[] _wallTiles;
        private readonly TileType[] _validConnectors;
        private readonly TileType[] _airTiles;
        private readonly TileType[] _rockTiles;
        
        private bool TileIsWall(CellMap map, int x, int y) => _wallTiles.Any(t => t.Equals(map.GetTileType(x, y)));
        private bool TileIsAir(CellMap map, int x, int y) => _airTiles.Any(t => t.Equals(map.GetTileType(x, y)));
        private bool IsValidConnector(CellMap map, int x, int y) => _validConnectors.Any(t => t.Equals(map.GetTileType(x, y)));

        public WallPlacementStrategy(Random random, TileType wallTile, TileType[] wallTiles, TileType[] validConnectors, TileType[] airTiles, TileType[] rockTiles) : base(random)
        {
            _wallTile = wallTile;
            _wallTiles = wallTiles;
            _validConnectors = validConnectors;
            _airTiles = airTiles;
            _rockTiles = rockTiles;
        }
        
        protected override void Generate(CellMap map)
        {
            PlaceWalls(map);    
        }

        private void PlaceWalls(CellMap map)
        {
            sbyte[,] bitmask = CreateWallBitmask(map);
            TileType[,] newTiles = GetNewTilesFromBitmask(bitmask, map);
            
            AddRocks(map);
            AddToCellMap(newTiles, map);
        }
        
        private sbyte[,] CreateWallBitmask(CellMap map)
        {
            sbyte[,] bitmask = new sbyte[map.Width, map.Height];

            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    bitmask[x, y] = -1;

                    if (TileIsWall(map, x, y) && TileTouchesAir(map, x, y))
                    {
                        bitmask[x, y] = 0;
                        if (WallCanJoinTile(map, x, y - 1)) bitmask[x, y] += 1;
                        if (WallCanJoinTile(map, x, y + 1)) bitmask[x, y] += 8;
                        if (WallCanJoinTile(map, x + 1, y)) bitmask[x, y] += 4;
                        if (WallCanJoinTile(map, x - 1, y)) bitmask[x, y] += 2;
                    }
                }
            }

            return bitmask;
        }

        private TileType[,] GetNewTilesFromBitmask(sbyte[,] bitmask, CellMap map)
        {
            TileType[,] newTiles = new TileType[map.Width, map.Height];
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    if (bitmask[x, y] != -1)
                    {
                        newTiles[x, y] = GetWallFromBit(bitmask[x, y]);
                    }
                }
            }
            return newTiles;
        }

        private void AddRocks(CellMap map)
        {
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    if (TileIsWall(map, x, y) && !TileTouchesAir(map, x, y))
                    {
                        map.SetTileType(x, y, _rockTiles[_random.Next(_rockTiles.Length)]);
                    }
                }
            }
        }

        private void AddToCellMap(TileType[,] newTiles, CellMap map)
        {
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    if (newTiles[x, y] != null && TileIsWall(map, x, y))
                    {
                        map.SetTileType(x, y, newTiles[x, y]);
                    }
                }
            }
        }

        private bool TileTouchesAir(CellMap map, int x, int y)
        {
            foreach (Point point in new Point(x, y).NeighboursAll())
            {
                if (TileIsAir(map, point.X, point.Y)) return true;
            }
            return false;
        }

        private bool WallCanJoinTile(CellMap map, int x, int y)
        {
            return (TileIsWall(map, x, y) || IsValidConnector(map, x, y)) && TileTouchesAir(map, x, y);
        }

        private TileType GetWallFromBit(sbyte bit)
        {
            if (bit < 0 || bit >= _wallTiles.Length - 1) return _wallTiles[_wallTiles.Length - 1];
            return _wallTiles[bit];
        }
    }
}
