﻿using KeyKeeper.Graphics;
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
        public int MaxHealth { get; private set; }
        public int BaseSpeed { get; private set; }
        public int Vision { get; private set; }

        public Species(string name, Sprite sprite, int maxHealth, int baseSpeed, int vision, Species parent = null)
        {
            Name = parent?.Name ?? name;
            Sprite = parent?.Sprite ?? sprite;
            MaxHealth = parent?.MaxHealth ?? maxHealth;
            BaseSpeed = parent?.BaseSpeed ?? baseSpeed;
            Vision = parent?.Vision ?? vision;
        }
    }
}