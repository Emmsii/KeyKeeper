using KeyKeeper.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.World
{
    public class CellType
    {
        public string Name{ get; }
        public Sprite Sprite { get; }
        public Color ForegroundColor{ get; }
        public Color BackgroundColor { get; }

        public bool IsSolid { get; }
        public bool IsTransparent { get; }

        public CellType(string name, Sprite sprite, Color foregroundColor, Color backgroundColor, bool isSolid, bool isTransparent)
        {
            Name = name;
            Sprite = sprite;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
            IsSolid = isSolid;
            IsTransparent = isTransparent;
        }

        public Cell NewCellFromType()
        {
            return new Cell(this);
        }
    }
}
