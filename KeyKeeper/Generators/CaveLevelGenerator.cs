using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyKeeper.Content;
using KeyKeeper.Interfaces;
using KeyKeeper.World;
using Microsoft.Xna.Framework;

namespace KeyKeeper.Generators
{
    internal class CaveLevelGenerator : BaseLevelGenerator
    {

        public CaveLevelGenerator(int width, int height, int depth, int seed) : base(width, height, depth, seed)
        {
            for (int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    if(x == 0 || y == 0 || x == width - 1 || y == height - 1)
                    {
                        _map.SetTile(x, y, Assets.GetCellType("wall").NewCellFromType());
                    }
                    else
                    {
                        _map.SetTile(x, y, Assets.GetCellType("floor").NewCellFromType());
                    }
                    //if(_random.NextDouble() < 0.5)
                    //{
                    //    _map.SetTile(x, y, Assets.GetCellType("wall").NewCellFromType());
                    //}
                    //else
                    //{
                    //    _map.SetTile(x, y, Assets.GetCellType("floor").NewCellFromType());
                    //}
                }
            }


            if(depth != 0)
            {
                _map.SetTile(_random.Next(1, width - 2), _random.Next(1, height - 2), Assets.GetCellType("stairs_up").NewCellFromType());
            }

            _map.SetTile(_random.Next(1, width - 2), _random.Next(1, height - 2), Assets.GetCellType("stairs_down").NewCellFromType());
        }

        public override IBuilder<GameLevel> Generate()
        {
            return this;
        }
    }
}
