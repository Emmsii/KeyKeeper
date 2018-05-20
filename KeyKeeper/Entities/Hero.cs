using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyKeeper.Input;
using KeyKeeper.Interfaces;
using Microsoft.Xna.Framework.Input;

namespace KeyKeeper.Entities
{
    public class Hero : Creature
    {
        private IAction _nextAction = null;

        public Hero(Species species) : base(species)
        {

        }

        public override bool NeedsInput => _nextAction == null;

        public void Input(Keys key)
        {
            //if (Controls.NORTH.IsPressed(key)) SetNextAction(new MoveAction(0, -1, 0));
        }

        protected override IAction OnGetAction()
        {
            IAction action = _nextAction;
            _nextAction = null;
            return action;
        }

        private void SetNextAction(IAction nextAction)
        {
            _nextAction = nextAction;
        }
    }
}
