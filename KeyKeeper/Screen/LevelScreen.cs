using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyKeeper.Content;
using KeyKeeper.Graphics;
using KeyKeeper.Helpers;
using KeyKeeper.Helpers.Game;
using KeyKeeper.Managers;
using KeyKeeper.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KeyKeeper.Screen
{
    public class LevelScreen : BaseScreen
    {
        public static bool HideFov = false;

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

                    if (!HideFov && !CurrentLevel.IsExplored(xp, yp))
                    {
                        continue;
                    }

                    bool inFov = HideFov || _gameManager.GameWorld.InFov(xp, yp, CurrentLevel.Depth);
                    DrawCell(batch, CurrentLevel.GetCell(xp, yp), xa + X, ya + Y, inFov);
                }
            }

            foreach (var creature in _gameManager.GameWorld.GetCreatures(_gameManager.Hero.Depth))
            {
                int x = creature.X - sx + X;
                int y = creature.Y - sy + Y;
                if (!HideFov && (!CurrentLevel.IsExplored(creature.X, creature.Y) || !_gameManager.GameWorld.InFov(creature.X, creature.Y, CurrentLevel.Depth)))
                {
                    continue;
                }
                if (InScreenBounds(x * Assets.TEXTURE_SCALE, y * Assets.TEXTURE_SCALE))
                {
                    Renderer.DrawSprite(batch,
                                        Assets.GetSprite("shadow"),
                                        x,
                                        y,
                                        (HasBorder ? -((Assets.DEFAULT_TEXTURE_WIDTH * Assets.TEXTURE_SCALE) / 2) : 0),
                                        (HasBorder ? -((Assets.DEFAULT_TEXTURE_HEIGHT * Assets.TEXTURE_SCALE) / 2) : 0) + 6,
                                        Color.Black * 0.5f,
                                        null);

                    DrawLevelSprite(batch, creature.Sprite, x, y, creature.ForegroundColor, null, true);
                }
            }

            foreach(GameEffect effect in _gameManager.Effects)
            {
                //if(effect.Sprite != null)
                //{
                //    //DrawLevelSprite(batch, effect.Sprite, effect.X, effect.Y, effect.Color, null, true);
                //}
                //effect.Draw(batch, sx , sy);
                if (effect.Sprite != null)
                {
                    int x = effect.X - sx + X;
                    int y = effect.Y - sy + Y;
                    DrawLevelSprite(batch, effect.Sprite, x, y, effect.Color, null, true);
                }
            }

            base.Draw(batch);
        }

        private void DrawCell(SpriteBatch batch, Cell cell, int xp, int yp, bool inFov)
        {
            if (cell != null)
            {
                DrawLevelSprite(batch, cell.Sprite, xp, yp, cell.ForegroundColor, KeyKeeper.BackgroundColor, inFov);
            }
        }

        private void DrawLevelSprite(SpriteBatch batch, Sprite sprite, int xp, int yp, Color color, Color? backgroundColor, bool inFov)
        {
            Renderer.DrawSprite(
                batch,
                sprite,
                xp,
                yp,
                HasBorder ? -((Assets.DEFAULT_TEXTURE_WIDTH * Assets.TEXTURE_SCALE) / 2) : 0,
                HasBorder ? -((Assets.DEFAULT_TEXTURE_HEIGHT * Assets.TEXTURE_SCALE) / 2) : 0,
                inFov ? color : ColorHelpers.Darken(color, KeyKeeper.BackgroundColor, 0.5f),
                backgroundColor
            );
        }
    }
}
