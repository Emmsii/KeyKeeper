using KeyKeeper.Interfaces;
using KeyKeeper.World;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Generators
{
    public class WorldGenerator : IBuilder<GameWorld>
    {
        private readonly int _width;
        private readonly int _height;
        private readonly int _depth;

        private readonly int _worldSeed;
        private readonly Random _random;

        private readonly GameLevel[] _levels;

        public WorldGenerator(int width, int height, int depth, int worldSeed)
        {
            _width = width;
            _height = height;
            _depth = depth;
            _worldSeed = worldSeed;
            _random = new Random(_worldSeed);

            _levels = new GameLevel[_depth];
        }

        public GameWorld Build()
        {
            return new GameWorld(_levels);
        }

        public IBuilder<GameWorld> Generate()
        {
            Console.WriteLine($"World Seed: {_worldSeed}");
            Console.WriteLine($"Generating {_width}x{_height}x{_depth} world...");

            var watch = Stopwatch.StartNew();

            for(int z = 0; z < _depth; z++)
            {
                _levels[z] = new CaveLevelGenerator(_width, _height, z).Generate().Build();
            }
            
            watch.Stop();
            Console.WriteLine($"Generated in {watch.Elapsed.TotalMilliseconds}ms");

            return this;
        }
    }
}
