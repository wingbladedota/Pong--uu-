using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;


namespace Pong
{
    class Pong : Game
    {
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        public static ContentManager content;

        public Player p1, p2;
        public Ball ball;


        public static Color Background { get; private set; }
        public static Vector2 WindowSize;
        public static Vector2 center;

        [STAThread]
        static void Main()
        {
            Pong pong = new Pong();
            pong.Run();
        }
        
        public Pong()
        {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);
            Background = Color.White;
            WindowSize = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            center = new Vector2(WindowSize.X / 2, WindowSize.Y / 2);

            Player p1 = new Player();
            Player p2 = new Player();
            Ball ball = new Ball();

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            bool movingError = false;

            if (keyboardState.IsKeyDown(Keys.Up)){p1.move("up");}
            if (keyboardState.IsKeyDown(Keys.Down)){p1.move("down");}

            if (keyboardState.IsKeyDown(Keys.W)) {p2.move("up");}
            if (keyboardState.IsKeyDown(Keys.S)){p2.move("down");}
            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            spriteBatch.Draw(ball.sprite, ball.Position);
            spriteBatch.Draw(p1.sprite, p1.Position);
            spriteBatch.Draw(p2.sprite, p2.Position);
            //DrawLives(p1);
            //DrawLives(p2);
            spriteBatch.End();
        }

        public void DrawLives(Player player)
        {
            byte lives = player.Life;
            byte i;
            for (i = 1; i <= lives; i++)
            {

            }
        }
    }
    
   
  
}

