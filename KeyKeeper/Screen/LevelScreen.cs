using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyKeeper.Content;
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

        private readonly int _widthInTiles;
        private readonly int _heightInTiles;

        private int ScrollX => Math.Max(0, Math.Min(_gameManager.Hero.X - (_widthInTiles) / 2, CurrentLevel.Width - (_widthInTiles)));
        private int ScrollY => Math.Max(0, Math.Min(_gameManager.Hero.Y - (_heightInTiles) / 2, CurrentLevel.Height - (_heightInTiles)));

        private GameLevel CurrentLevel => _gameManager.GameWorld.GetLevel(_gameManager.Hero.Depth);

        public LevelScreen(int x, int y, int width, int height, bool hasBorder, GameManager gameManager) : base(x, y, width, height, hasBorder)
        {
            _gameManager = gameManager;

            _widthInTiles = Width / Assets.TEXTURE_SCALE;
            _heightInTiles = Height / Assets.TEXTURE_SCALE;
        }

        public override void Draw(SpriteBatch batch)
        {
            int sx = ScrollX;
            int sy = ScrollY;

            for (int ya = 0; ya < _heightInTiles; ya++)
            {
                int yp = ya + sy;
                for (int xa = 0; xa < _widthInTiles; xa++)
                {
                    int xp = xa + sx;
                    DrawCell(batch, CurrentLevel.GetCell(xp, yp), xa + X, ya + Y);
                }
            }

            foreach (var creature in _gameManager.GameWorld.GetCreatures(_gameManager.Hero.Depth))
            {
                int x = creature.X - sx + X;
                int y = creature.Y - sy + Y;
                if (InScreenBounds(x * Assets.TEXTURE_SCALE, y * Assets.TEXTURE_SCALE))
                {
                    DrawLevelSprite(batch, creature.Sprite, x, y, creature.ForegroundColor);
                }
            }

            base.Draw(batch);
        }

        private void RendererDrawSprite(Sprite sprite, int v1, int v2, Color foregroundColor, Color black, SpriteBatch batch)
        {
            throw new NotImplementedException();
        }

        private void DrawCell(SpriteBatch batch, Cell cell, int xp, int yp)
        {
            if (cell != null)
            {
                DrawLevelSprite(batch, cell.Sprite, xp, yp, cell.ForegroundColor);
            }
        }

        private void DrawLevelSprite(SpriteBatch batch, Sprite sprite, int xp, int yp, Color color)
        {
            Renderer.DrawSprite(batch,
                sprite,
                xp,
                yp,
                HasBorder ? -((Assets.DEFAULT_TEXTURE_WIDTH * Assets.TEXTURE_SCALE) / 2) : 0,
                HasBorder ? -((Assets.DEFAULT_TEXTURE_HEIGHT * Assets.TEXTURE_SCALE) / 2) : 0,
                color,
                Color.Black);
        }
    }
}
