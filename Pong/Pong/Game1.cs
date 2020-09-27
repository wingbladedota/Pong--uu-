using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;


namespace Pong
{
    class Pong : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Player p1;
        public Player p2;
        public Ball ball;


        public Color Background { get; private set; }
        public Vector2 WindowSize;
        public Vector2 center;



        public SoundEffect errorSound;

        [STAThread]
        static void Main()
        {
            Pong game = new Pong();
            game.Run();

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
            this.p2.sprite = Content.Load<Texture2D>("blauweSpeler");
            this.p1.sprite = Content.Load<Texture2D>("rodeSpeler");
            this.ball.sprite = Content.Load<Texture2D>("bal");
            this.ball.Position = center;
            this.p2.Position = new Vector2(0, WindowSize.Y / 2 - p2.Size.Y / 2);
            this.p1.Position = new Vector2(WindowSize.X - p1.Size.X, WindowSize.Y / 2 - p1.Size.Y / 2);

        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            bool movingError = false;

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                movingError = p1.move("up");
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                movingError = p1.move("down");
            }
            if (keyboardState.IsKeyDown(Keys.W))
            {
                movingError = p2.move("up");
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                movingError = p2.move("down");
            }
            if (movingError) { Console.Beep(); }
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

