using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyKeeper.World;
using KeyKeeper.Extensions;
using Microsoft.Xna.Framework;
using KeyKeeper.Helpers;

namespace KeyKeeper.Generators.Strategies
{
    internal class CorridorPlacementStrategy : BaseGeneratorStrategy
    {
        private const float DEFAULT_WINDING_CHANCE = 0.4f;
        private const int DEFAULT_MIN_CORRIDOR_LENGTH = 2;

        private float _windingChance = DEFAULT_WINDING_CHANCE;
        private int _minCorridorLength = DEFAULT_MIN_CORRIDOR_LENGTH;

        public float WindingChance {
            get { return _windingChance; }
            set {
                if (value < 0) value = 0;
                if (value > 1) value = 1;
                _windingChance = value;
            }
        }

        private readonly TileType _wallTile;
        private readonly TileType[] _corridorTiles;
        private readonly List<Direction> _directions = new List<Direction>();

        private bool TileIsWall(CellMap map, int x, int y) => _wallTile.Equals(map.GetTileType(x, y));
        private bool IsCarveable(CellMap map, Point point, Direction direction) => IsCarveable(map, new Point(direction.NextX(point.X, DEFAULT_MIN_CORRIDOR_LENGTH), direction.NextY(point.Y, DEFAULT_MIN_CORRIDOR_LENGTH)));
        private bool IsCarveable(CellMap map, Point point) => TileIsWall(map, point.X, point.Y) &&
                                                      TileIsWall(map, point.X + 1, point.Y + 1) &&
                                                      TileIsWall(map, point.X - 1, point.Y + 1) &&
                                                      TileIsWall(map, point.X + 1, point.Y - 1) &&
                                                      TileIsWall(map, point.X - 1, point.Y - 1);

        private TileType RandomCorridorTile => _corridorTiles[_random.Next(_corridorTiles.Length - 1)];
        
        public CorridorPlacementStrategy(Random random, TileType wallTile, TileType[] corridorTiles) : base(random)
        {
            _wallTile = wallTile;
            _corridorTiles = corridorTiles;
        }

        public override void Reset()
        {
            _directions.Clear();
            base.Reset();
        }

        protected override void Generate(CellMap map)
        {
            SpawnCorridors(map);
        }

        private void SpawnCorridors(CellMap map)
        {
            for (int y = 1; y < map.Height; y += 2)
            {
                for (int x = 1; x < map.Width; x += 2)
                {
                    Point point = new Point(x, y);
                    if (IsCarveable(map, point))
                    {
                        CarveMaze(map, point);
                    }
                }
            }
        }
               
        private void CarveMaze(CellMap map, Point point)
        {
            Direction lastDirection = null;
            while (true)
            {
                map.SetTileType(point.X, point.Y, RandomCorridorTile);
                _directions.Clear();

                foreach (Direction direction in Direction.All.OrderBy(d => _random.Next()))
                {
                    if (IsCarveable(map, point, direction))
                    {
                        _directions.Add(direction);
                    }
                }

                if (_directions.Count <= 0) return;

                Direction carvingDirection;

                if (lastDirection != null && _directions.Contains(lastDirection) && _random.NextDouble() > WindingChance)
                {
                    carvingDirection = lastDirection;
                }
                else
                {
                    carvingDirection = _directions[_random.Next(_directions.Count)];
                }

                lastDirection = carvingDirection;

                carvingDirection.Next(ref point);
                map.SetTileType(point.X, point.Y, RandomCorridorTile);
                carvingDirection.Next(ref point);
            }
        }
    }
}
