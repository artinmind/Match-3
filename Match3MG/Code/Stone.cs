using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Match3MG
{
    

    public class Stone
    {
        //public static Texture2D _texture;
        //public static Vector2 _position /*= new Vector2(10, 10)*/;
        //public static float _scale;
        public Type type1 { get; set; }
        public Point _point;
        public bool ver { get; set; }
        public bool hor { get; set; }
        public bool coloredBomb { get; set; }

        public Stone(Type type, Point _point, bool ver, bool hor)
        {
            this.type1 = type;
            this._point = _point;
            this.ver = ver;
            this.hor = hor;
        }

        //public Stone(Texture2D texture)
        //{
        //    _texture = texture;
        //}

        //public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        //{
        //    //spriteBatch.Draw(_texture, _position, Color.White);
        //    spriteBatch.Draw(_texture, _position, null, Color.White, 0, Vector2.Zero, 0.75f, SpriteEffects.None, 0);
        //}

        //public override void Update(GameTime gameTime)
        //{
            
        //}
    }
}
