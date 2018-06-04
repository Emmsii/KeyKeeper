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

        private const int _widthInTiles = 74;
        private const int _heightInTiles = 46;

        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private readonly MouseInputHandler _mouseInput = new MouseInputHandler();
        private readonly DelayedInputHandler _delayedInput = new DelayedInputHandler(20, 2);    
                
        private GameManager _gameManager;

        private KeyboardState _lastState;
        private bool _showDebugBoxes = false;

        public KeyKeeper()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _delayedInput.InputFireEvent += HandleInputEvent;

            _graphics.PreferredBackBufferWidth = Assets.DEFAULT_TEXTURE_WIDTH * Assets.UI_SPRITE_SCALE * _widthInTiles;
            _graphics.PreferredBackBufferHeight = Assets.DEFAULT_TEXTURE_HEIGHT * Assets.UI_SPRITE_SCALE * _heightInTiles;

            IsMouseVisible = true;
        }

        protected override void Initialize()
        { 
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Assets.LoadAssets(Content);

            _gameManager = new GameManager(Environment.TickCount, _widthInTiles, _heightInTiles);
            _gameManager.Init(_mouseInput);
        }

        protected override void UnloadContent()
        {

        }

        private void HandleInputEvent(object sender, KeyEventArgs args)
        {
            _gameManager.Input(args.Key);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.F1) && !_lastState.IsKeyDown(Keys.F1))
            {
                _showDebugBoxes = !_showDebugBoxes;
            }

            _delayedInput.Update(Keyboard.GetState());
            _mouseInput.Update(_graphics.GraphicsDevice.Viewport);
            _gameManager.Update();

            base.Update(gameTime);

            _lastState = Keyboard.GetState();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, blendState: BlendState.AlphaBlend);
            _gameManager.Draw(_spriteBatch);
            _spriteBatch.End();

            if (_showDebugBoxes)
            {
                DrawDebugBoxes(_spriteBatch);
            }

            base.Draw(gameTime);
        }

        public void DrawDebugBoxes(SpriteBatch batch)
        {
            batch.Begin(samplerState: SamplerState.PointClamp, blendState: BlendState.AlphaBlend);
            Rectangle destination = new Rectangle();
            Sprite sprite = Assets.GetSprite("debug_box_small");

            for(int y = 0; y < _heightInTiles; y++)
            {
                for(int x = 0; x < _widthInTiles; x++)
                {
                    destination.X = x * sprite.Width * sprite.Scale;
                    destination.Y = y * sprite.Height * sprite.Scale;
                    destination.Width = sprite.Width * sprite.Scale;
                    destination.Height = sprite.Height* sprite.Scale;

                    batch.Draw(sprite.Texture, destination, sprite.Bounds, Color.Red);
                }
            }

            batch.End();
        }

    }
}
