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
    public class Cell
    {

        public int X { get; }
        public int Y { get; }

        private readonly CellType _cellType;

        public string Name { get { return _cellType.Name; } }
        public Sprite Sprite { get { return _cellType.Sprite; } }
        public Color ForegroundColor { get { return _cellType.ForegroundColor; } }
        public Color BackgroundColor { get { return _cellType.BackgroundColor; } }

        public bool IsSolid { get { return _cellType.IsSolid; } }
        public bool IsTransparent { get { return _cellType.IsTransparent; } }

        public Item Item { get; set; }
        //public Prop Prop { get; set; }

        public Cell(int x, int y, CellType type)
        {
            X = x;
            Y = y;
            _cellType = type;
        }
    }
}
