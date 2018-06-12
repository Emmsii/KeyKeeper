using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyKeeper.Content;
using KeyKeeper.Graphics;
using KeyKeeper.World;
using Microsoft.Xna.Framework;

namespace KeyKeeper.Helpers.Game
{
    class FireEffect : GameEffect
    {
        private readonly float _moveSpeed;

        private float _xf;
        private float _yf;
        private int _tx;
        private int _ty;

        private Sprite[] _projectileSprites;

        public FireEffect(int x, int y, int tx, int ty, Color color, float moveSpeed, int speed) : base(x, y, null, color, speed, -1, true)
        {
            _xf = x;
            _yf = y;
            _tx = tx;
            _ty = ty;
            _moveSpeed = moveSpeed;

            _projectileSprites = new Sprite[]
            {
                Assets.GetSprite("arrow_ne"),
                Assets.GetSprite("arrow_e"),
                Assets.GetSprite("arrow_se"),
                Assets.GetSprite("arrow_s"),
                Assets.GetSprite("arrow_sw"),
                Assets.GetSprite("arrow_e"),
                Assets.GetSprite("arrow_nw"),
                Assets.GetSprite("arrow_n")
            };
        }

        public override bool Update(GameLevel level)
        {
            float dx = _tx - _xf;
            float dy = _ty - _yf;
            float l = (float)Math.Sqrt(dx * dx + dy * dy);
            dx /= l;
            dy /= l;

            _xf += dx * _moveSpeed;
            _yf += dy * _moveSpeed;

            X = (int)_xf;
            Y = (int)_yf;

            _sprite = GetSprite(X, Y, _tx, _ty);

            if (level.IsSolid(X, Y))
            {
                return false;
            }

            return base.Update(level);
        }

        private Sprite GetSprite(int ox, int oy, float tx, float ty)
        {
            float angle = MathHelper.ToDegrees((float) Math.Atan2(ty - oy, tx - ox));
            int split = 360 / _projectileSprites.Length;
            angle -= 315 - (split / 2);
            if (angle < 0) angle += 360;
            int index = (int)(angle / split);
            if (index >= _projectileSprites.Length) index = _projectileSprites.Length - 1;
            return _projectileSprites[index];
        }
    }
}
