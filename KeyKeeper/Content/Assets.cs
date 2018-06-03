using KeyKeeper.Entities;
using KeyKeeper.Graphics;
using KeyKeeper.World;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;
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

        public const int DEFAULT_TEXTURE_WIDTH = 16;
        public const int DEFAULT_TEXTURE_HEIGHT = 16;
        public const int DEFAULT_FONT_WIDTH = 8;
        public const int DEFAULT_FONT_HEIGHT = 16;
        public const int TEXTURE_SCALE = 2;
        public const int UI_SPRITE_SCALE = 1;
        
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

        public static Font GetFont(string name)
        {
            Spritesheet fontSheet;
            try
            {
                fontSheet = GetSpritesheet(name);
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException($"Cannot get font '{name}'.");
            }

            Font font = (fontSheet as Font) ?? null;

            if(font == null)
            {
                throw new InvalidCastException($"'{name}' is not a font.");
            }

            return font;
        }

        public static Sprite GetSprite(string name)
        {
            if (!_sprites.TryGetValue(name, out Sprite sprite))
            {
                throw new KeyNotFoundException($"Cannot get sprite '{name}'.");
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
            AddSpritesheet("font", new Font(DEFAULT_CHARSET, 8, 16, content.Load<Texture2D>("font")));
        }

        private static void LoadSprites(ContentManager content)
        {
            Spritesheet tiles = GetSpritesheet("tiles");
            Spritesheet creatures = GetSpritesheet("creatures");
            Spritesheet ui = GetSpritesheet("ui");

            AddSprite("wall", tiles.CutSprite(0, 0, TEXTURE_SCALE, "wall"));
            AddSprite("floor", tiles.CutSprite(3, 0, TEXTURE_SCALE, "floor"));
            AddSprite("floor2", tiles.CutSprite(4, 0, TEXTURE_SCALE, "floor2"));
            AddSprite("floor3", tiles.CutSprite(5, 0, TEXTURE_SCALE, "floor3"));

            AddSprite("hero", creatures.CutSprite(0, 0, TEXTURE_SCALE, "hero"));
            AddSprite("troll", creatures.CutSprite(7, 5, TEXTURE_SCALE, "troll"));

            AddSprite("stairs_down", tiles.CutSprite(2, 1, TEXTURE_SCALE, "stairs_down"));
            AddSprite("stairs_up", tiles.CutSprite(3, 1, TEXTURE_SCALE, "stairs_up"));

            AddSprite("debug_box_large", tiles.CutSprite(3, 10, TEXTURE_SCALE, "debug_box_large"));
            AddSprite("debug_box_small", tiles.CutSprite(3, 10, UI_SPRITE_SCALE, "debug_box_small"));

            AddSprite("border_horizontal", ui.CutSprite(0, 0, UI_SPRITE_SCALE, "border_horizontal"));
            AddSprite("border_vertical", ui.CutSprite(1, 0, UI_SPRITE_SCALE, "border_vertical"));
            AddSprite("border_bottom_right", ui.CutSprite(2, 0, UI_SPRITE_SCALE, "border_bottom_right"));
            AddSprite("border_bottom_left", ui.CutSprite(3, 0, UI_SPRITE_SCALE, "border_bottom_left"));
            AddSprite("border_top_left", ui.CutSprite(4, 0, UI_SPRITE_SCALE, "border_top_left"));
            AddSprite("border_top_right", ui.CutSprite(5, 0, UI_SPRITE_SCALE, "border_top_right"));
            AddSprite("border_vertical_right", ui.CutSprite(6, 0, UI_SPRITE_SCALE, "border_vertical_right"));
            AddSprite("border_vertical_left", ui.CutSprite(8, 0, UI_SPRITE_SCALE, "border_vertical_left"));
            AddSprite("border_horizontal_top", ui.CutSprite(0, 1, UI_SPRITE_SCALE, "border_horizontal_top"));
            AddSprite("border_horizontal_bottom", ui.CutSprite(1, 1, UI_SPRITE_SCALE, "border_horizontal_bottom"));
            AddSprite("dot", ui.CutSprite(2, 1, UI_SPRITE_SCALE, "dot"));
            AddSprite("dither_fill", ui.CutSprite(3, 1, UI_SPRITE_SCALE, "dither_fill"));
        }

        private static void LoadTileTypes()
        {
            AddTileType("wall", new TileType("wall", GetSprite("wall"), Color.Beige, Color.Black, true, false));
            AddTileType("floor", new TileType("floor", GetSprite("floor"), Color.Beige, Color.Black, false, true));
            AddTileType("floor2", new TileType("floor2", GetSprite("floor2"), Color.Beige, Color.Black, false, true));
            AddTileType("floor3", new TileType("floor3", GetSprite("floor3"), Color.Beige, Color.Black, false, true));

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
