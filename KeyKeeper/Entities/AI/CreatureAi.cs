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
            if (!CanEnter(x, y, depth)) return false;
            //_creature.World.Move(x, y, depth, _creature);
            return true;
        }

        public bool CanEnter(int x, int y, int depth)
        {
            Cell cell = _creature.World.GetCell(x, y, depth);
            if (cell.IsSolid) return false;
            if (_creature.World.GetCreature(x, y, depth) != null) return false;
            return true;
        }
    }
}
