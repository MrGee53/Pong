using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Pong
{
    public class Ball : Microsoft.Xna.Framework.DrawableGameComponent
    {
        
        #region Private Members
        private SpriteBatch spriteBatch;
        private readonly ContentManager contentManager;

        // Default speed of ball
        private const float DEFAULT_X_SPEED = 150;
        private const float DEFAULT_Y_SPEED = 150;

        // Initial location of the ball
        private const float INIT_X_POS = 80;
        private const float INIT_Y_POS = 80;

        private const float INCREASE_SPEED = 50;

        // Ball sprite
        private Texture2D ballSprite;

        // Ball Location
        private Vector2 ballPostition;

        // Ball's Motion
        Vector2 ballSpeed = new Vector2(DEFAULT_X_SPEED, DEFAULT_Y_SPEED);
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the Ball's horizontal speed.
        /// </summary>
        public float SpeedX
        {
            get { return ballSpeed.X; }
            set { ballSpeed.X = value; }
        }

        /// <summary>
        /// Gets or sets the Ball's vertical speed
        /// </summary>
        public float SpeedY
        {
            get { return ballSpeed.Y; }
            set { ballSpeed.Y = value; }
        }

        /// <summary>
        /// Gets or sets the X position of the ball.
        /// </summary>
        public float X
        {
            get { return ballPostition.X; }
            set { ballPostition.X = value; }
        }

        /// <summary>
        /// Gets or sets the Y position of the ball.
        /// </summary>
        public float Y
        {
            get { return ballPostition.Y; }
            set { ballPostition.Y = value; }
        }

        /// <summary>
        /// Gets the Width of the ball sprite
        /// </summary>
        public int Width
        {
            get { return ballSprite.Width; }
        }

        /// <summary>
        /// Gets the Height of the ball sprite
        /// </summary>
        public int Height
        {
            get { return ballSprite.Height; }
        }

        /// <summary>
        /// Gets the bounding rectangle of the ball.
        /// </summary>
        public Rectangle Boundary
        {
            get
            {
                return new Rectangle((int)ballPostition.X, (int)ballPostition.Y,
                    ballSprite.Width, ballSprite.Height);
            }
        }
        #endregion

        public Ball(Game game)
            : base(game)
        {
            contentManager = new ContentManager(game.Services);
        }

        /// <summary>
        /// Set the ball in the middle of the screen with default
        /// speed.
        /// </summary>
        public void Reset()
        {
            ballSpeed.X = DEFAULT_X_SPEED;
            ballSpeed.Y = DEFAULT_Y_SPEED;

            ballPostition.X = GraphicsDevice.Viewport.Width / 2 
                - ballSprite.Width;
            ballPostition.Y = GraphicsDevice.Viewport.Height / 2
                - ballSprite.Height;
        }

        /// <summary>
        /// Increases the ball's speed in both the X and Y
        /// directions.
        /// </summary>
        public void SpeedUp()
        {
            if (ballSpeed.Y < 0)
                ballSpeed.Y -= INCREASE_SPEED;
            else
                ballSpeed.Y += INCREASE_SPEED;

            if (ballSpeed.X < 0)
                ballSpeed.X -= INCREASE_SPEED;
            else
                ballSpeed.X += INCREASE_SPEED;
        }

        /// <summary>
        /// Inverts the ball's horizontal direction.
        /// </summary>
        public void ChangeHorzDirection()
        {
            ballSpeed.X *= -1;
        }

        /// <summary>
        /// Inverts the ball's vertical direction
        /// </summary>
        public void ChangeVertDirection()
        {
            ballSpeed.Y *= -1;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs
        /// before starting to run
        /// </summary>
        public override void Initialize()
        {
            ballPostition.X = INIT_X_POS;
            ballPostition.Y = INIT_Y_POS;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place
        /// to load all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ballSprite = contentManager.Load<Texture2D>(@"Content\testball");

            ballPostition.X = GraphicsDevice.Viewport.Width / 2
                - ballSprite.Width;
            ballPostition.Y = GraphicsDevice.Viewport.Height / 2
                - ballSprite.Height;
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">
        /// Provides a snapshot of timing values.
        /// </param>
        public override void Update(GameTime gameTime)
        {
            // Move the sprite by speed, scaled by elapsed time.
            ballPostition += ballSpeed * 
                (float)gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();
            spriteBatch.Draw(ballSprite, ballPostition, Color.White);
            spriteBatch.End();
        }
    }
}
