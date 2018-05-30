using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using KeyKeeper.Input;
using KeyKeeper.Managers;
using System;
using KeyKeeper.Content;

namespace KeyKeeper
{
    public class KeyKeeper : Game
    {

        private const int _scale = 2;
        private const int _tileSize = 16;

        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private readonly DelayedInputHandler _delayedInput = new DelayedInputHandler(20, 2);
        private readonly MouseInputHandler _mouseInput = new MouseInputHandler();

        private GameManager _gameManager;

        public KeyKeeper()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _delayedInput.InputFireEvent += HandleInputEvent;

            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _gameManager = new GameManager(Environment.TickCount);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Assets.LoadAssets(Content);

            int widthInTiles = (_graphics.PreferredBackBufferWidth / _tileSize) / _scale;
            int heightInTiles = (_graphics.PreferredBackBufferHeight / _tileSize) / _scale;

            _gameManager.Init(widthInTiles, heightInTiles, _scale);
            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private void HandleInputEvent(object sender, InputEventArgs args)
        {
            // Handle input
            _gameManager.Input(args.Key);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _delayedInput.Update(Keyboard.GetState());
            _mouseInput.Update(Mouse.GetState());

            _gameManager.MouseInput(_mouseInput);
            _gameManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _gameManager.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
