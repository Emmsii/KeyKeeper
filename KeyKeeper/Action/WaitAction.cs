using KeyKeeper.Entities;
using KeyKeeper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Action
{
    public sealed class WaitAction : IAction
    {
        public WaitAction()
        {
        }

        public ActionResult Perform(Creature creature)
        {
            return ActionResult.SUCCESS;
        }
    }
}
