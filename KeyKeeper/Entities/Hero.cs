using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyKeeper.Action;
using KeyKeeper.Input;
using KeyKeeper.Interfaces;
using Microsoft.Xna.Framework.Input;

namespace KeyKeeper.Entities
{
    public class Hero : Creature
    {
        private IAction _nextAction = null;

        public delegate void NewActionSet(IAction action);
        public event NewActionSet NewActionSetEvent;

        public Hero(Species species) : base(species)
        {

        }

        public override bool NeedsInput => _nextAction == null;

        public void Input(Keys key)
        {
            if (Controls.NORTH.IsPressed(key)) SetNextAction(new MoveAction(0, -1, 0));
            else if (Controls.SOUTH.IsPressed(key)) SetNextAction(new MoveAction(0, 1, 0));
            else if (Controls.EAST.IsPressed(key)) SetNextAction(new MoveAction(1, 0, 0));
            else if (Controls.WEST.IsPressed(key)) SetNextAction(new MoveAction(-1, 0, 0));

            else if (Controls.NORTH_EAST.IsPressed(key)) SetNextAction(new MoveAction(1, -1, 0));
            else if (Controls.NORTH_WEST.IsPressed(key)) SetNextAction(new MoveAction(-1, -1, 0));
            else if (Controls.SOUTH_EAST.IsPressed(key)) SetNextAction(new MoveAction(1, 1, 0));
            else if (Controls.SOUTH_WEST.IsPressed(key)) SetNextAction(new MoveAction(-1, 1, 0));

            else if (Controls.WAIT.IsPressed(key)) SetNextAction(new WaitAction());
        }

        protected override IAction OnGetAction()
        {
            IAction action = _nextAction;
            _nextAction = null;
            NewActionSetEvent?.Invoke(action);
            return action;
        }

        private void SetNextAction(IAction nextAction)
        {
            _nextAction = nextAction;
        }
    }
}
