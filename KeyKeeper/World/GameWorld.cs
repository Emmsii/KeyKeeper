using KeyKeeper.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.World
{
    public class GameWorld
    {
        private readonly GameLevel[] _levels;
        private readonly FieldOfView _fov;

        public int CurrentLevelWidth(int depth) => GetLevel(depth).Width;
        public int CurrentLevelHeight(int depth) => GetLevel(depth).Height;
        public int Depth => _levels.Length;

        public void SetExplored(int x, int y, int depth, bool isExplored) => GetLevel(depth).SetExplored(x, y, isExplored);

        public bool InBounds(int x, int y, int depth) => GetLevel(depth).InBounds(x, y);
        public bool IsTransparent(int x, int y, int depth) => throw new NotImplementedException();

        public GameWorld(GameLevel[] levels)
        {
            if (levels == null) throw new ArgumentNullException("Cannot create game world with a null levels array.");
            if (levels.Length == 0) throw new ArgumentException("Cannot create game world with an empty levels array.");
            _levels = levels;

            _fov = new FieldOfView(this);
        }

        public void ComputeFov(int x, int y, int depth, int radius, FovType type)
        {
            _fov.ClearFov();
            _fov.Compute(x, y, depth, radius, type);
        }

        public GameLevel GetLevel(int depth)
        {
            if (_levels == null) throw new InvalidOperationException("Cannot get level when levels array has not been defined.");
            if (depth < 0 || depth >= _levels.Length) throw new ArgumentOutOfRangeException($"Cannot get level ({depth}) less than 0 or greater than {_levels.Length - 1}.");
            return _levels[depth];
        }
    }
}
