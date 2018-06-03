using KeyKeeper.Action;
using KeyKeeper.Content;
using KeyKeeper.Entities;
using KeyKeeper.Entities.AI;
using KeyKeeper.Generators;
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
        private readonly MessageLogManager _messageLogManager = new MessageLogManager();
        private readonly CreatureManager _creatureManager = new CreatureManager();

        public GameWorld GameWorld { get; private set; }
        public Hero Hero { get; private set; }

        public static Random Random;

        //private LevelScreen _levelScreen;
        private BaseScreen _baseScreen;

        public int Tick { get; private set; }

        public GameManager(int gameSeed, int widthInTiles, int heightInTiles)
        {
            GameSeed = gameSeed;
            Random = new Random(GameSeed);
            WorldSeed = Random.Next();

            _baseScreen = new BaseScreen(0, 0, widthInTiles, heightInTiles, false);
            _baseScreen.AddScreen(new LevelScreen(0, 0, 48, 30, true, this));
            //_baseScreen.AddScreen(new StatScreen(24, 0, 13, 23, 2, true));
            _basescreen.addscreen(new logscreen(0, 15, 24, 8, 2, true));

            //LevelScreen levelScreen = new LevelScreen(2, 3, 5, 5, 2, true, this);
            //levelScreen.ParentAnchorPosition = Alignment.None;
            //levelScreen.SelfAnchorPosition = Alignment.Left;
            //_baseScreen.AddScreen(levelScreen);
            //_levelScreen = new LevelScreen(0, 0, 12, 12, 2, this);

            _replayCaptureManager = new ReplayCaptureManager<IAction>(this, "Replays/");
            _replayCaptureManager.RegisterType<MoveAction>(0);
            _replayCaptureManager.RegisterType<WaitAction>(1);

            // TODO: Only do this if starting a new game.
            _replayCaptureManager.StartNewReplay();
        }

        public void Init()
        {
            GameWorld = new WorldGenerator(80, 80, 1, WorldSeed).Generate().Build();

            Hero = new Hero(Assets.GetSpecies("hero"));
            new HeroAi(Hero);

            Hero.NewActionSetEvent += _replayCaptureManager.AddReplayEvent;

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
            Tick++;
            _creatureManager.UpdateCreatures(GameWorld.GetCreatures(Hero.Depth));
        }

        public void Draw(SpriteBatch batch)
        {
            _baseScreen.Draw(batch);   
        }
    }
}
