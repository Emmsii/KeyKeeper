using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyKeeper.Content;
using KeyKeeper.Interfaces;
using KeyKeeper.World;

namespace KeyKeeper.Generators
{
    public class CaveLevelGenerator : BaseLevelGenerator
    {

        public CaveLevelGenerator(int width, int height, int depth, Random random) : base(width, height, depth, random)
        {
            for (int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    if(_random.NextDouble() < 0.5)
                    {
                        _map.SetTile(x, y, Assets.GetCellType("wall").NewCellFromType(x, y));
                    }
                    else
                    {
                        _map.SetTile(x, y, Assets.GetCellType("floor").NewCellFromType(x, y));
                    }
                }
            }
        }

        public override IBuilder<GameLevel> Generate()
        {
            return this;
        }
    }
}
