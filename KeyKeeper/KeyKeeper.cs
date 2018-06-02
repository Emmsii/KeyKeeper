using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using KeyKeeper.Input;
using KeyKeeper.Managers;
using System;
using KeyKeeper.Content;
using KeyKeeper.Graphics;

namespace KeyKeeper
{
    public class KeyKeeper : Game
    {

        private const int _scale = 2;

        private int _widthInTiles;
        private int _heightInTiles;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private DelayedInputHandler _delayedInput = new DelayedInputHandler(20, 2);

        private GameManager _gameManager;

        public KeyKeeper()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _delayedInput.InputFireEvent += HandleInputEvent;

            _widthInTiles = _graphics.PreferredBackBufferWidth / (16 * _scale);
            _heightInTiles = _graphics.PreferredBackBufferHeight / (16 * _scale);

            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _gameManager = new GameManager(Environment.TickCount, _widthInTiles, _heightInTiles);
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Assets.LoadAssets(Content);

            

            _gameManager.Init();
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
            _gameManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, blendState: BlendState.Opaque);
            _gameManager.Draw(_spriteBatch);
            _spriteBatch.End();

            DrawDebugBoxes(_spriteBatch);

            base.Draw(gameTime);
        }

        public void DrawDebugBoxes(SpriteBatch batch)
        {
            batch.Begin(samplerState: SamplerState.PointClamp, blendState: BlendState.AlphaBlend);
            Rectangle destination = new Rectangle();
            Sprite sprite = Assets.GetSprite("debug_box");

            for(int y = 0; y < _heightInTiles; y++)
            {
                for(int x = 0; x < _widthInTiles; x++)
                {
                    destination.X = x * sprite.Width * 2;
                    destination.Y = y * sprite.Height * 2;
                    destination.Width = sprite.Width * 2;
                    destination.Height = sprite.Height* 2;

                    batch.Draw(sprite.Texture, destination, sprite.Bounds, Color.Red);
                }
            }

            batch.End();
        }

    }
}
