using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyKeeper.Content;
using KeyKeeper.Interfaces;
using KeyKeeper.World;
using Microsoft.Xna.Framework;

namespace KeyKeeper.Generators
{
    internal class CaveLevelGenerator : BaseLevelGenerator
    {

        private readonly TileType[] _emptyTiles;
        private readonly TileType[] _solidTiles;

        public CaveLevelGenerator(int width, int height, int depth, int seed) : base(width, height, depth, seed)
        {
            _emptyTiles = new TileType[]
            {
                Assets.GetTileType("floor_dot")
            };

            _solidTiles = new TileType[]
            {
                Assets.GetTileType("rocks_1"),
                Assets.GetTileType("rocks_2"),
            };
        }

        //public override IBuilder<GameLevel> Generate()
        //{
        //    FillWithType(_solidTiles);
        //    AddRandomTiles(_emptyTiles, 50);

        //    SmoothTiles(8, _emptyTiles, _solidTiles);

        //    return this;
        //}

        private void SmoothTiles(int count, TileType[] emptyTile, TileType[] solidTile )
        {
            TileType[,] newTypes = new TileType[_width, _height];

            for (int i = 0; i < count; i++)
            {
                for (int y = 0; y < _height; y++)
                {
                    for (int x = 0; x < _width; x++)
                    {
                        SmoothTile(x, y, newTypes, emptyTile, solidTile);
                    }
                }

                ReplaceTileTypes(newTypes);
            }
        }

        private void SmoothTile(int x, int y, TileType[,] newTypes, TileType[] emptyTiles, TileType[] solidTiles)
        {
            int floors = 0;
            int walls = 0;

            for (int oy = -1; oy <= 1; oy++)
            {
                int ya = y + oy;
                for (int ox = -1; ox <= 1; ox++)
                {
                    int xa = x + ox;
                    if (!_map.InBounds(xa, ya))
                    {
                        continue;
                    }

                    if (emptyTiles.Contains(_map.GetTileType(xa, ya)))
                    {
                        floors++;
                    }
                    else
                    {
                        walls++;
                    }
                }
            }

            int rand = _random.Next(floors >= walls ? emptyTiles.Length : solidTiles.Length);
            newTypes[x, y] = floors >= walls ? emptyTiles[rand] : solidTiles[rand];
        }

        private void ReplaceTileTypes(TileType[,] newTypes)
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _map.SetTileType(x, y, newTypes[x, y]);
                }
            }
        }

        protected override bool IsMapValid()
        {
            throw new NotImplementedException();
        }

        protected override void InitializeStrategies()
        {
            throw new NotImplementedException();
        }
    }
}
