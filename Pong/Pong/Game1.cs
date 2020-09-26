using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class BasicGame : Game
{
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch; 
    Texture2D blauweSpeler, rodeSpeler, bal;
    Vector2 ballPosition, blauweSpelerPosition, rodeSpelerPosition;
    float paddleSpeed=10;
    [STAThread]
    static void Main()
    {
        BasicGame game = new BasicGame();
        game.Run();
    }

    public BasicGame()
    {
        Content.RootDirectory = "Content";
        graphics = new GraphicsDeviceManager(this);
  
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        blauweSpeler = Content.Load<Texture2D>("blauweSpeler");
        rodeSpeler = Content.Load<Texture2D>("rodeSpeler");
        bal = Content.Load<Texture2D>("bal");
        ballPosition = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
        blauweSpelerPosition = new Vector2(0, graphics.PreferredBackBufferHeight / 2-blauweSpeler.Height/2);
        rodeSpelerPosition = new Vector2(graphics.PreferredBackBufferWidth - rodeSpeler.Width, graphics.PreferredBackBufferHeight / 2 - rodeSpeler.Height / 2);
    }

    protected override void Update(GameTime gameTime)
    {
        KeyboardState keyboardState = Keyboard.GetState();
        if (rodeSpelerPosition.Y + rodeSpeler.Height < graphics.PreferredBackBufferHeight)
        {
            if (keyboardState.IsKeyDown(Keys.L))
            {
                rodeSpelerPosition.Y += paddleSpeed;
            } 
        }
        if (rodeSpelerPosition.Y > 0)
        {
            if (keyboardState.IsKeyDown(Keys.O))
            {
                rodeSpelerPosition.Y -= paddleSpeed;
            }
        }
        if (blauweSpelerPosition.Y > 0)
        {
            if (keyboardState.IsKeyDown(Keys.Q))
            {
                blauweSpelerPosition.Y -= paddleSpeed;
            }
        }
        if (blauweSpelerPosition.Y + blauweSpeler.Height < graphics.PreferredBackBufferHeight)
        {
            if (keyboardState.IsKeyDown(Keys.A))
            {
                blauweSpelerPosition.Y += paddleSpeed;
            }
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White); 
        spriteBatch.Begin();
        spriteBatch.Draw(bal, ballPosition);
        spriteBatch.Draw(rodeSpeler, rodeSpelerPosition);
        spriteBatch.Draw(blauweSpeler, blauweSpelerPosition);
        spriteBatch.End();
    }
}