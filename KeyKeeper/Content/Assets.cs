﻿using KeyKeeper.Entities;
using KeyKeeper.Graphics;
using KeyKeeper.World;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using KeyKeeper.Helpers;

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

        public static readonly Color VeryDarkViolet = ColorHelpers.FromRgb(0xff17111D);
        public static readonly Color ShadowyLavender = ColorHelpers.FromRgb(0xff4e4a4e);
        public static readonly Color Flint = ColorHelpers.FromRgb(0xff716E61);
        public static readonly Color RegentGrey = ColorHelpers.FromRgb(0xff86949F);
        public static readonly Color Peppermint = ColorHelpers.FromRgb(0xffD7E7D0);

        public static readonly Color RedEarth = ColorHelpers.FromRgb(0xff462428);
        public static readonly Color RootBeer = ColorHelpers.FromRgb(0xff814D30);
        public static readonly Color FadedRed = ColorHelpers.FromRgb(0xffD3494E);
        public static readonly Color Bronze = ColorHelpers.FromRgb(0xffCD7F32);
        public static readonly Color BirthdaySuit = ColorHelpers.FromRgb(0xffD4A798);
        public static readonly Color Banana = ColorHelpers.FromRgb(0xffE3CF57);

        public static readonly Color DeepKoamaru = ColorHelpers.FromRgb(0xff333366);
        public static readonly Color Indigo = ColorHelpers.FromRgb(0xff5D76CB);
        public static readonly Color CadetBlue = ColorHelpers.FromRgb(0xff7AC5CD);

        public static readonly Color HunterGreen = ColorHelpers.FromRgb(0xff215E21);
        public static readonly Color Leaf = ColorHelpers.FromRgb(0xff71AA34);

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
            AddSpritesheet("effects", new Spritesheet(16, 16, content.Load<Texture2D>("effects")));
            AddSpritesheet("font", new Font(DEFAULT_CHARSET, 8, 16, content.Load<Texture2D>("font")));
        }

        private static void LoadSprites(ContentManager content)
        {
            Spritesheet tiles = GetSpritesheet("tiles");
            Spritesheet creatures = GetSpritesheet("creatures");
            Spritesheet ui = GetSpritesheet("ui");
            Spritesheet effects = GetSpritesheet("effects");

            AddSprite("bricks_1", tiles.CutSprite(0, 0, TEXTURE_SCALE, "bricks_1"));
            AddSprite("bricks_2", tiles.CutSprite(1, 0, TEXTURE_SCALE, "bricks_2"));
            AddSprite("bricks_3", tiles.CutSprite(2, 0, TEXTURE_SCALE, "bricks_3"));
            AddSprite("rocks_1", tiles.CutSprite(3, 0, TEXTURE_SCALE, "rocks_1"));
            AddSprite("rocks_2", tiles.CutSprite(4, 0, TEXTURE_SCALE, "rocks_2"));
            AddSprite("rocks_3", tiles.CutSprite(5, 0, TEXTURE_SCALE, "rocks_3"));

            AddSprite("floor_1", tiles.CutSprite(5, 8, TEXTURE_SCALE, "floor_1"));
            AddSprite("floor_2", tiles.CutSprite(6, 8, TEXTURE_SCALE, "floor_2"));
            AddSprite("floor_3", tiles.CutSprite(7, 8, TEXTURE_SCALE, "floor_3"));
            
            AddSprite("wall_hor", tiles.CutSprite(7, 2, TEXTURE_SCALE, "wall_hor"));
            AddSprite("wall_ver", tiles.CutSprite(0, 3, TEXTURE_SCALE, "wall_ver"));
            AddSprite("wall_ver_end", tiles.CutSprite(1, 3, TEXTURE_SCALE, "wall_ver_end"));
            AddSprite("wall_top_rig", tiles.CutSprite(2, 3, TEXTURE_SCALE, "wall_top_rig"));
            AddSprite("wall_top_lef", tiles.CutSprite(3, 3, TEXTURE_SCALE, "wall_top_lef"));
            AddSprite("wall_bot_rig", tiles.CutSprite(4, 3, TEXTURE_SCALE, "wall_bot_rig"));
            AddSprite("wall_bot_lef", tiles.CutSprite(5, 3, TEXTURE_SCALE, "wall_bot_lef"));
            AddSprite("wall_ver_rig", tiles.CutSprite(6, 3, TEXTURE_SCALE, "wall_ver_rig"));
            AddSprite("wall_ver_lef", tiles.CutSprite(7, 3, TEXTURE_SCALE, "wall_ver_lef"));
            AddSprite("wall_hor_bot", tiles.CutSprite(0, 4, TEXTURE_SCALE, "wall_hor_bot"));
            AddSprite("wall_hor_top", tiles.CutSprite(1, 4, TEXTURE_SCALE, "wall_hor_top"));
            AddSprite("wall_all", tiles.CutSprite(2, 4, TEXTURE_SCALE, "wall_all"));

            AddSprite("door_closed", tiles.CutSprite(4, 1, TEXTURE_SCALE, "door_closed"));
            AddSprite("door_open", tiles.CutSprite(5, 1, TEXTURE_SCALE, "door_open"));

            AddSprite("floor_dot", ui.CutSprite(2, 1, TEXTURE_SCALE, "floor_dot"));

            AddSprite("shadow", creatures.CutSprite(2, 8, TEXTURE_SCALE, "shadow"));

            AddSprite("hero", creatures.CutSprite(1, 0, TEXTURE_SCALE, "hero"));
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
            AddSprite("fill", ui.CutSprite(4, 1, UI_SPRITE_SCALE, "fill"));
            AddSprite("button_left", ui.CutSprite(5, 1, UI_SPRITE_SCALE, "button_left"));
            AddSprite("button_center", ui.CutSprite(6, 1, UI_SPRITE_SCALE, "button_center"));
            AddSprite("button_right", ui.CutSprite(7, 1, UI_SPRITE_SCALE, "button_right"));

            AddSprite("arrow_ne", effects.CutSprite(0, 0, TEXTURE_SCALE, "arrow_ne"));
            AddSprite("arrow_e", effects.CutSprite(1, 0, TEXTURE_SCALE, "arrow_e"));
            AddSprite("arrow_se", effects.CutSprite(2, 0, TEXTURE_SCALE, "arrow_se"));
            AddSprite("arrow_s", effects.CutSprite(3, 0, TEXTURE_SCALE, "arrow_s"));
            AddSprite("arrow_sw", effects.CutSprite(0, 1, TEXTURE_SCALE, "arrow_sw"));
            AddSprite("arrow_w", effects.CutSprite(1, 1, TEXTURE_SCALE, "arrow_w"));
            AddSprite("arrow_nw", effects.CutSprite(2, 1, TEXTURE_SCALE, "arrow_nw"));
            AddSprite("arrow_n", effects.CutSprite(3, 1, TEXTURE_SCALE, "arrow_n"));
        }

        private static void LoadTileTypes()
        {

            AddTileType("bricks_1", new TileType("bricks_1", GetSprite("bricks_1"), Flint, Color.Black, false, true));
            AddTileType("bricks_2", new TileType("bricks_2", GetSprite("bricks_2"), Flint, Color.Black, false, true));
            AddTileType("bricks_3", new TileType("bricks_3", GetSprite("bricks_3"), Flint, Color.Black, false, true));
            AddTileType("rocks_1", new TileType("rocks_1", GetSprite("rocks_1"), ShadowyLavender, Color.Black, true, false));
            AddTileType("rocks_2", new TileType("rocks_2", GetSprite("rocks_2"), ShadowyLavender, Color.Black, true, false));
            AddTileType("rocks_3", new TileType("rocks_3", GetSprite("rocks_3"), ShadowyLavender, Color.Black, true, false));

            AddTileType("floor_1", new TileType("floor_2", GetSprite("floor_2"), Color.White, Color.Black, false, false));
            AddTileType("floor_2", new TileType("floor_3", GetSprite("floor_3"), Color.White, Color.Black, false, false));
            AddTileType("floor_3", new TileType("floor_3", GetSprite("floor_3"), Color.White, Color.Black, false, false));

            AddTileType("wall_hor", new TileType("wall_hor", GetSprite("wall_hor"), ShadowyLavender, Color.Black, true, false));
            AddTileType("wall_ver", new TileType("wall_ver", GetSprite("wall_ver"), ShadowyLavender, Color.Black, true, false));
            AddTileType("wall_ver_end", new TileType("wall_ver_end", GetSprite("wall_ver_end"), ShadowyLavender, Color.Black, true, false));
            AddTileType("wall_top_rig", new TileType("wall_top_rig", GetSprite("wall_top_rig"), ShadowyLavender, Color.Black, true, false));
            AddTileType("wall_top_lef", new TileType("wall_top_lef", GetSprite("wall_top_lef"), ShadowyLavender, Color.Black, true, false));
            AddTileType("wall_bot_rig", new TileType("wall_bot_rig", GetSprite("wall_bot_rig"), ShadowyLavender, Color.Black, true, false));
            AddTileType("wall_bot_lef", new TileType("wall_bot_lef", GetSprite("wall_bot_lef"), ShadowyLavender, Color.Black, true, false));
            AddTileType("wall_ver_rig", new TileType("wall_ver_rig", GetSprite("wall_ver_rig"), ShadowyLavender, Color.Black, true, false));
            AddTileType("wall_ver_lef", new TileType("wall_ver_lef", GetSprite("wall_ver_lef"), ShadowyLavender, Color.Black, true, false));
            AddTileType("wall_hor_bot", new TileType("wall_hor_bot", GetSprite("wall_hor_bot"), ShadowyLavender, Color.Black, true, false));
            AddTileType("wall_hor_top", new TileType("wall_hor_top", GetSprite("wall_hor_top"), ShadowyLavender, Color.Black, true, false));
            AddTileType("wall_all", new TileType("wall_all", GetSprite("wall_all"), ShadowyLavender, Color.Black, true, false));

            AddTileType("door_closed", new TileType("door_closed", GetSprite("door_closed"), RootBeer, Color.Black, false, false));
            AddTileType("door_open", new TileType("door_open", GetSprite("door_open"), Bronze, Color.Black, false, true));

            AddTileType("floor_dot", new TileType("floor_dot", GetSprite("floor_dot"), Flint, Color.Black, false, true));
            
            AddTileType("stairs_down", new TileType("stairs_down", GetSprite("stairs_down"), Color.Beige, Color.Black, false, true));
            AddTileType("stairs_up", new TileType("stairs_up", GetSprite("stairs_up"), Color.Beige, Color.Black, false, true));
        }

        private static void LoadItems()
        {

        }

        private static void LoadSpecies()
        {
            AddSpecies("hero", new Species("hero", GetSprite("hero"), Peppermint, 10, 0, 10));
            AddSpecies("troll", new Species("troll", GetSprite("troll"), RedEarth, 10, -1, 10));
        }

    }
}
