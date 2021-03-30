using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace JankyPong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Ball sprite
        Texture2D ballSprite;

        // Ball location
        Vector2 ballPos;

        // Information about the sprites movement
        Vector2 ballSpeed = new Vector2(150, 150);

        // Paddle sprite
        Texture2D paddleSprite;

        // Paddle location
        Vector2 paddlePos;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            ballSprite = Content.Load<Texture2D>("testBall");

            paddleSprite = Content.Load<Texture2D>("testPaddle");

            paddlePos = new Vector2(
                GraphicsDevice.Viewport.Width / 2 - paddleSprite.Width / 2 ,
                GraphicsDevice.Viewport.Height - paddleSprite.Height
                );
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            // Move the Sprite by speed multiplied by elapsed time
            ballPos += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            int maxX = GraphicsDevice.Viewport.Width - ballSprite.Width;
            int maxY = GraphicsDevice.Viewport.Height - ballSprite.Height;

            // Check if ball is hitting a wall
            if (ballPos.X > maxX || ballPos.X < 0)
                ballSpeed.X *= -1;

            if (ballPos.Y < 0)
                ballSpeed.Y *= -1;
            else if (ballPos.Y > maxY)
            {
                // Ball hit bottom of the screen so reset ball
                ballPos.Y = 0;
                ballSpeed.X = 150;
                ballSpeed.Y = 150;
            }

            // Check for Input
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Right))
                paddlePos.X += 5;
            else if (keyState.IsKeyDown(Keys.Left))
                paddlePos.X -= 5;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            _spriteBatch.Draw(ballSprite, ballPos, Color.White);
            _spriteBatch.Draw(paddleSprite, paddlePos, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
