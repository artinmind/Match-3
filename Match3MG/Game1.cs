using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

enum Scene {Menu, Play, Result}

namespace Match3MG
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Scene Scene = Scene.Menu;
        private Texture2D _texture;
        private Texture2D selected;
        private Texture2D textureExplotion;
        
        private Field field;


        private List<Component> _gameComponents;
        private MouseState currentMouseState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
            currentMouseState = Mouse.GetState();
            // TODO: Add your initialization logic here 

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
           
            Menu.Background = Content.Load<Texture2D>("background");
            //Menu.PlayButton = Content.Load<Texture2D>("PlayButton");
            //Menu.Font = Content.Load<SpriteFont>("PlayBtnFont");

            Play.TimerFont = Content.Load<SpriteFont>("timerFont");
            Play.ScoreFont = Content.Load<SpriteFont>("scoreFont");
            Play.Background = Content.Load<Texture2D>("playBackg");
            Play.Field = Content.Load<Texture2D>("field");
            Play.Stat = Content.Load<Texture2D>("stat");

            Result.Background = Content.Load<Texture2D>("resultBackg");
            Result.ScoreFont = Content.Load<SpriteFont>("scoreFont");
            Result.ButtonOk = Content.Load<Texture2D>("buttonOk");

            _texture = Content.Load<Texture2D>("stoneAssets");
            textureExplotion = Content.Load<Texture2D>("explotion");
            selected = Content.Load<Texture2D>("selected");

            field = new Field(_texture, textureExplotion, selected);

            var playButton = new PlayButton(Content.Load<Texture2D>("PlayButton")) {
                //Position = new Vector2(450, 300)
                Position = new Vector2((Window.ClientBounds.Width / 2) - (_texture.Width / 4) - 50, (Window.ClientBounds.Height / 2) - (_texture.Height / 8) - 50)
            };

            playButton.Click += PlayButton_Click;

            _gameComponents = new List<Component>()
            {
                playButton
            };


            // TODO: use this.Content to load your game content here 
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            Scene = Scene.Play;
        }

        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //  Exit();
            if (!field.isInitField)
                field.InitField();
            MouseState lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            switch (Scene) {
                case Scene.Menu:
                    Menu.Update();
                    field.InitField();
                    //if (Keyboard.GetState().IsKeyDown(Keys.Space)) Scene = Scene.Play;
                    break;
                case Scene.Play:
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Scene = Scene.Menu;
                    if (currentMouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
                        field.MouseLeftClick(currentMouseState.X, currentMouseState.Y);
                    break;
                case Scene.Result:
                    if (Keyboard.GetState().IsKeyDown(Keys.Space)) Scene = Scene.Result;
                    if (currentMouseState.X > 400 && currentMouseState.X < 750 && currentMouseState.Y > 600 && currentMouseState.Y < 700 && currentMouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
                        Scene = Scene.Menu;
                    break;
            }

            foreach (var component in _gameComponents)
                component.Update(gameTime);
            
            Menu.Update();
            // TODO: Add your update logic here 

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.PaleGoldenrod);

            _spriteBatch.Begin();
            switch (Scene)
            {
                case Scene.Menu:
                    Menu.Draw(_spriteBatch);
                    foreach (var component in _gameComponents)
                        component.Draw(gameTime, _spriteBatch);
                    break;
                case Scene.Play:
                    Play.Draw(_spriteBatch);
                    field.Draw(gameTime, _spriteBatch);
                    //if (Keyboard.GetState().IsKeyDown(Keys.F))
                    //    Play.Draw(_spriteBatch);
                    field.time.Start();
                    if (field.time.Elapsed.Seconds == 59)
                    {
                        field.time.Stop();
                        //Menu.Draw(_spriteBatch);
                        Scene = Scene.Result;
                    }                        
                    break;
                case Scene.Result:
                    Result.Draw(_spriteBatch);
                    break;
            }
            _spriteBatch.End();
            // TODO: Add your drawing code here 

            base.Draw(gameTime);
        }
    }
}
