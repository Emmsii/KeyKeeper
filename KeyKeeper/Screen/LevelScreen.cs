using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyKeeper.Managers;
using KeyKeeper.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KeyKeeper.Screen
{
    public class LevelScreen : Screen
    {
        private readonly GameManager _gameManager;

        private int ScrollX => Math.Max(0, Math.Min(_gameManager.Hero.X - (Width) / 2, CurrentLevel.Width - (Width)));
        private int ScrollY => Math.Max(0, Math.Min(_gameManager.Hero.Y - (Height) / 2, CurrentLevel.Height - (Height)));

        private GameLevel CurrentLevel => _gameManager.GameWorld.GetLevel(_gameManager.Hero.Depth);

        public LevelScreen(int x, int y, int width, int height, int scale, GameManager gameManager) : base(x, y, width, height, scale)
        {
            _gameManager = gameManager;
        }

        public override void Draw(SpriteBatch batch)
        {
            GameLevel level = _gameManager.GameWorld.GetLevel(_gameManager.Hero.Depth);

            int sx = ScrollX;
            int sy = ScrollY;

            for (int ya = 0; ya < Height; ya++)
            {
                int yp = ya + sy;
                for(int xa = 0; xa < Width; xa++)
                {
                    int xp = xa + sx;
                    DrawCell(level.GetCell(xp, yp), xa, ya, batch);
                }
            }

            foreach(var creature in _gameManager.GameWorld.GetCreatures(_gameManager.Hero.Depth))
            {
                DrawSprite(creature.Sprite, creature.X - sx, creature.Y - sy, creature.ForegroundColor, Color.Black, batch);
            }
        }

        private void DrawCell(Cell cell, int xp, int yp, SpriteBatch batch)
        {
            if (cell == null) return;
            DrawSprite(cell.Sprite, xp, yp, cell.ForegroundColor, cell.BackgroundColor, batch);
        }
    }
}
