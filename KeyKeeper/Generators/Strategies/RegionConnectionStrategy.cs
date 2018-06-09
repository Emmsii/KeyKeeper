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
    internal class RegionConnectionStrategy : BaseRegionStrategy
    {
        private const float DEFAULT_RANDOM_CONNECTION_CHANCE = 0f;

        private int[,] _regionIds;
        private int _currentRegion;

        private float _randomConnectionChance = DEFAULT_RANDOM_CONNECTION_CHANCE;

        public float RandomConnectionChance {
            get { return _randomConnectionChance; }
            set {
                if (value < 0) value = 0;
                if (value > 1) value = 1;
                _randomConnectionChance = value;
            }
        }

        private TileType RandomConnectorTile => _connectionTiles[_random.Next(_connectionTiles.Length - 1)];

        public RegionConnectionStrategy(Random random, CellMap map, TileType[] emptyTiles, TileType[] solidTiles, TileType[] connectionTiles) : base(random, map, emptyTiles, solidTiles, connectionTiles)
        {

        }

        public override Type RequiredStrategy()
        {
            return typeof(RegionCreationStrategy);
        }

        protected override void Generate(CellMap map)
        {
            _regionIds = GetRegionCreationStrategy().RegionIds;
            _currentRegion = GetRegionCreationStrategy().CurrentRegion;

            ConnectRegions();
        }

        private void ConnectRegions()
        {
            Dictionary<Point, HashSet<int>> connectors = FindConnectors();
            List<Point> connectorPoints = connectors.Keys.OrderBy(p => _random.Next()).ToList();

            int[] merged = new int[_currentRegion];
            HashSet<int> unjoined = new HashSet<int>();

            for(int i = 0; i < _currentRegion; i++)
            {
                merged[i] = i;
                unjoined.Add(i);
            }

            HashSet<int> tempSet = new HashSet<int>();

            for(int i = connectorPoints.Count - 1; i > 1; i--)
            {
                Point connector = connectorPoints[i];

                HashSet<int> regions = connectors[connector];
                tempSet.Clear();

                foreach(int region in regions)
                {
                    tempSet.Add(merged[region]);
                }
                
                if(tempSet.Count <= 1)
                {
                    if(_random.NextDouble() < RandomConnectionChance)
                    {
                        _map.SetTileType(connector.X, connector.Y, RandomConnectorTile);
                    }
                    continue;
                }
                _map.SetTileType(connector.X, connector.Y, RandomConnectorTile);
                regions.Clear();
                regions.UnionWith(tempSet);

                int source = regions.FirstOrDefault();
                int[] destinations = GetDestinations(regions, merged);

                for(int regionIndex = 0; regionIndex < _currentRegion; regionIndex++)
                {
                    foreach(int destination in destinations)
                    {
                        if(merged[regionIndex] == destination)
                        {
                            merged[regionIndex] = source;
                        }
                    }
                }

                foreach(int destination in destinations)
                {
                    unjoined.Remove(destination);
                }
            }
        }

        private Dictionary<Point, HashSet<int>> FindConnectors()
        {
            Dictionary<Point, HashSet<int>> connectors = new Dictionary<Point, HashSet<int>>();

            for (int y = 1; y < _map.Height - 1; y++)
            {
                for (int x = 1; x < _map.Width - 1; x++)
                {
                    AddConnector(connectors, x, y);
                }
            }

            return connectors;
        }

        private void AddConnector(Dictionary<Point, HashSet<int>> connectors, int x, int y)
        {
            if (_solidTiles.Any(t => t.Equals(_map.GetTileType(x, y))))
            {
                HashSet<int> regions = new HashSet<int>();
                foreach (Point neighbour in new Point(x, y).NeighboursCardinal())
                {
                    if(_map.InBounds(neighbour.X, neighbour.Y))
                    {
                        int region = _regionIds[neighbour.X, neighbour.Y];
                        if(region >= 0 && !IsSolidTile(_map, neighbour.X, neighbour.Y))
                        {
                            regions.Add(region);
                        }
                    }

                    if(regions.Count > 1)
                    {
                        connectors[new Point(x, y)] = regions;
                    }
                }
            }
        }

        private int[] GetDestinations(HashSet<int> regions, int[] merged)
        {
            int[] destinations = new int[regions.Count];
            int index = 0;

            foreach(int region in regions)
            {
                destinations[index++] = merged[region];
            }

            return destinations;
        }

        private RegionCreationStrategy GetRegionCreationStrategy()
        {
            if (_requiresStrategy == null)
            {
                throw new InvalidOperationException("Missing required strategy.");
            }

            if (!(_requiresStrategy is RegionCreationStrategy))
            {
                throw new InvalidCastException("Invalid required strategy type.");
            }

            return _requiresStrategy as RegionCreationStrategy;
        }

    }
}
