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

        private static Dictionary<string, TileType> _tileTypes = new Dictionary<string, TileType>();
        private static Dictionary<string, Species> _species = new Dictionary<string, Species>();

        private static void AddSpritesheet(string name, Spritesheet spritesheet) => _spritesheets.Add(name, spritesheet);
        private static void AddSprite(string name, Sprite sprite) => _sprites.Add(name, sprite);
        private static void AddTileType(string name, TileType type) => _tileTypes.Add(name, type);
        private static void AddSpecies(string name, Species species) => _species.Add(name, species);

        private static int TotalAssetCount => _spritesheets.Count + _sprites.Count + _tileTypes.Count + _species.Count;
        
        public static Spritesheet GetSpritesheet(string name)
        {
            if (!_spritesheets.TryGetValue(name, out Spritesheet spritesheet))
            {
                throw new KeyNotFoundException($"Cannot get spritesheet '{name}'.");
            }
            return spritesheet;
        }

        public static Sprite GetSprite(string name)
        {
            if (!_sprites.TryGetValue(name, out Sprite sprite))
            {
                throw new KeyNotFoundException($"Cannot get sprite'{name}'.");
            }
            return sprite;
        }

        public static TileType GetTileType(string name)
        {
            if (!_tileTypes.TryGetValue(name, out TileType tileType))
            {
                throw new KeyNotFoundException($"Cannot get tile type '{name}'.");
            }
            return tileType;
        }

        public static Species GetSpecies(string name)
        {
            if (!_species.TryGetValue(name, out Species species))
            {
                throw new KeyNotFoundException($"Cannot get species '{name}'.");
            }
            return species;
        }

        public static void LoadAssets(ContentManager content)
        {
            Console.WriteLine("Loading assets...");
            var watch = Stopwatch.StartNew();

            // TODO: Load all data from data files.

            LoadSpritesheets(content);
            LoadSprites(content);
            LoadTileTypes();
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
            AddSpritesheet("ui", new Spritesheet(16, 16, content.Load<Texture2D>("ui")));
        }

        private static void LoadSprites(ContentManager content)
        {
            Spritesheet tiles = GetSpritesheet("tiles");
            Spritesheet creatures = GetSpritesheet("creatures");
            Spritesheet ui = GetSpritesheet("ui");

            AddSprite("wall", tiles.CutSprite(0, 0, "wall"));
            AddSprite("floor", tiles.CutSprite(3, 0, "floor"));

            AddSprite("hero", creatures.CutSprite(0, 0, "hero"));
            AddSprite("troll", creatures.CutSprite(7, 5, "troll"));

            AddSprite("stairs_down", tiles.CutSprite(2, 1, "stairs_down"));
            AddSprite("stairs_up", tiles.CutSprite(3, 1, "stairs_up"));
        }

        private static void LoadTileTypes()
        {
            AddTileType("wall", new TileType("wall", GetSprite("wall"), Color.Beige, Color.Black, true, false));
            AddTileType("floor", new TileType("floor", GetSprite("floor"), Color.Beige, Color.Black, false, true));
            AddTileType("stairs_down", new TileType("stairs_down", GetSprite("stairs_down"), Color.Beige, Color.Black, false, true));
            AddTileType("stairs_up", new TileType("stairs_up", GetSprite("stairs_up"), Color.Beige, Color.Black, false, true));
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
