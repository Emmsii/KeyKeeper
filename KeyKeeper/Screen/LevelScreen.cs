using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyKeeper.Content;
using KeyKeeper.Extensions;
using KeyKeeper.Graphics;
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

        private GameLevel CurrentLevel => _gameManager.Hero.CurrentLevel;

        private readonly Font _font;

        public LevelScreen(int x, int y, int width, int height, int scale, bool hasBorder, GameManager gameManager) : base(x, y, width, height, scale, hasBorder)
        {
            _gameManager = gameManager;
            _font = Assets.GetFont("font");
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
                    DrawCell(CurrentLevel.GetCell(xp, yp), xa, ya, batch);
                }
            }

            foreach(var creature in CurrentLevel.Creatures)
            {
                batch.DrawSprite(creature.Sprite, creature.X - sx, creature.Y - sy, Scale, creature.ForegroundColor);
            }

            //batch.DrawFont("hello world", _font, 15, 2, Scale, (float) Math.Sin(t * 2) + 2, Color.Pink);
            //batch.DrawUiPanel(0, 0, 10, 10, Scale, "title", Color.White);
        }

        private void DrawCell(Cell cell, int xp, int yp, SpriteBatch batch)
        {
            if (cell == null) return;
            batch.DrawSprite(cell.Sprite, xp, yp, Scale, cell.ForegroundColor);
        }
    }
}
