using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace JankyPong
{
    public class Paddle : Microsoft.Xna.Framework.DrawableGameComponent
    {
        #region Private Members
        private SpriteBatch spriteBatch;
        private readonly ContentManager contentManager;

        // Paddle Sprite
        private Texture2D paddleSprite;

        // Paddle location
        private Vector2 paddlePosition;

        private const float DEFAULT_Y_SPEED = 300;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the paddle vertical speed.
        /// </summary>
        public float Speed { get; set; }

        /// <summary>
        /// Gets or sets the X position of the paddle.
        /// </summary>
        public float X
        {
            get { return paddlePosition.X; }
            set { paddlePosition.X = value; }
        }

        /// <summary>
        /// Gets or sets the Y position of the paddle.
        /// </summary>
        public float Y
        {
            get { return paddlePosition.Y; }
            set { paddlePosition.Y = value; }
        }

        /// <summary>
        /// Gets the width of the paddle's sprite
        /// </summary>
        public int Width
        {
            get { return paddleSprite.Width; }
        }

        /// <summary>
        /// Gets the height of the paddle's sprite.
        /// </summary>
        public int Height
        {
            get { return paddleSprite.Height; }
        }

        /// <summary>
        /// Gets the bounding rectangle of the paddle.
        /// </summary>
        public Rectangle Boundary
        {
            get
            {
                return new Rectangle((int)paddlePosition.X, (int)paddlePosition.Y,
                    paddleSprite.Width, paddleSprite.Height);
            }
        }
        #endregion

        public Paddle(Game game)
            : base(game)
        {
            contentManager = new ContentManager(game.Services);
        }

        /// <summary>
        /// Allows the game component to perform any initializataion it needs
        /// to before starting to run. This is where it can query for any 
        /// required services and load content.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
            
            // Make sure base.Initialize() is called before this or 
            // paddleSprite will be null.
            X = 0;
            Y = (GraphicsDevice.Viewport.Height - Height) / 2;

            Speed = DEFAULT_Y_SPEED;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to 
        /// load all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            paddleSprite = contentManager.Load<Texture2D>(@"Content\testPaddle");
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // Add update logic here

            // Scale the movement based on time
            float moveDistance = Speed * (float)gameTime.ElapsedGameTime
                .TotalSeconds;

            // Move the paddle, but don't allow movement off the screen
            KeyboardState newKeyState = Keyboard.GetState();
            if (newKeyState.IsKeyDown(Keys.Down) && Y + 
                paddleSprite.Width + moveDistance <= GraphicsDevice.Viewport.Height)
            {
                Y += moveDistance;
            }
            else if (newKeyState.IsKeyDown(Keys.Up) && Y - moveDistance >= 0)
            {
                Y -= moveDistance;
            }

            base.Update(gameTime);
        }

        // TODO Add Draw method

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(paddleSprite, paddlePosition, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
