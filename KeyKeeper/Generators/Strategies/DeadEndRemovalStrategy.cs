using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyKeeper.Helpers;
using KeyKeeper.World;
using Microsoft.Xna.Framework;

namespace KeyKeeper.Generators.Strategies
{
    internal class DeadEndRemovalStrategy : BaseGeneratorStrategy
    {
        private const int DEFAULT_DEADEND_REMOVAL_ITERATIONS = int.MaxValue;

        private int _deadEndRemovalIterations = DEFAULT_DEADEND_REMOVAL_ITERATIONS;

        public int DeadEndRemovalIterations {
            get { return _deadEndRemovalIterations; }
            set {
                if (value < 0) value = 0;
                _deadEndRemovalIterations = value;
            }
        }

        private readonly TileType _wallTile;
        private readonly TileType _fillTile;
        private readonly TileType[] _corridorTiles;

        private bool TileIsWall(CellMap map, int x, int y) => _wallTile.Equals(map.GetTileType(x, y));
        private bool TileIsCorridor(CellMap map, int x, int y) => _corridorTiles.Any(t => t.Equals(map.GetTileType(x, y)));

        public DeadEndRemovalStrategy(Random random, TileType wallTile, TileType fillTile, TileType[] corridorTiles) : base(random)
        {
            _wallTile = wallTile;
            _fillTile = fillTile;
            _corridorTiles = corridorTiles;
        }

        protected override void Generate(CellMap map)
        {
            RemoveDeadEnds(map);
        }

        public void RemoveDeadEnds(CellMap map)
        {
            if (DeadEndRemovalIterations <= 0)
            {
                return;
            }

            List<PointClass> deadEnds = new List<PointClass>();

            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    if (IsDeadEnd(map, x, y))
                    {
                        deadEnds.Add(new PointClass(x, y));
                    }
                }
            }

            for (int i = 0; i < DeadEndRemovalIterations && deadEnds.Count > 0; i++)
            {
                for (int j = deadEnds.Count - 1; j >= 0; j--)
                {
                    PointClass point = deadEnds[j];
                    map.SetTileType(point.X, point.Y, _wallTile);
                    if (!FindDeadEndNeighbour(map, point))
                    {
                        deadEnds.Remove(point);
                    }
                }
            }
        }

        private bool FindDeadEndNeighbour(CellMap map, PointClass deadEnd)
        {
            foreach (Direction direction in Direction.All)
            {
                if (IsDeadEnd(map, direction.NextX(deadEnd.X), direction.NextY(deadEnd.Y)))
                {
                    deadEnd.X = direction.NextX(deadEnd.X);
                    deadEnd.Y = direction.NextY(deadEnd.Y);
                    return true;
                }
            }
            return false;
        }

        private bool IsDeadEnd(CellMap map, int x, int y)
        {
            if (TileIsCorridor(map, x, y))
            {
                int wallNeighbours = 0;
                int nextX, nextY;

                foreach (Direction direction in Direction.All)
                {
                    nextX = direction.NextX(x);
                    nextY = direction.NextY(y);
                    if (TileIsWall(map, nextX, nextY))
                    {
                        wallNeighbours++;
                    }
                }
                return wallNeighbours >= 3;
            }
            return false;
        }

    }

    class PointClass
    {
        public int X { get; set; }
        public int Y { get; set; }

        public PointClass(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
