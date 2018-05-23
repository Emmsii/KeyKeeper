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

        protected readonly IMap<Cell> _map;

        protected readonly Random _random;

        public BaseLevelGenerator(int width, int height, int depth, Random random)
        {
            _width = width;
            _height = height;
            _depth = depth;
            _random = random;

            _map = new BaseMap<Cell>(_width, _height);
        }

        public virtual GameLevel Build()
        {
            return new GameLevel(_map);
        }

        public abstract IBuilder<GameLevel> Generate();
    }
}
