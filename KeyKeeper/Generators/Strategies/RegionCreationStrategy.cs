using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyKeeper.Extensions;
using KeyKeeper.World;
using Microsoft.Xna.Framework;

namespace KeyKeeper.Generators.Strategies
{
    internal class RegionCreationStrategy : BaseRegionStrategy
    {

        private int[,] _regionIds;
        private int _currentRegion;
        
        public int[,] RegionIds => _regionIds;
        public int CurrentRegion => _currentRegion;

        private TileType RandomConnectorTile => _connectionTiles[_random.Next(_connectionTiles.Length - 1)];

        public RegionCreationStrategy(Random random, CellMap map, TileType[] emptyTiles, TileType[] solidTiles, TileType[] connectionTiles) : base(random, map, emptyTiles, solidTiles, connectionTiles)
        {

        }

        public override void Reset()
        {
            _regionIds = new int[_map.Width, _map.Height];
            _currentRegion = 1;
            base.Reset();
        }

        protected override void Generate(CellMap map)
        {
            CreateRegions();
        }

        private Dictionary<int, HashSet<Point>> CreateRegions()
        {
            Dictionary<int, HashSet<Point>> regions = new Dictionary<int, HashSet<Point>>();

            for (int y = 0; y < _map.Height; y++)
            {
                for (int x = 0; x < _map.Width; x++)
                {
                    if (_regionIds[x, y] == 0 && _emptyTiles.Any(t => t.Equals(_map.GetTileType(x, y))))
                    {
                        int next = _currentRegion++;
                        regions[next] = FillRegion(x, y, next);
                    }
                }
            }

            for(int y = 0; y < _map.Height; y++)
            {
                for(int x = 0; x < _map.Width; x++)
                {
                    _regionIds[x, y]--;
                }
            }

            _currentRegion -= 1;

            return regions;
        }

        private HashSet<Point> FillRegion(int x, int y, int regionId)
        {
            List<Point> open = new List<Point>();
            HashSet<Point> closed = new HashSet<Point>();

            open.Add(new Point(x, y));
            _regionIds[x, y] = regionId;

            while (open.Count > 0)
            {
                Point next = open[0];
                open.RemoveAt(0);

                foreach (Point neighbour in next.NeighboursAll())
                {
                    if (_map.InBounds(neighbour.X, neighbour.Y))
                    {
                        if (_regionIds[neighbour.X, neighbour.Y] > 0 || _solidTiles.Any(t => t.Equals(_map.GetTileType(neighbour.X, neighbour.Y))))
                        {
                            continue;
                        }

                        _regionIds[neighbour.X, neighbour.Y] = regionId;
                        open.Add(neighbour);
                        closed.Add(neighbour);
                    }
                }
            }

            return closed;
        }
    }
}
