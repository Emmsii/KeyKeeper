using KeyKeeper.Entities;
using KeyKeeper.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.World
{
    public sealed class Cell
    {

        public TileType TileType { get; set; }

        public string Name { get { return TileType.Name; } }
        public Sprite Sprite { get { return TileType.Sprite; } }
        public Color ForegroundColor { get { return TileType.ForegroundColor; } }
        public Color BackgroundColor { get { return TileType.BackgroundColor; } }

        public bool IsSolid { get { return TileType.IsSolid; } }
        public bool IsTransparent { get { return TileType.IsTransparent; } }

        public Item Item { get; set; }
        public Prop Prop { get; set; }

        public Cell(TileType tileType)
        {
            TileType = tileType;
        }
    }
}
