using KeyKeeper.Entities;
using KeyKeeper.Graphics;
using KeyKeeper.World;
using Content = Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using System.IO;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace KeyKeeper.Content
{
    internal static class Assets
    {

        private static Dictionary<string, Spritesheet> _spritesheets = new Dictionary<string, Spritesheet>();
        private static Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();

        private static Dictionary<string, CellType> _cellTypes = new Dictionary<string, CellType>();
        private static Dictionary<string, Species> _species = new Dictionary<string, Species>();

        public static Spritesheet GetSpritesheet(string name) => _spritesheets[name];
        public static Sprite GetSprite(string name) => _sprites[name];
        public static CellType GetCellType(string name) => _cellTypes[name];
        public static Species GetSpecies(string name) => _species[name];

        private static void AddSpritesheet(string name, Spritesheet spritesheet) => _spritesheets.Add(name, spritesheet);
        private static void AddSprite(string name, Sprite sprite) => _sprites.Add(name, sprite);
        private static void AddCellType(string name, CellType type) => _cellTypes.Add(name, type);
        private static void AddSpecies(string name, Species species) => _species.Add(name, species);

        private static int TotalAssetCount => _spritesheets.Count + _sprites.Count + _cellTypes.Count + _species.Count;

        public static void LoadAssets(ContentManager content)
        {
            Console.WriteLine("Loading assets...");
            var watch = Stopwatch.StartNew();

            // TODO: Load all data from data files.

            LoadSpritesheets(content);
            LoadSprites(content);
            LoadCellTypes();
            LoadItems();
            LoadSpecies();

            watch.Stop();
            Console.WriteLine($"Loaded {TotalAssetCount} assets in {watch.Elapsed.TotalMilliseconds}ms.");
        }

        private static void LoadSpritesheets(ContentManager content)
        {
            AddSpritesheet("tiles", new Spritesheet(16, 16, content.Load<Texture2D>("tiles")));
            AddSpritesheet("creatures", new Spritesheet(16, 16, content.Load<Texture2D>("creatures")));
            AddSpritesheet("items", new Spritesheet(16, 16, content.Load<Texture2D>("items")));
        }

        private static void LoadSprites(ContentManager content)
        {
            Spritesheet tiles = GetSpritesheet("tiles");
            Spritesheet creatures = GetSpritesheet("creatures");

            AddSprite("wall", tiles.CutSprite(0, 0, "wall"));
            AddSprite("floor", tiles.CutSprite(3, 0, "floor"));

            AddSprite("hero", creatures.CutSprite(0, 0, "hero"));
            AddSprite("troll", creatures.CutSprite(7, 5, "troll"));
        }

        private static void LoadCellTypes()
        {
            AddCellType("wall", new CellType("wall", GetSprite("wall"), Color.Beige, Color.Black, true, false));
            AddCellType("floor", new CellType("floor", GetSprite("floor"), Color.Beige, Color.Black, false, true));
        }

        private static void LoadItems()
        {

        }

        private static void LoadSpecies()
        {
            AddSpecies("hero", new Species("hero", GetSprite("hero"), Color.Red, 10, 0, 10));
            AddSpecies("troll", new Species("troll", GetSprite("troll"), Color.Cyan, 10, -1, 10));
        }

    }
}
