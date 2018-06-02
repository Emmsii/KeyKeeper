using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyKeeper.Graphics;
using KeyKeeper.Managers;
using KeyKeeper.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KeyKeeper.Screen
{
    public class LevelScreen : BaseScreen
    {
        private readonly GameManager _gameManager;

        private int ScrollX => Math.Max(0, Math.Min(_gameManager.Hero.X - (Width) / 2, CurrentLevel.Width - (Width)));
        private int ScrollY => Math.Max(0, Math.Min(_gameManager.Hero.Y - (Height) / 2, CurrentLevel.Height - (Height)));

        private GameLevel CurrentLevel => _gameManager.GameWorld.GetLevel(_gameManager.Hero.Depth);

        public LevelScreen(int x, int y, int width, int height, int scale, bool hasBorder, GameManager gameManager) : base(x, y, width, height, scale, hasBorder)
        {
            _gameManager = gameManager;
        }

        public override void Draw(SpriteBatch batch)
        {
            //GameLevel level = _gameManager.GameWorld.GetLevel(_gameManager.Hero.Depth);

            int sx = ScrollX;
            int sy = ScrollY;

            for (int ya = 0; ya < Height; ya++)
            {
                int yp = ya + sy;
                for(int xa = 0; xa < Width; xa++)
                {
                    int xp = xa + sx;
                    DrawCell(CurrentLevel.GetCell(xp, yp), xa + X, ya + Y, batch);
                }
            }

            foreach(var creature in _gameManager.GameWorld.GetCreatures(_gameManager.Hero.Depth))
            {
                Renderer.DrawSprite(batch, creature.Sprite, creature.X - sx + X, creature.Y - sy + Y, Scale, creature.ForegroundColor);
            }

            base.Draw(batch);
        }

        private void RendererDrawSprite(Sprite sprite, int v1, int v2, Color foregroundColor, Color black, SpriteBatch batch)
        {
            throw new NotImplementedException();
        }

        private void DrawCell(Cell cell, int xp, int yp, SpriteBatch batch)
        {
            if (cell == null) return;
            Renderer.DrawSprite(batch, cell.Sprite, xp, yp, Scale, cell.ForegroundColor);
        }
    }
}
