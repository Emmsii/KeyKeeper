using KeyKeeper.Graphics;
using KeyKeeper.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Screen
{
    public abstract class Screen
    {
        private readonly bool _hasBorder;

        protected int X { get; }
        protected int Y { get; }
        protected int Width { get; }
        protected int Height { get; }
        protected int Scale { get; }

        /*
         * 
         * TODO:
         * 
         * List/Stack of screens
         * 
         * Render top to bottom, only render if tile is not in bounds of other screen
         * Either set bool array to true = already rendered, reset each frame, or Rectangle intersects bounds check every other screen
         * bool HasBorder
         * If has border, xy + 1 widthheight - 2
         * draw border @ xy - 1 widthheight + 2
         * border needs fill with Sprite/Color
         * Screen needs Input/UpdateMethod
         * Input method needs key
         * Screen needs to know ORDER of screen stack
         * bool CanUpdateWhenNoFocus
         * bool HasFocus
         * mouse input = draggable
         * bool HadControlButtons = Close button
         * screen title
         * 
         * UI controller has stack of screens
         * Screen has list of sub-screens
         * 
         */

        protected List<Screen> _screens = new List<Screen>();

        public Screen(int x, int y, int width, int height, int scale, bool hasBorder)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Scale = scale;
            _hasBorder = hasBorder;

            if (_hasBorder)
            {
                X++;
                Y++;
                Width -= 2;
                Height -= 2;
            }
        }
        
        public virtual void Draw(SpriteBatch batch)
        {
            foreach(Screen screen in _screens)
            {
                screen.Draw(batch);
            }
        }

        public void Input(Keys key)
        {

        }

        public void MouseInput(MouseInputHandler mouseInput)
        {
            
        }

        //protected void DrawSprite(Sprite sprite, int xp, int yp, Color foregroundColor, Color backgroundColor, SpriteBatch batch)
        //{
        //    Rectangle destination = new Rectangle(xp * sprite.Width * Scale,
        //                                          yp * sprite.Height * Scale,
        //                                          sprite.Width * Scale,
        //                                          sprite.Height * Scale);

        //    // TODO: Draw background color

        //    batch.Draw(sprite.Texture, destination, sprite.Bounds, foregroundColor);
        //}

        public void AddScreen(Screen screen)
        {
            _screens.Add(screen);
        }

        public void PopScreen(Screen screen)
        {
            _screens.Remove(screen);
        }
        
    }
}
