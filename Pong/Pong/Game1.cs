using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class Pong : Game
{
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch; 
    Texture2D blauweSpeler, rodeSpeler, bal;


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
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        blauweSpeler = Content.Load<Texture2D>("blauweSpeler");
        rodeSpeler = Content.Load<Texture2D>("rodeSpeler");
        bal = Content.Load<Texture2D>("bal");

    }

    protected override void Update(GameTime gameTime)
    {

    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);

    }
}
class Ball
{
    public Ball()
    {

    }

}
class Paddle
{
    public Paddle()
    {

    }
}
class Life
{
    Life p1 = new Life();
    Life p2 = new Life();

    public int count;


    public Life()
    {
        
    }
    public void reset()
    {
        this.count = 3;
    }
    public void subtract()
    {
        this.count -= lives;
    }
    
}