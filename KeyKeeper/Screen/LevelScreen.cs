using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyKeeper.Managers;
using KeyKeeper.World;
using Microsoft.Xna.Framework.Graphics;

namespace KeyKeeper.Screen
{
    public class LevelScreen : Screen
    {
        private readonly GameManager _gameManager;

        private int ScrollX => Math.Max(0, Math.Min(_gameManager.Hero.X - (Width) / 2, _gameManager.GameWorld.LevelWidth(_gameManager.Hero.Depth) - Width));
        private int ScrollY => Math.Max(0, Math.Min(_gameManager.Hero.Y - (Height) / 2, _gameManager.GameWorld.LevelWidth(_gameManager.Hero.Depth) - Height));
        
        public LevelScreen(int x, int y, int width, int height, int scale, GameManager gameManager) : base(x, y, width, height, scale)
        {
            _gameManager = gameManager;
        }

        public override void Draw(SpriteBatch batch)
        {
            GameLevel level = _gameManager.GameWorld.GetLevel(_gameManager.Hero.Depth);

            int sx = ScrollX;
            int sy = ScrollY;

            for (int y = 0; y < level.Height; y++)
            {
                int yp = y + sy;
                for(int x = 0; x < level.Width; x++)
                {
                    int xp = x + sx;
                    DrawCell(level.GetCell(xp, yp), x, y, batch);
                }
            }
        }

        private void DrawCell(Cell cell, int xp, int yp, SpriteBatch batch)
        {
            DrawSprite(cell.Sprite, xp, yp, cell.ForegroundColor, cell.BackgroundColor, batch);
        }
    }
}
