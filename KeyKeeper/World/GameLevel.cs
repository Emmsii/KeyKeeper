using fNbt;
using KeyKeeper.Content;
using KeyKeeper.Entities;
using KeyKeeper.Helpers;
using KeyKeeper.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.World
{
    public class GameLevel
    {
        private readonly IMap<Cell> _cellMap;
        private readonly FieldOfView _fov;
        private IMap<bool> _exploredMap;

        public int Width { get { return _cellMap.Width; } }
        public int Height { get { return _cellMap.Height; } }

        public int Depth { get; }

        private readonly List<Creature> _creatures = new List<Creature>();
        public List<Creature> Creatures => _creatures;

        public bool InBounds(int x, int y) => _cellMap.InBounds(x, y);
        public bool IsExplored(int x, int y) => _exploredMap.InBounds(x, y) && _exploredMap.GetTile(x, y);
        public bool IsTransparent(int x, int y) => GetCell(x, y).IsTransparent;
        public bool IsSolid(int x, int y) => GetCell(x, y).IsSolid;

        public Cell GetCell(int x, int y) => _cellMap.GetTile(x, y);

        public GameLevel(IMap<Cell> cellMap, int depth)
        {
            _cellMap = cellMap;
            Depth = depth;
            _fov = new FieldOfView(this);
            _exploredMap = new BaseMap<bool>(_cellMap.Width, _cellMap.Height, false);
        }

        public void ComputeFov(int x, int y, int radius, FovType type)
        {
            _fov.ClearFov();
            _fov.Compute(x, y, radius, type);
        }

        public void SetExplored(int x, int y, bool isExplored)
        {
            if (!_exploredMap.InBounds(x, y)) return;
            _exploredMap.SetTile(x, y, isExplored);
        }

        public void AddCreature(Point spawn, Creature creature)
        {
            if (spawn == null) throw new ArgumentNullException("Cannot spawn creature at null point.");
            if (creature == null) throw new ArgumentNullException("Cannot spawn null creature.");
            if (!InBounds(spawn.X, spawn.Y)) throw new ArgumentException($"Cannot spawn creature: {creature.Name} at invalid position: {spawn}.");
            _creatures.Add(creature);
            creature.X = spawn.X;
            creature.Y = spawn.Y;
            creature.Init(this);
        }

        public void SwitchCreatureLevel(Creature creature, int newDepth)
        {

        }

        public Creature GetCreature(Point point)
        {
            if (point == null) throw new ArgumentNullException("Cannot get creature at null point.");
            return GetCreature(point.X, point.Y);
        }

        public Creature GetCreature(int x, int y)
        {
            if (!InBounds(x, y)) throw new ArgumentOutOfRangeException($"Cannot get creature at out of bounds position: {x}, {y}.");
            foreach (Creature creature in _creatures)
            {
                if (creature.X == x && creature.Y == y) return creature;
            }
            return null;
        }
    }
}
