using KeyKeeper.Interfaces;
using KeyKeeper.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Entities
{
    public abstract class Creature
    {
        private readonly EnergyManager _energyManager = new EnergyManager();
        private int _speed = EnergyManager.NORMAL_SPEED;
        public int Speed => _speed; // + species.BaseSpeed;

        public virtual bool NeedsInput { get; set; } = false;
        
        public IAction GetAction() => OnGetAction();
        public bool CanTakeTurn => _energyManager.CanTakeTurn;
        public bool GainEnergy => _energyManager.GainEnergy(Speed);
        public void FinishTurn() => _energyManager.Spend();

        protected abstract IAction OnGetAction();

    }
}
