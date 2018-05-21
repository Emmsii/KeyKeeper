using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyKeeper.Interfaces;
using KeyKeeper.World;

namespace KeyKeeper.Generators
{
    public class CaveLevelGenerator : BaseLevelGenerator
    {
        
        public CaveLevelGenerator(int width, int height, int depth) : base(width, height, depth)
        {

        }

        public override IBuilder<GameLevel> Generate()
        {
            return this;
        }
    }
}
