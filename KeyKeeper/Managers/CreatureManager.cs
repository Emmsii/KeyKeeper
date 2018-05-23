using KeyKeeper.Action;
using KeyKeeper.Entities;
using KeyKeeper.Helpers.Game;
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
        public int CurrentTurn { get; private set; }
        public int CurrentSubTurn { get; private set; }

        public GameResult UpdateCreatures(List<Creature> creatures)
        {
            GameResult gameResult = new GameResult();

            while (Running)
            {
                Creature creature = CurrentCreature(creatures);

                if (creature.CanTakeTurn && creature.NeedsInput)
                {
                    return gameResult;
                }

                CurrentSubTurn++;
                gameResult.MadeProgress = true;

                IAction action = null;
                while(action == null)
                {
                    creature = CurrentCreature(creatures);
                    if(creature.CanTakeTurn || creature.GainEnergy)
                    {
                        if (creature.NeedsInput)
                        {
                            return gameResult;
                        }
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
                    if(creature is Hero)
                    {
                        CurrentTurn++;
                        if (creature.HasMoved)
                        {
                            creature.World.ComputeFov(creature.X, creature.Y, creature.Depth, 10, Helpers.FovType.Shadowcast);
                        }
                    }
                }
            }

            return gameResult;
        }

        private void AdvanceCreatureIndex(List<Creature> creatures)
        {
            _currentCreatureIndex = (_currentCreatureIndex + 1) % creatures.Count;
        }
    }
}
