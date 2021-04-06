﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace JankyPong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Ball ball;
        private Paddle paddle1;
        private Paddle paddle2;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            paddle1 = new Paddle(this, false);
            paddle2 = new Paddle(this, true);
            ball = new Ball(this);


            Components.Add(paddle1);
            Components.Add(paddle2);
            Components.Add(ball);
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
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            int maxX = GraphicsDevice.Viewport.Width;
            int maxY = GraphicsDevice.Viewport.Height;

            // Check if ball is hitting a wall
            if (ball.X > maxX - ball.Width)
                ball.Reset();
            else if (ball.X < 0)
                ball.Reset();

            if (ball.Y > maxY - ball.Height)
                ball.ChangeVertDirection();
            else if (ball.Y < 0)
                ball.ChangeVertDirection();


            // Check if the ball is hitting the paddle
            if (ball.Boundary.Intersects(paddle1.Boundary) && ball.SpeedX < 0)
            {
                ball.SpeedUp();

                ball.ChangeHorzDirection();
            }
            else if (ball.Boundary.Intersects(paddle2.Boundary) && ball.SpeedX > 0)
            {
                ball.SpeedUp();

                ball.ChangeHorzDirection();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here


            base.Draw(gameTime);
        }
    }
}
