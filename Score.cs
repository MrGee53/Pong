using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Pong
{
    class Score : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private ContentManager contentManager;

        // Font
        private SpriteFont font;

        private string score;


        public int Player1Score { get; set; }
        public int Player2Score { get; set; }

        public Score(Game game) : base(game)
        {
            contentManager = new ContentManager(game.Services);
        }

        public override void Initialize()
        {
            base.Initialize();

            Player1Score = 0;
            Player2Score = 0;

            score = $"{Player1Score}  |  {Player2Score}";

        }

        public void Reset()
        {
            Player1Score = 0;
            Player2Score = 0;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = contentManager.Load<SpriteFont>(@"Content\scoreFont");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            Vector2 textMiddlePoint = font.MeasureString(score);
            Vector2 position = new Vector2((GraphicsDevice.Viewport.Width / 2) - 
                font.MeasureString($"{Player1Score}  |  {Player2Score}").X / 2,
                0);

            spriteBatch.DrawString(font, $"{Player1Score}  |  {Player2Score}",
                position, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
