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

        public LogScreen(int x, int y, int width, int height, bool hasBorder, MessageLogManager messageLog) : base(x, y, width, height, hasBorder)
        {
            _messageLog = messageLog;
        }

        public override void Draw(SpriteBatch batch)
        {

            base.Draw(batch);
        }
    }
}
