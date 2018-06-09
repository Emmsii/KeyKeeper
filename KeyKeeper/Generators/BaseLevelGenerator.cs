using KeyKeeper.Content;
using KeyKeeper.Interfaces;
using KeyKeeper.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Generators
{
    internal abstract class BaseLevelGenerator : IBuilder<GameLevel>
    {
        protected readonly int _width;
        protected readonly int _height;
        protected readonly int _depth;

        protected readonly CellMap _map;

        private List<IGeneratorStrategy> _strategies = new List<IGeneratorStrategy>();

        protected readonly Random _random;

        public TileType FillTile {
            get;
            set;
        }

        public BaseLevelGenerator(int width, int height, int depth, int seed)
        {
            _width = width;
            _height = height;
            _depth = depth;
            _random = new Random(seed);

            _map = new CellMap(_width, _height, new Cell(Assets.GetTileType("rocks_1")));
        }

        public virtual GameLevel Build()
        {
            return new GameLevel(_map, _depth);
        }

        public IBuilder<GameLevel> Generate()
        {
            InitializeStrategies();

            do
            {
                Reset();
                foreach(IGeneratorStrategy strategy in _strategies)
                {
                    Type requiredType = strategy.RequiredStrategy();
                    
                    if(requiredType != null)
                    {
                        IGeneratorStrategy required = GetNextCompletedStrategyOfType(requiredType);
                        strategy.SetRequiredStrategy(required);
                    }
                    strategy.RunStrategy(_map);
                }
            } while (!IsMapValid());

            return this;
        }

        protected abstract void InitializeStrategies();
        protected abstract bool IsMapValid();

        protected virtual void Reset()
        {
            _strategies.ForEach(s => s.Reset()); 
            FillWithType(FillTile ?? _map.NullTile.TileType);
        }

        public IGeneratorStrategy GetNextCompletedStrategyOfType(Type type)
        {
            foreach(IGeneratorStrategy strategy in _strategies)
            {
                if(strategy.HasCompleted() && strategy.GetType() == type)
                {
                    return strategy;
                }
            }

            throw new InvalidOperationException($"Unable to find a completed generator strategy of type '{type}'.");
        }

        protected void AddRandomTiles(TileType[] typesToAdd, int percentageChance, params TileType[] onlyOnTypes)
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    if (onlyOnTypes.Length == 0 || onlyOnTypes.Contains(_map.GetTileType(x, y)))
                    {
                        if (_random.Next(100) < percentageChance)
                        {
                            _map.SetTileType(x, y, typesToAdd[_random.Next(typesToAdd.Length)]);
                        }
                    }
                }
            }
        }

        public void FillWithType(params TileType[] tileTypes)
        {
            if (tileTypes.Length == 0)
            {
                throw new ArgumentException("Must specify at least one tile type to fill map with.");
            }

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _map.SetTileType(x, y, tileTypes[_random.Next(tileTypes.Length)]);
                }
            }
        }

        public void AddGeneratorStrategy(IGeneratorStrategy strategy)
        {
            _strategies.Add(strategy);
        }
    }


}
