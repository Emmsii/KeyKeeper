using KeyKeeper.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Generators
{
    public abstract class BaseLevelGenerator
    {
        protected readonly int _width;
        protected readonly int _height;
        protected readonly int _depth;

        public BaseLevelGenerator(int width, int height, int depth)
        {
            _width = width;
            _height = height;
            _depth = depth;
        }

        public abstract BaseLevelGenerator Generate();
        public abstract GameLevel[] Build();
    }
}
