using KeyKeeper.Content;
using KeyKeeper.Entities;
using KeyKeeper.Generators;
using KeyKeeper.Screen;
using KeyKeeper.World;
using Microsoft.Xna.Framework.Graphics;
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
        public GameWorld GameWorld { get; private set; }
        public Hero Hero { get; private set; }

        public Random Random;

        private LevelScreen _levelScreen;

        public GameManager(int gameSeed)
        {
            _gameSeed = gameSeed;
            Random = new Random(_gameSeed);
            _worldSeed = Random.Next();

            _levelScreen = new LevelScreen(0, 0, 10, 10, 2, this);
        }

        public void Init()
        {
            GameWorld = new WorldGenerator(32, 32, 1, _worldSeed).Generate().Build();

            Hero = new Hero(Assets.GetSpecies("hero"));
        }

        public void Input(Keys key)
        {

        }

        public void Update()
        {
            _creatureManager.UpdateCreatures(GameWorld.GetCreatures(Hero.Depth));
        }

        public void Draw(SpriteBatch batch)
        {
            _levelScreen.Draw(batch);   
        }
    }
}
