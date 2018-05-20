using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using KeyKeeper.Input;
using KeyKeeper.Managers;
using System;

namespace KeyKeeper
{
    public class KeyKeeper : Game
    {

        private const int _scale = 2;
        
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private DelayedInputHandler _delayedInput = new DelayedInputHandler(20, 2);

        private GameManager _gameManager;

        public KeyKeeper()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _delayedInput.InputFireEvent += HandleInputEvent;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Console.WriteLine("Init");
            _gameManager = new GameManager(Environment.TickCount);
            _gameManager.Init();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Console.WriteLine("Load");
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private void HandleInputEvent(object sender, InputEventArgs args)
        {
            // Handle input
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
