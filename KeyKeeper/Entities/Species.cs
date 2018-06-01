using KeyKeeper.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Entities
{
    public class Species
    {
        public string Name { get; private set; }
        public Sprite Sprite { get; private set; }
        public Color ForegroundColor { get; private set; }
        public int MaxHealth { get; private set; }
        public int BaseSpeed { get; private set; }
        public int Vision { get; private set; }

        public Species(string name = null, Sprite sprite = null, Color? foregroundColor = null, int? maxHealth = null, int? baseSpeed = null, int? vision = null, Species parent = null)
        {
            Name = name ?? parent?.Name ?? throw new ArgumentNullException(nameof(name), "Species must have a name.");
            Sprite = sprite ?? parent?.Sprite ?? throw new ArgumentNullException(nameof(sprite), "Species must have a sprite.");
            ForegroundColor = foregroundColor ?? parent?.ForegroundColor ?? throw new ArgumentNullException(nameof(foregroundColor), "Species must have a foregorund color.");
            MaxHealth = maxHealth ?? parent?.MaxHealth ?? throw new ArgumentNullException(nameof(maxHealth), "Species must have a max health.");
            BaseSpeed = baseSpeed ?? parent?.BaseSpeed ?? throw new ArgumentNullException(nameof(baseSpeed), "Species must have a base speed.");
            Vision = vision ?? parent?.Vision ?? throw new ArgumentNullException(nameof(vision), "Species must have a vision.");
        }
    }
}
