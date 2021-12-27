using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Match3MG
{
    class Play
    {
        public static Texture2D Background { get; set; }
        public static Texture2D Field { get; set; }
        public static Texture2D Stat { get; set; }
        public static long GameScore {get; set;}
        
        public static SpriteFont TimerFont { get; set; }
        static Vector2 TimerPos = new Vector2(830, 100);
        public static SpriteFont ScoreFont{ get; set; }
        static Vector2 ScorePos = new Vector2(830, 250);

        public static void IncGameScore(int b)
        {
            GameScore += (10 * b);
        }

        static public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(Background, new Rectangle(0, 0, 1200, 800), Color.White);
            //_spriteBatch.Draw(Field, new Rectangle(0, 0, 800, 800), Color.White);
            _spriteBatch.Draw(Stat, new Rectangle(801, 0, 1200, 800), Color.White);
            _spriteBatch.DrawString(TimerFont, "Time: ", TimerPos, Color.Crimson);
            _spriteBatch.DrawString(ScoreFont, "Score: " + GameScore.ToString(), ScorePos, Color.PaleGoldenrod);
            //_spriteBatch.DrawString(ScoreFont, GameScore.ToString(), new Vector2(ScorePos.X + 180, ScorePos.Y), Color.LightGoldenrodYellow);
        }
    }
}
