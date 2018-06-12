using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Helpers
{
    public static class ColorHelpers
    {
        public static uint RgbToBgr(uint rgb)
        {
            uint a = (rgb >> 24) & 0xff;
            uint r = (rgb >> 16) & 0xff;
            uint g = (rgb >> 8) & 0xff;
            uint b = rgb & 0xff;

            return (a << 24) | (b << 16) | (g << 8) | (r << 0);
        }

        public static Color FromRgb(uint rgb)
        {
            return new Color(RgbToBgr(rgb));
        }

        public static Color Darken(Color color, Color darkenColor, float amount)
        {
            return Color.Lerp(color, darkenColor, amount);
        }
    }
}
