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

            ballPos = new Vector2(
                GraphicsDevice.Viewport.Width / 2 - ballSprite.Width / 2 ,
                GraphicsDevice.Viewport.Height / 2 - ballSprite.Height / 2
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
            if (ballPos.X > maxX)
                ballSpeed.X *= -1;

            if (ballPos.X < 0)
            {
                ballPos.X = maxX / 2;
                ballPos.Y = maxY / 2;
                ballSpeed.X = 150;
                ballSpeed.Y = 150;
            }
             
            if (ballPos.Y > maxY || ballPos.Y < 0)
                ballSpeed.Y *= -1;
            
            // Check if the ball hit the paddle
            Rectangle ballRect =
                new Rectangle((int)ballPos.X, (int)ballPos.Y,
                paddleSprite.Width, paddleSprite.Height);

            Rectangle paddleRect =
                new Rectangle((int)paddlePos.X, (int)paddlePos.Y,
                paddleSprite.Width, paddleSprite.Height);

            if (ballRect.Intersects(paddleRect) && ballSpeed.X < 0)
            {
                // Increase speed
                ballSpeed.Y += 50;
                if (ballSpeed.X < 0)
                    ballSpeed.X -= 50;
                else
                    ballSpeed.X += 50;

                // Send ball back up the screen
                ballSpeed.X *= -1;
            }

            // Keep the paddle on screen
            if (paddlePos.Y < 0)
                paddlePos.Y = 0;
            if (paddlePos.Y > maxY - paddleSprite.Height / 2)
                paddlePos.Y = maxY - paddleSprite.Height /2;

            // Check for Input
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Down))
                paddlePos.Y += 5;
            else if (keyState.IsKeyDown(Keys.Up))
                paddlePos.Y -= 5;

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
