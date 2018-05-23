using KeyKeeper.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Helpers.Game
{
    public class GameEffect
    {
        public int X { get; }
        public int Y { get; }

        public Sprite Sprite { get; }
        public Color Color { get; }

        public int Energy { get; private set; } = 0;
        public int Life { get; private set; }
        public int Speed { get; }

        public bool LockActors { get; }

        public GameEffect(int x, int y, Sprite sprite, Color color, int speed, int life, bool lockActors)
        {
            X = x;
            Y = y;
            Sprite = sprite;
            Color = color;
            Speed = speed;
            Life = life;
            LockActors = lockActors;
        }

        public bool Update()
        {
            Energy = Energy % Speed;
            return Life == -1 ? true : --Life >= 0;
        }

        public void Draw(SpriteBatch batch, int sx, int sy, int scale)
        {
            batch.Draw(Sprite.Texture,
                       new Rectangle((X - sx) * Sprite.Width * scale, 
                                     (Y - sy) * Sprite.Height * scale,
                                     Sprite.Width * scale,
                                     Sprite.Height * scale),
                       Sprite.Bounds,
                       Color);
        }

    }
}
