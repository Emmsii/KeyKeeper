using KeyKeeper.Entities;
using KeyKeeper.Generators;
using KeyKeeper.World;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Managers
{
    public class GameManager
    {
        private readonly int _gameSeed;
        private readonly int _worldSeed;
                
        private readonly CreatureManager _creatureManager = new CreatureManager();
        private GameWorld _gameWorld;
        private Hero _hero;

        public Random Random;

        public GameManager(int gameSeed)
        {
            _gameSeed = gameSeed;
            Random = new Random(_gameSeed);
            _worldSeed = Random.Next();

        }

        public void Init()
        {
            _gameWorld = new GameWorld(new EmptyLevelGenerator(32, 32, 1).Generate().Build());
        }

        public void Input(Keys key)
        {

        }

        public void Update()
        {
            _creatureManager.UpdateCreatures(_gameWorld.GetCreatures(_hero.Depth));
        }
    }
}
