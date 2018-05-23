using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyKeeper.Action;
using KeyKeeper.Interfaces;
using KeyKeeper.Managers;

namespace KeyKeeper.Entities.AI
{
    public class MonsterAi : CreatureAi
    {
        public MonsterAi(Creature creature) : base(creature)
        {
        }

        public override IAction DecideNextAction()
        {
            int rx = GameManager.Random.Next(-1, 2);
            int ry = GameManager.Random.Next(-1, 2);
            return new MoveAction(rx, ry, 0);
        }
    }
}
