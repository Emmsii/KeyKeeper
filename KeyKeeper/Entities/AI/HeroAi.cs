using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyKeeper.Interfaces;

namespace KeyKeeper.Entities.AI
{
    public class HeroAi : CreatureAi
    {
        public HeroAi(Creature creature) : base(creature)
        {

        }

        public override IAction DecideNextAction()
        {
            return null;
        }
    }
}
