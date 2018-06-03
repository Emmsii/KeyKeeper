using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Managers
{
    public class MessageLogManager
    {
        private List<string> _messages = new List<string>();

        public void AddMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }
            _messages.Add(message.Trim());
        }

        public List<string> GetMessages()
        {
            return _messages;
        }

        public IEnumerable<string> GetLastMessage(int count)
        {
            if(count <= 0)
            {
                throw new ArgumentException("Cannot get less than 1 messages.");
            }

            for(int i = _messages.Count - 1; i >= Math.Min(0, count); i--)
            {
                yield return _messages[i];
            }
        }
    }
}
