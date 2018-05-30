using KeyKeeper.Interfaces;
using KeyKeeper.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Entities.AI
{
    public abstract class CreatureAi
    {
        protected readonly Creature _creature;
        public Creature Creature { get => _creature; }

        public CreatureAi(Creature creature)
        {
            _creature = creature;
            creature.SetAi(this);
        }

        public abstract IAction DecideNextAction();

        public bool CanMove(int x, int y, int depth)
        {
            if (!CanEnter(x, y)) return false;

            if (depth == 1 && _creature.CurrentLevel.GetCell(x, y).Name.Equals("stairs_down"))
            {
                return true;
            }
            else if (depth == -1 && _creature.CurrentLevel.GetCell(x, y).Name.Equals("stairs_up"))
            {
                return true;
            }

            /*
             * TODO: if depth != 0, switch level.
             */

            //_creature.World.Move(x, y, depth, _creature);
            return true;
        }

        public bool CanEnter(int x, int y)
        {
            if (_creature.CurrentLevel.IsSolid(x, y)) return false;
            if (_creature.CurrentLevel.GetCreature(x, y) != null) return false;
            return true;
        }
    }
}
