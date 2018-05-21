using KeyKeeper.Interfaces;
using KeyKeeper.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Generators
{
    public abstract class BaseLevelGenerator : IBuilder<GameLevel>
    {
        protected readonly int _width;
        protected readonly int _height;
        protected readonly int _depth;

        protected readonly IMap<Cell> _map;

        public BaseLevelGenerator(int width, int height, int depth)
        {
            _width = width;
            _height = height;
            _depth = depth;

            _map = new BaseMap<Cell>(_width, _height);
        }

        public virtual GameLevel Build()
        {
            return new GameLevel(_map);
        }

        public abstract IBuilder<GameLevel> Generate();
    }
}
