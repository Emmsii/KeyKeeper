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

namespace KeyKeeper.Content
{
    public static class Assets
    {

        private static Dictionary<string, Spritesheet> _spritesheets = new Dictionary<string, Spritesheet>();
        private static Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();

        private static Dictionary<string, CellType> _cellTypes = new Dictionary<string, CellType>();
        private static Dictionary<string, Species> _species = new Dictionary<string, Species>();

        private static void AddSpritesheet(string name, Spritesheet spritesheet) => _spritesheets.Add(name, spritesheet);
        private static void AddSprite(string name, Sprite sprite) => _sprites.Add(name, sprite);
        private static void AddCellType(string name, CellType type) => _cellTypes.Add(name, type);

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

        }

        private static void LoadCellTypes()
        {

        }

        private static void LoadItems()
        {

        }

        private static void LoadSpecies()
        {

        }

    }
}
