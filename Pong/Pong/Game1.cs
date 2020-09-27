using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class BasicGame : Game
{
    //sorry voor de magic numbers, maar dit soort opdrachten zijn gewoon veel sneller met wat hardcode
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    Texture2D blauweSpeler, rodeSpeler, bal;
    Vector2 ballPosition, blauweSpelerPosition, rodeSpelerPosition, ballSpeed;
    float paddleSpeed = 10;
    //bandaid way of generating a random decimal instead of integer
    float totalBallSpeed = 500;
    float actualTotalBallSpeed;
    Random random;
    int randomMod;
    // GameState
    int GameState;// Start = -1, Playing = 0, GameOver = 1
    
    int ballOffset = 3;

    int rodespelerlevens = 3;
    int blauwespelerlevens = 3;

    string mesStart = "Press (SPACE) to continue";//message to display at start of the game
    string mesGameOver = "GAME OVER";//message to display at end of the game
    string mesHasWon = " has defeated his opponent";//win message
    string mesPlayer; // var to store the winning player
    Color mesColor;


    SpriteFont font; // store the font to be used for messages


    [STAThread]
    static void Main()
    {
        BasicGame game = new BasicGame();
        game.Run();
    }

    public BasicGame()
    {
        actualTotalBallSpeed = totalBallSpeed / 100;
        Content.RootDirectory = "Content";
        graphics = new GraphicsDeviceManager(this);


    }

    protected override void LoadContent()
    {
        GameState = -1;
        spriteBatch = new SpriteBatch(GraphicsDevice);
        blauweSpeler = Content.Load<Texture2D>("blauweSpeler");
        rodeSpeler = Content.Load<Texture2D>("rodeSpeler");
        bal = Content.Load<Texture2D>("bal");
        font = Content.Load<SpriteFont>("gameover");
        BallSetup();
    }

    protected void BallSetup()
    {
        ballPosition = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
        blauweSpelerPosition = new Vector2(0, graphics.PreferredBackBufferHeight / 2 - blauweSpeler.Height / 2);
        rodeSpelerPosition = new Vector2(graphics.PreferredBackBufferWidth - rodeSpeler.Width, graphics.PreferredBackBufferHeight / 2 - rodeSpeler.Height / 2);
        random = new Random();
        randomMod = random.Next(-1000, 1000);
        ballSpeed.Y = (float)random.Next((int)-totalBallSpeed, (int)totalBallSpeed) / 150;
        if (ballSpeed.Y >= 0)
            ballSpeed.X = (actualTotalBallSpeed - ballSpeed.Y);
        if (ballSpeed.Y < 0)
            ballSpeed.X = (actualTotalBallSpeed + ballSpeed.Y);
        if (randomMod <= 0)
        {
            ballSpeed.X *= -1;
        }
        
    }

    protected override void Update(GameTime gameTime)
    {

        ballPosition += ballSpeed;
        KeyboardState keyboardState = Keyboard.GetState();



        //input/////////////////////////////////////////////////////////////////////////////
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
        //collision//////////////////////////////////////////////////////////////////////////////
        if (ballPosition.Y < 0)
        {
            ballPosition.Y = 0;
            ballSpeed.Y *= -1;
        }
        if (ballPosition.Y > graphics.PreferredBackBufferHeight - bal.Height)
        {
            ballPosition.Y = graphics.PreferredBackBufferHeight - bal.Height;
            ballSpeed.Y *= -1;
        }
        if (ballPosition.X < 0)
        {
            //GAME OVER STATE ALS LEVENS OP ZIJN
            blauwespelerlevens--;
            BallSetup();


        }
        if (ballPosition.X > graphics.PreferredBackBufferWidth - bal.Width)
        {
            //GAME OVER STATE
            rodespelerlevens--;
            BallSetup();

        }

        if (ballPosition.X + bal.Width > rodeSpelerPosition.X)
        {
            if (ballPosition.Y + bal.Height > rodeSpelerPosition.Y)
            {
                if (ballPosition.Y < rodeSpelerPosition.Y + rodeSpeler.Height / 3)
                {
                    ballPosition.X = rodeSpelerPosition.X - bal.Width;
                    ballSpeed.X *= -1.1f;
                    ballSpeed.Y -= ballOffset;
                }
                else if (ballPosition.Y < rodeSpelerPosition.Y + rodeSpeler.Height / 3 * 2)
                {
                    ballPosition.X = rodeSpelerPosition.X - bal.Width;
                    ballSpeed.X *= -1.1f;

                }
                else if (ballPosition.Y < rodeSpelerPosition.Y + rodeSpeler.Height)
                {
                    ballPosition.X = rodeSpelerPosition.X - bal.Width;
                    ballSpeed.X *= -1.1f;
                    ballSpeed.Y += ballOffset;
                }
            }

        }
        if (ballPosition.X < blauweSpelerPosition.X + blauweSpeler.Width)
        {
            if (ballPosition.Y + bal.Height > blauweSpelerPosition.Y)
            {
                if (ballPosition.Y < blauweSpelerPosition.Y + blauweSpeler.Height / 3)
                {
                    ballPosition.X = blauweSpelerPosition.X + blauweSpeler.Width;
                    ballSpeed.X *= -1.1f;
                    ballSpeed.Y -= ballOffset;
                }
                else if (ballPosition.Y < blauweSpelerPosition.Y + blauweSpeler.Height / 3 * 2)
                {
                    ballPosition.X = blauweSpelerPosition.X + blauweSpeler.Width;
                    ballSpeed.X *= -1.1f;

                }
                else if (ballPosition.Y < blauweSpelerPosition.Y + blauweSpeler.Height)
                {
                    ballPosition.X = blauweSpelerPosition.X + blauweSpeler.Width;
                    ballSpeed.X *= -1.1f;
                    ballSpeed.Y += ballOffset;
                }
            }

        }

        if (ballPosition.X < blauweSpelerPosition.X + blauweSpeler.Width &&
            ballPosition.Y + bal.Height > blauweSpelerPosition.Y &&
            ballPosition.Y < blauweSpelerPosition.Y + blauweSpeler.Height)
        {
            ballPosition.X = blauweSpelerPosition.X + blauweSpeler.Width;
            ballSpeed.X *= -1.1f;

        }
        if (rodespelerlevens < 0 || blauwespelerlevens < 0)
        {
            GameState = 1;
        }

        if (keyboardState.IsKeyDown(Keys.Space))
        {
            GameState = 0;
            rodespelerlevens = 3;
            blauwespelerlevens = 3;
            BallSetup();
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        Vector2 startmesloc = new Vector2(0,0);//the location to draw the text of GameState-1
        Vector2 gameovermesloc = new Vector2(0, graphics.PreferredBackBufferHeight / 2 - 20);//the location to draw the "GameOver" of GameState1
        Vector2 winmesloc = new Vector2(0, graphics.PreferredBackBufferHeight / 2);//the location to draw the winmessage of GameState1
        Vector2 nextbluelifepos = new Vector2(0, 0);
        Vector2 nextredlifepos = new Vector2(graphics.PreferredBackBufferWidth - ballwidth, 0);


        if (GameState == 0) // Draw the PLAYING-state
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();

            // draw the gameobjects at their respective positions
            spriteBatch.Draw(bal, ballPosition);
            spriteBatch.Draw(rodeSpeler, rodeSpelerPosition);
            spriteBatch.Draw(blauweSpeler, blauweSpelerPosition);
            
            
            float ballwidth = bal.Width;//store the width of the ball to be able to draw the lives next to each other
            for (int i = 0; i < blauwespelerlevens; i++) //go through the number of lives of blue
            {
                spriteBatch.Draw(bal, nextbluelifepos); //draw the next life at it's position
                nextbluelifepos = new Vector2(nextbluelifepos.X + ballwidth, 0); // calculate the next position

            }
            
            for (int i = 0; i < rodespelerlevens; i++)
            {
                spriteBatch.Draw(bal, nextredlifepos);
                nextredlifepos = new Vector2(nextredlifepos.X - ballwidth, 0); //calculate the next position in reverse X
            }

            spriteBatch.End();
        }

        if (GameState == -1) //draw the BEGINstate
        {
            Vector2 Startmessageloc = new Vector2();
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            spriteBatch.DrawString(font, mesStart, startmesloc, Color.Black);
            spriteBatch.End();
        }
        if (GameState == 1) //draw the GAMEOVER-state
        {
            GraphicsDevice.Clear(Color.Black);
            
            spriteBatch.Begin();

            spriteBatch.DrawString(font, "GAME OVER", gameovermesloc, Color.White);

            if (rodespelerlevens > blauwespelerlevens){// who has won; set values accordingly
                string mesPlayer = "red";
                Color mesColor = Color.Red;
            }else{
                string mesPlayer = "red";
                Color mesColor = Color.Red;
            }
            string output = mesPlayer + mesHasWon; // put together the final message
            spriteBatch.DrawString(font, output, winmesloc, mesColor); // draw the haswon message on screen
            spriteBatch.End();
            
        }
    }
}