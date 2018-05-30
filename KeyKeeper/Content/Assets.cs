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

        private const string DEFAULT_CHARSET = "0123456789ABCDEF" +
                                               "GHIJKLMNOPQRSTUV" +
                                               "WXYZ????????????" +
                                               "?????abcdefghijk" +
                                               "lmnopqrstuvwxyz?" +
                                               "????????????????" +
                                               "#%&@$.,!?:;'\"()[" +
                                               "]*/\\+-<=>???????" +
                                               "???⚪◔◑◕⚫█▓▒░??? ";

        private static Dictionary<string, Spritesheet> _spritesheets = new Dictionary<string, Spritesheet>();
        private static Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();

        private static Dictionary<string, CellType> _cellTypes = new Dictionary<string, CellType>();
        private static Dictionary<string, Species> _species = new Dictionary<string, Species>();

        public static Spritesheet GetSpritesheet(string name) => _spritesheets[name];
        public static Sprite GetSprite(string name) => _sprites[name];

        public static Font GetFont(string name)
        {
            if(!_spritesheets.TryGetValue(name, out Spritesheet sheet))
            {
                throw new ArgumentException($"Font: {name} does not exist.");
            }

            Font font = sheet as Font;
            if(font == null)
            {
                throw new InvalidCastException($"{name} is not a font.");
            }

            return font;
        }

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
            AddSpritesheet("font", new Font(DEFAULT_CHARSET, 8, 16, content.Load<Texture2D>("font")));
            AddSpritesheet("ui", new Spritesheet(16, 16, content.Load<Texture2D>("ui")));
        }

        private static void LoadSprites(ContentManager content)
        {
            Spritesheet tiles = GetSpritesheet("tiles");
            Spritesheet creatures = GetSpritesheet("creatures");
            Spritesheet ui = GetSpritesheet("ui");

            // Tile Sprites
            AddSprite("wall", tiles.CutSprite(0, 0, "wall"));
            AddSprite("floor", tiles.CutSprite(3, 0, "floor"));
            AddSprite("stairs_down", tiles.CutSprite(2, 1, "stairs_down"));
            AddSprite("stairs_up", tiles.CutSprite(3, 1, "stairs_up"));

            // Creature Sprites
            AddSprite("hero", creatures.CutSprite(0, 0, "hero"));
            AddSprite("troll", creatures.CutSprite(7, 5, "troll"));

            // UI Sprites
            AddSprite("border_horizontal", ui.CutSprite(0, 0, "border_horizontal"));
            AddSprite("border_vertical", ui.CutSprite(1, 0, "border_vertical"));
            AddSprite("border_bottom_right", ui.CutSprite(2, 0, "border_bottom_right"));
            AddSprite("border_bottom_left", ui.CutSprite(3, 0, "border_bottom_left"));
            AddSprite("border_top_left", ui.CutSprite(4, 0, "border_top_left"));
            AddSprite("border_top_right", ui.CutSprite(5, 0, "border_top_right"));
            AddSprite("border_vertical_right", ui.CutSprite(6, 0, "border_vertical_right"));
            AddSprite("border_vertical_left", ui.CutSprite(8, 0, "border_vertical_left"));
            AddSprite("border_horizontal_top", ui.CutSprite(0, 1, "border_horizontal_top"));
            AddSprite("border_horizontal_bottom", ui.CutSprite(1, 1, "border_horizontal_bottom"));
            AddSprite("dot", ui.CutSprite(2, 1, "dot"));
            AddSprite("dither_fill", ui.CutSprite(3, 1, "dither_fill"));
        }

        private static void LoadCellTypes()
        {
            AddCellType("wall", new CellType("wall", GetSprite("wall"), Color.Beige, Color.Black, true, false));
            AddCellType("floor", new CellType("floor", GetSprite("floor"), Color.Beige, Color.Black, false, true));
            AddCellType("stairs_down", new CellType("stairs_down", GetSprite("stairs_down"), Color.Beige, Color.Black, false, true));
            AddCellType("stairs_up", new CellType("stairs_up", GetSprite("stairs_down"), Color.Beige, Color.Black, false, true));
        }

        private static void LoadItems()
        {

        }

        private static void LoadProps()
        {

        }

        private static void LoadSpecies()
        {
            AddSpecies("hero", new Species("hero", GetSprite("hero"), Color.Red, 10, 0, 10));
            AddSpecies("troll", new Species("troll", GetSprite("troll"), Color.Cyan, 10, -1, 10));
        }

    }
}
