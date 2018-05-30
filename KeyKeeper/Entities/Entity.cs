using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyKeeper.Graphics;
using KeyKeeper.World;
using Microsoft.Xna.Framework;
using MonoGame;

namespace KeyKeeper.Entities
{
    public abstract class Entity
    {
        private static int EntityCount = 0;

        protected GameLevel _currentLevel;
        public GameLevel CurrentLevel{ get { return _currentLevel; } set { _currentLevel = value; } }

        public int ID { get; private set; }

        private Point _location = new Point();
        private Point Location { get { return _location; } }

        public int X { get { return _location.X; } set { _location.X = value; } }
        public int Y { get { return _location.Y; } set { _location.Y = value; } }
        public int Depth => CurrentLevel.Depth;

        public string Name { get {return _name; } protected set { _name = value; } }

        protected string _name;
        public Sprite Sprite { get; }
        public Color ForegroundColor { get; }
        private HashSet<string> _flags = new HashSet<string>();

        public bool AddFlag(string flag) => _flags.Add(flag);
        public bool RemoveFlag(string flag) => _flags.Remove(flag);
        public bool HasFlag(string flag) => _flags.Contains(flag);

        public Entity(string name, Sprite sprite, Color foregroundColor)
        {
            _name = name;
            Sprite = sprite;
            ForegroundColor = foregroundColor;
        }

        public void Init(GameLevel currentLevel)
        {
            ID = EntityCount++;
            _currentLevel = currentLevel;
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
