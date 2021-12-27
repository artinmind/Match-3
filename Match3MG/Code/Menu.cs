using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Match3MG
{
    static class Menu
    {
        public static Texture2D Background { get; set; }
        public static Texture2D PlayButton { get; set; }
        //public static SpriteFont Font { get; set; }
        //static Vector2 btnPos = new Vector2(600, 330);

        static public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(Background, new Rectangle(0, 0, 1200, 800), Color.White);
            //_spriteBatch.Draw(PlayButton, new Rectangle(500, 250, 400, 200), Color.White);
            //_spriteBatch.DrawString(Font, "Play", btnPos, Color.Crimson);
        }

        static public void Update()
        {
        }

    }
}
