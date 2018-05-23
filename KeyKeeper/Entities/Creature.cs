using KeyKeeper.Entities.AI;
using KeyKeeper.Interfaces;
using KeyKeeper.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Entities
{
    public abstract class Creature : Entity
    {
        private Species Species { get; }

        public CreatureAi Ai { get; private set; }

        private readonly EnergyManager _energyManager = new EnergyManager();
        private int _speed = EnergyManager.NORMAL_SPEED;
        public int Speed => _speed + Species.BaseSpeed;

        public virtual bool NeedsInput { get; set; } = false;
        
        public IAction GetAction() => OnGetAction();
        public bool CanTakeTurn => _energyManager.CanTakeTurn;
        public bool GainEnergy => _energyManager.GainEnergy(Speed);
        public void FinishTurn() => _energyManager.Spend();

        public bool HasMoved { get; private set; } = false;

        public Creature(Species species) : base(species.Name, species.Sprite, species.ForegroundColor)
        {
            Species = species;
        }

        protected abstract IAction OnGetAction();

        public bool MoveBy(int x, int y, int depth)
        {
            if (!_world.InBounds(X + x, Y + y, Depth + depth)) return false;
            bool canMove = Ai.CanMove(X + x, Y + y, Depth + depth);
            if (canMove)
            {
                X += x;
                Y += y;
                //if (depth != 0) _world.SwitchCreatureLevel(this, Depth + depth);
            }
            HasMoved = canMove;
            return HasMoved;
        }

        public void SetAi(CreatureAi ai)
        {
            Ai = ai;
        }
    }
}
