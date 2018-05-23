using KeyKeeper.Content;
using KeyKeeper.Entities;
using KeyKeeper.Entities.AI;
using KeyKeeper.Generators;
using KeyKeeper.Screen;
using KeyKeeper.World;
using Microsoft.Xna.Framework;
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

        public static Random Random;

        private LevelScreen _levelScreen;

        public GameManager(int gameSeed)
        {
            _gameSeed = gameSeed;
            Random = new Random(_gameSeed);
            _worldSeed = Random.Next();

            _levelScreen = new LevelScreen(0, 0, 12, 12, 2, this);
        }

        public void Init()
        {
            GameWorld = new WorldGenerator(12, 12, 1, _worldSeed).Generate().Build();

            Hero = new Hero(Assets.GetSpecies("hero"));
            new HeroAi(Hero);

            GameWorld.AddCreature(new Point(10, 10), 0, Hero);

            Monster monster = new Monster(Assets.GetSpecies("troll"));
            new MonsterAi(monster);
            GameWorld.AddCreature(new Point(1, 1), 0, monster);

        }

        public void Input(Keys key)
        {
            Hero.Input(key);
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
