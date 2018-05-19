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
        public string Name { get; }
        public Sprite Sprite { get; }
        public Color ForegroundColor { get; }
        public Color BackgroundColor { get; }

        public bool IsSolid { get; }
        public bool IsTransparent { get; }

        public Cell(Sprite sprite, string name, Color foregroundColor, Color backgroundColor, bool isSolid, bool isTransparent)
        {
            Sprite = sprite;
            Name = name;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
            IsSolid = isSolid;
            IsTransparent = isTransparent;
        }
    }
}
