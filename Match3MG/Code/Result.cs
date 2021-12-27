using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Match3MG
{
    class Result
    {
        public static SpriteFont ScoreFont { get; set; }
        //public static Vector2 Text = new Vector2(Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height);

        public static Texture2D Background { get; set; }
        public static Texture2D ButtonOk { get; set; }

        static public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(Background, new Rectangle(0, 0, 1200, 800), Color.White);
            _spriteBatch.DrawString(ScoreFont, "GAME OVER", new Vector2(380, 350), Color.PaleGoldenrod);
            _spriteBatch.DrawString(ScoreFont, "Your SCORE is ", new Vector2(300, 450), Color.PaleGoldenrod);
            _spriteBatch.DrawString(ScoreFont, Play.GameScore.ToString(), new Vector2(750, 450), Color.Orange);

            _spriteBatch.Draw(ButtonOk, new Rectangle(400, 600, 350, 100), Color.White);
        }

        static public void Update()
        {
        }
    }
}
