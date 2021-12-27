using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Match3MG
{
    public class PlayButton : Component
    {
        #region Fields
        private MouseState _currentMouse;

        //private SpriteFont _font;

        private bool _isHovering;

        private MouseState _previousMouse;

        private Texture2D _texture;
        #endregion

        #region Properties
        public event EventHandler Click;

        public bool Clicked { get; private set; }

        public Color PenColor { get; set; }

        public Vector2 Position { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width / 7 * 2, _texture.Height / 7 * 2);
            }
        }

        public string Text { get; set; }
        #endregion

        public PlayButton(Texture2D texture)
        {
            _texture = texture;

            PenColor = Color.Black;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var color = Color.White;

            if (_isHovering)
                color = Color.Gray;

            spriteBatch.Draw(_texture, Rectangle, color);
        }

        public override void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            _isHovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                _isHovering = true;

                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                } 
            } 
        }
    }
}
