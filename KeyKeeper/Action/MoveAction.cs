using KeyKeeper.Entities;
using KeyKeeper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Action
{
    public sealed class MoveAction : IAction
    {
        private int _x, _y, _depth;

        public MoveAction(int x, int y, int depth)
        {
            _x = x;
            _y = y;
            _depth = depth;
        }

        public ActionResult Perform(Creature creature)
        {
            if (_x == 0 && _y == 0 && _depth == 0) return ActionResult.SUCCESS;

            // move logic
            if(creature.MoveBy(_x, _y, _depth))
            {
                return ActionResult.SUCCESS;
            }

            return ActionResult.SUCCESS;
        }
    }
}
