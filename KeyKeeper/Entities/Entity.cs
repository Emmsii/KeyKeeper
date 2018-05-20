using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyKeeper.World;
using Microsoft.Xna.Framework;
using MonoGame;

namespace KeyKeeper.Entities
{
    public abstract class Entity
    {
        private static int EntityCount = 0;

        protected GameWorld _world;
        public GameWorld World { get { return _world; } }

        public int ID { get; private set; }

        private Point _location = new Point();
        private Point Location { get { return _location; } }

        public int X { get { return _location.X; } set { _location.X = value; } }
        public int Y { get { return _location.X; } set { _location.X = value; } }
        public int Depth { get; protected set; }

        public string Name { get {return _name; } protected set { _name = value; } }

        protected string _name;
        //protected readonly Sprite _sprite;
        private HashSet<string> _flags = new HashSet<string>();

        public bool AddFlag(string flag) => _flags.Add(flag);
        public bool RemoveFlag(string flag) => _flags.Remove(flag);
        public bool HasFlag(string flag) => _flags.Contains(flag);

        public Entity(string name)
        {
            _name = name;
        }

        public void Init(GameWorld world)
        {
            ID = EntityCount++;
            _world = world;
        }

        public void SetPosition(int x, int y, int z)
        {
            X = x;
            Y = Y;
        }

        public override string ToString()
        {
            return $"{Name} @ {X}, {Y}";
        }
    }
}
