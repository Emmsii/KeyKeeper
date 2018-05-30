using KeyKeeper.Action;
using KeyKeeper.Content;
using KeyKeeper.Entities;
using KeyKeeper.Entities.AI;
using KeyKeeper.Generators;
using KeyKeeper.Input;
using KeyKeeper.Interfaces;
using KeyKeeper.Managers.Replays;
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
        public int GameSeed { get; }
        public int WorldSeed { get; }

        private readonly ReplayCaptureManager<IAction> _replayCaptureManager;
        private readonly CreatureManager _creatureManager = new CreatureManager();
        public GameWorld GameWorld { get; private set; }
        public Hero Hero { get; private set; }

        public static Random Random;

        private EmptyScreen _mainScreen;

        //private LevelScreen _levelScreen;
        //private StatScreen _statScreen;

        public int Tick { get; private set; }

        public GameManager(int gameSeed)
        {
            GameSeed = gameSeed;
            Random = new Random(GameSeed);
            WorldSeed = Random.Next();

            _replayCaptureManager = new ReplayCaptureManager<IAction>(this, "Replays/");
            _replayCaptureManager.RegisterType<MoveAction>(0);
            _replayCaptureManager.RegisterType<WaitAction>(1);

            // TODO: Only do this if starting a new game.
            _replayCaptureManager.StartNewReplay();
        }

        public void Init(int widthInTiles, int heightInTiles, int scale)
        {
            GameWorld = new WorldGenerator(12, 12, 1, WorldSeed).Generate().Build();

            Hero = new Hero(Assets.GetSpecies("hero"));
            new HeroAi(Hero);

            Hero.NewActionSetEvent += _replayCaptureManager.AddReplayEvent;

            GameWorld.AddCreature(new Point(10, 10), 0, Hero);

            Monster monster = new Monster(Assets.GetSpecies("troll"));
            new MonsterAi(monster);
            GameWorld.AddCreature(new Point(1, 1), 0, monster);

            _mainScreen = new EmptyScreen(0, 0, widthInTiles, heightInTiles, scale);

            _mainScreen.AddScreen(new LevelScreen(0, 0, 12, 12, scale, true, this));
            _mainScreen.AddScreen(new StatScreen(12, 0, 12, 12, scale));
        }

        public void Input(Keys key)
        {
            Hero.Input(key);
        }

        public void MouseInput(MouseInputHandler mouseInput)
        {
            _mainScreen.MouseInput(mouseInput);
        }

        public void Update()
        {
            Tick++;
            _creatureManager.UpdateCreatures(GameWorld.GetCreatures(Hero.Depth));
        }

        public void Draw(SpriteBatch batch)
        {
            //_levelScreen.Draw(batch);
            //_statScreen.Draw(batch);
            _mainScreen.Draw(batch);
        }
    }
}
