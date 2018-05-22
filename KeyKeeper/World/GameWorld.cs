using KeyKeeper.Entities;
using KeyKeeper.Helpers;
using Microsoft.Xna.Framework;
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

        private Dictionary<int, List<Creature>> _creatures = new Dictionary<int, List<Creature>>();

        public int LevelWidth(int depth) => GetLevel(depth).Width;
        public int LevelHeight(int depth) => GetLevel(depth).Height;
        public int Depth => _levels.Length;

        public void SetExplored(int x, int y, int depth, bool isExplored) => GetLevel(depth).SetExplored(x, y, isExplored);

        public bool InBounds(int x, int y, int depth) => GetLevel(depth).InBounds(x, y);
        public bool IsTransparent(int x, int y, int depth) => throw new NotImplementedException();

        public Cell GetCell(int x, int y, int depth) => GetLevel(depth).GetCell(x, y);
        
        public GameWorld(GameLevel[] levels)
        {
            if (levels == null) throw new ArgumentNullException("Cannot create game world with a null levels array.");
            if (levels.Length == 0) throw new ArgumentException("Cannot create game world with an empty levels array.");
            _levels = levels;

            _fov = new FieldOfView(this);

            for(int depth = 0; depth < levels.Length; depth++)
            {
                _creatures.Add(depth, new List<Creature>());
            }
        }

        public void ComputeFov(int x, int y, int depth, int radius, FovType type)
        {
            _fov.ClearFov();
            _fov.Compute(x, y, depth, radius, type);
        }
        
        public void SwitchCreatureLevel(Creature creature, int newDepth)
        {

        }

        public void AddCreature(Point spawn, int depth, Creature creature)
        {
            if (spawn == null) throw new ArgumentNullException("Cannot spawn creature at null point.");
            if (creature == null) throw new ArgumentNullException("Cannot spawn null creature.");
            if (!InBounds(spawn.X, spawn.Y, depth)) throw new ArgumentException($"Cannot spawn creature: {creature.Name} at invalid position: {spawn} depth {depth}.");
            _creatures[depth].Add(creature);
        }

        public List<Creature> GetCreatures(int depth)
        {
            if (depth < 0 || depth >= _levels.Length - 1) throw new ArgumentOutOfRangeException($"Cannot get creatures on level {depth}, out of bounds.");
            return _creatures[depth];
        }

        public Creature GetCreature(Point point, int depth)
        {
            if (point == null) throw new ArgumentNullException("Cannot get creature at null point.");
            return GetCreature(point.X, point.Y, depth);
        }

        public Creature GetCreature(int x, int y, int depth)
        {
            if (!InBounds(x, y, depth)) throw new ArgumentOutOfRangeException($"Cannot get creature at out of bounds position: {x}, {y} depth {depth}.");
            foreach(Creature creature in _creatures[depth])
            {
                if (creature.X == x && creature.Y == y) return creature;
            }
            return null;
        }
        
        public GameLevel GetLevel(int depth)
        {
            if (_levels == null) throw new InvalidOperationException("Cannot get level when levels array has not been defined.");
            if (depth < 0 || depth >= _levels.Length) throw new ArgumentOutOfRangeException($"Cannot get level ({depth}) less than 0 or greater than {_levels.Length - 1}.");
            return _levels[depth];
        }
    }
}
