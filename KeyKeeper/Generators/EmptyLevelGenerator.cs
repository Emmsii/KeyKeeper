using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyKeeper.World;

namespace KeyKeeper.Generators
{
    public class EmptyLevelGenerator : BaseLevelGenerator
    {
        private readonly GameLevel[] _levels;

        public EmptyLevelGenerator(int width, int height, int depth) : base(width, height, depth)
        {
            _levels = new GameLevel[depth];
        }

        public override GameLevel[] Build()
        {
            return _levels;
        }

        public override BaseLevelGenerator Generate()
        {
            for(int z = 0; z < _depth; z++)
            {
                for(int y = 0; y < _height; y++)
                {
                    for(int x = 0; x < _width; x++)
                    {
                        if(x == 0 || y == 0 || x == _width - 1 || y == _height - 1)
                        {
                            // set to wall tile
                        }
                        else
                        {
                            // set to floor tile
                        }
                    }
                }
            }

            return this;
        }
    }
}
