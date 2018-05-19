using KeyKeeper.Action;
using KeyKeeper.Entities;
using KeyKeeper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Managers
{
    public class CreatureManager
    {
        private int _currentCreatureIndex;

        private Creature CurrentCreature(List<Creature> creatures) => creatures[_currentCreatureIndex];

        public bool Running { get; set; } = true;

        public void UpdateCreatures(List<Creature> creatures)
        {
            while (Running)
            {
                Creature creature = CurrentCreature(creatures);

                if (creature.CanTakeTurn && creature.NeedsInput) return; // gameresult

                //gameresult.madeprofgress = true

                IAction action = null;
                while(action == null)
                {
                    //creature = CurrentCreature(creatures);
                    if(creature.CanTakeTurn || creature.GainEnergy)
                    {
                        if (creature.NeedsInput) return; // gameresult
                        action = creature.GetAction();
                    }
                    else
                    {
                        AdvanceCreatureIndex(creatures);
                    }
                }

                ActionResult result = action.Perform(creature);
                while(result.Alternative != null)
                {
                    action = result.Alternative;
                    result = action.Perform(creature);
                }

                if (result.Succeeded)
                {
                    creature.FinishTurn();
                    AdvanceCreatureIndex(creatures);
                    // check if hero, && has moved if yes compute fov
                }
            }
        }

        private void AdvanceCreatureIndex(List<Creature> creatures)
        {
            _currentCreatureIndex = (_currentCreatureIndex + 1) % creatures.Count;
        }
    }
}
