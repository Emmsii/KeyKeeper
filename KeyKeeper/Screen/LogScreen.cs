using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyKeeper.Content;
using KeyKeeper.Graphics;
using KeyKeeper.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KeyKeeper.Screen
{
    public class LogScreen : BaseScreen
    {

        private readonly MessageLogManager _messageLog;
        private readonly Font _font;

        public LogScreen(int x, int y, int width, int height, bool hasBorder, MessageLogManager messageLog) : base(x, y, width, height, hasBorder)
        {
            _messageLog = messageLog;
            _font = Assets.GetFont("font");
        }

        public override void Draw(SpriteBatch batch)
        {
            int line = 0;
            
            foreach (ColoredMessage message in _messageLog.GetLastMessage(Height))
            {
                Renderer.DrawString(batch, _font, message.Message, X, Y + line++, message.Color);
            }

            base.Draw(batch);
        }
    }
}
