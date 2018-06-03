using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Managers
{
    public class MessageLogManager
    {
        private readonly Color DefaultMessageColor = Color.White;

        private List<ColoredMessage> _messages = new List<ColoredMessage>();

        public int Count => _messages.Count;

        public void AddMessage(string message, Color? color = null)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            _messages.Add(new ColoredMessage(message.Trim(), color ?? DefaultMessageColor));
        }

        public List<ColoredMessage> GetMessages()
        {
            return _messages;
        }

        public IEnumerable<ColoredMessage> GetLastMessage(int count)
        {
            return _messages.Skip(Math.Max(0, _messages.Count() - count));
        }
    }

    public class ColoredMessage
    {
        public string Message { get; }
        public Color Color { get; }
        
        public ColoredMessage(string message, Color color)
        {
            Message = message;
            Color = color;
        }
    }
}
