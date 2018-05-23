using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyKeeper.Graphics;
using Microsoft.Xna.Framework;

namespace KeyKeeper.Entities
{
    public class Prop : Entity
    {
        public Prop(string name, Sprite sprite, Color foregroundColor) : base(name, sprite, foregroundColor)
        {
        }
    }
}
