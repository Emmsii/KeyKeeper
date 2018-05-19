using KeyKeeper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Action
{
    public class ActionResult
    {
        public static readonly ActionResult SUCCESS = new ActionResult(true);
        public static readonly ActionResult FAILURE = new ActionResult(false);

        public bool Succeeded { get; }
        public IAction Alternative { get; }

        public ActionResult(bool succeeded)
        {
            Succeeded = succeeded;
            Alternative = null;
        }

        public ActionResult(IAction alternative)
        {
            Alternative = alternative;
            Succeeded = true;
        }
    }
}
