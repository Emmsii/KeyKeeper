using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Managers.Replays
{
    public class ReplayException : Exception
    {
        public ReplayException() : base() { }

        public ReplayException(string message) : base(message) { }

        public ReplayException(string message, Exception exception) : base(message, exception) { }
    }
}
