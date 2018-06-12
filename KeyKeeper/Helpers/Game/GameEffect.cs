using KeyKeeper.Graphics;
using KeyKeeper.World;
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
        public int X { get; protected set; }
        public int Y { get; protected set; }

        protected Sprite _sprite;

        public virtual Sprite Sprite { get { return _sprite; } }
        public Color Color { get; }

        public int Energy { get; private set; } = 0;
        public int Life { get; private set; }
        public int Speed { get; }

        public bool LockActors { get; }

        public GameEffect(int x, int y, Sprite sprite, Color color, int speed, int life, bool lockActors)
        {
            X = x;
            Y = y;
            _sprite = sprite;
            Color = color;
            Speed = speed;
            Life = life;
            LockActors = lockActors;
        }

        public virtual bool Update(GameLevel level)
        {
            Energy = Energy % Speed;
            return Life == -1 ? true : --Life >= 0;
        }

        public void Draw(SpriteBatch batch, int sx, int sy)
        {
            if (Sprite != null)
            {
                //batch.Draw(Sprite.Texture,
                //           new Rectangle((X - sx) * Sprite.Width * Sprite.Scale,
                //                         (Y - sy) * Sprite.Height * Sprite.Scale,
                //                         Sprite.Width * Sprite.Scale,
                //                         Sprite.Height * Sprite.Scale),
                //           Sprite.Bounds,
                //           Color);
            }
        }

        public bool Gain()
        {
            return ++Energy >= Speed;
        }

    }
}
