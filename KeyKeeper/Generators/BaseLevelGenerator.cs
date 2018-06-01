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

        protected readonly Random _random;

        public BaseLevelGenerator(int width, int height, int depth, int seed)
        {
            _width = width;
            _height = height;
            _depth = depth;
            _random = new Random(seed);

            _map = new CellMap(_width, _height, new Cell(Assets.GetTileType("wall")));
        }

        public virtual GameLevel Build()
        {
            return new GameLevel(_map, _depth);
        }

        public abstract IBuilder<GameLevel> Generate();
    }
}
