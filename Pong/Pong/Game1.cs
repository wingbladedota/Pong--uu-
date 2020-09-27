using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong;

class BasicGame : Game
{
    //Als de game groter was, dan had ik een gameobject en spritegameobject class gemaakt waarvan ik de spelers, ballen, en levens zou laten inheriten, maar het maken van die classes
    //kost meer tijd dan het maken van de hele game op deze manier.
    //sorry voor de magic numbers, maar dit soort opdrachten zijn gewoon veel sneller met wat hardcode
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    Texture2D blauweSpeler, rodeSpeler, bal;
    Vector2 ballPosition, blauweSpelerPosition, rodeSpelerPosition, ballSpeed;
    float paddleSpeed = 10; //snelheid van de paddles
    float totalBallSpeed = 500;    //bandaid way of generating a random decimal instead of integer
    float actualTotalBallSpeed; 
    Random random;
    int randomMod; //zodat de snelheid zowel positief als negatief kan zijn
    int GameState;// Start = -1, Playing = 0, GameOver = 1

    int ballOffset = 3;//de y-snelheid die je de bal meegeeft als je hem op het randje van je paddle raakt

    int rodespelerlevens;
    int blauwespelerlevens;

    int spelerlevens = 3;

    string mesStart = "Press (SPACE) to continue";//message to display at start of the game
    string mesGameOver = "GAME OVER";//message to display at end of the game
    string mesHasWon = " has defeated it's opponent";//win message
    string mesPlayer; // var to store the winning player
    string outmes; // outputmessage of winning player
    Color mesColor;// messageColor of winning player


    float ballwidth;//store the width of the ball to be able to draw the lives next to each other
    Vector2 startmesloc;//the location to draw the text of GameState-1
    Vector2 gameovermesloc;//the location to draw the "GameOver" of GameState1
    Vector2 winmesloc;//the location to draw the winmessage of GameState1
    Vector2 nextbluelifepos;//location of the next life of blue
    Vector2 nextredlifepos;//location of the next life of red


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
        BallSetup();//de functie die de positie van de paddles, bal, en de richting van de bal geeft. Dat de peddles ook resetten is bewust gedaan
    }

    protected void BallSetup()
    {
        ballPosition = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
        blauweSpelerPosition = new Vector2(0, graphics.PreferredBackBufferHeight / 2 - blauweSpeler.Height / 2);
        rodeSpelerPosition = new Vector2(graphics.PreferredBackBufferWidth - rodeSpeler.Width, graphics.PreferredBackBufferHeight / 2 - rodeSpeler.Height / 2);
        random = new Random();
        randomMod = random.Next(-1000, 1000);
        ballSpeed.Y = (float)random.Next((int)-totalBallSpeed, (int)totalBallSpeed) / 150; //de Y heeft een random snelheid, en de x krijgt "wat over is" en dan random of het positief of
        //negatief is
        if (ballSpeed.Y >= 0)
            ballSpeed.X = (actualTotalBallSpeed - ballSpeed.Y);
        if (ballSpeed.Y < 0)
            ballSpeed.X = (actualTotalBallSpeed + ballSpeed.Y);
        if (randomMod <= 0)
        {
            ballSpeed.X *= -1;
        }

    }

    public Vector2 stringsize(SpriteFont font, string text)
    {
        Vector2 size = font.MeasureString(text);
        return size;
    }


    protected override void Update(GameTime gameTime)
    {

        ballPosition += ballSpeed;
        KeyboardState keyboardState = Keyboard.GetState();

        

        ballwidth = bal.Width;//store the width of the ball to be able to draw the lives next to each other
        startmesloc = new Vector2(
            (graphics.PreferredBackBufferWidth - stringsize(font, mesStart).X) / 2,
            (graphics.PreferredBackBufferHeight - stringsize(font, mesStart).Y) / 2);//the location to draw the text of GameState-1
        gameovermesloc = new Vector2(
            (graphics.PreferredBackBufferWidth - stringsize(font, mesGameOver).X) / 2,
            graphics.PreferredBackBufferHeight / 2 - stringsize(font, mesGameOver).Y);//the location to draw the "GameOver" of GameState1
        
        
        
        nextbluelifepos = new Vector2(0, 0);//location of the next life of blue
        nextredlifepos = new Vector2(graphics.PreferredBackBufferWidth - ballwidth, 0);//location of the next life of red

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
            blauwespelerlevens--;
            BallSetup();


        }
        if (ballPosition.X > graphics.PreferredBackBufferWidth - bal.Width)
        {
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
                    ballSpeed.X *= -1.1f;//balletje gaat sneller met elke bounce
                    ballSpeed.Y -= ballOffset;
                }
                else if (ballPosition.Y < rodeSpelerPosition.Y + rodeSpeler.Height / 3 * 2)
                {
                    ballPosition.X = rodeSpelerPosition.X - bal.Width;
                    ballSpeed.X *= -1.1f;//balletje gaat sneller met elke bounce

                }
                else if (ballPosition.Y < rodeSpelerPosition.Y + rodeSpeler.Height)
                {
                    ballPosition.X = rodeSpelerPosition.X - bal.Width;
                    ballSpeed.X *= -1.1f;//balletje gaat sneller met elke bounce
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
                    ballSpeed.X *= -1.1f;//balletje gaat sneller met elke bounce
                    ballSpeed.Y -= ballOffset;
                }
                else if (ballPosition.Y < blauweSpelerPosition.Y + blauweSpeler.Height / 3 * 2)
                {
                    ballPosition.X = blauweSpelerPosition.X + blauweSpeler.Width;
                    ballSpeed.X *= -1.1f;//balletje gaat sneller met elke bounce

                }
                else if (ballPosition.Y < blauweSpelerPosition.Y + blauweSpeler.Height)
                {
                    ballPosition.X = blauweSpelerPosition.X + blauweSpeler.Width;
                    ballSpeed.X *= -1.1f;//balletje gaat sneller met elke bounce
                    ballSpeed.Y += ballOffset;
                }
            }

        }

        if (ballPosition.X < blauweSpelerPosition.X + blauweSpeler.Width &&
            ballPosition.Y + bal.Height > blauweSpelerPosition.Y &&
            ballPosition.Y < blauweSpelerPosition.Y + blauweSpeler.Height)
        {
            ballPosition.X = blauweSpelerPosition.X + blauweSpeler.Width;
            ballSpeed.X *= -1.1f;//balletje gaat sneller met elke bounce

        }
        if (rodespelerlevens < 0 || blauwespelerlevens < 0)
        {
            GameState = 1;
        }

        if (keyboardState.IsKeyDown(Keys.Space))//when space is pressed
        {
            GameState = 0;//set PLAYING-state
            rodespelerlevens = spelerlevens;// reset red lives
            blauwespelerlevens = spelerlevens;// reset blue lives
            BallSetup();// reset the ball
        }
        if (GameState == 1)
        {
            if (rodespelerlevens > blauwespelerlevens)// who has won?; set values accordingly
            {
                mesPlayer = "RED";
                mesColor = Color.Red;
            }
            else
            {
                mesPlayer = "BLUE";
                mesColor = Color.Blue;
            }
            outmes = mesPlayer + mesHasWon; // put together the final message
            winmesloc = new Vector2(
                (graphics.PreferredBackBufferWidth - stringsize(font, outmes).X) / 2,
                graphics.PreferredBackBufferHeight / 2);//the location to draw the winmessage of GameState1
        }
        if (GameState != 0)// don't move the ball if not playing
        {
            ballPosition = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        if (GameState == -1) //draw the BEGINstate
        {
            
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            spriteBatch.DrawString(font, mesStart, startmesloc, Color.Black);
            spriteBatch.End();
        }
        if (GameState == 0) // Draw the PLAYING-state
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();

            // draw the gameobjects at their respective positions
            spriteBatch.Draw(bal, ballPosition);
            spriteBatch.Draw(rodeSpeler, rodeSpelerPosition);
            spriteBatch.Draw(blauweSpeler, blauweSpelerPosition);

            for (int i = 0; i < blauwespelerlevens; i++) //go through the number of lives of blue
            {
                spriteBatch.Draw(bal, nextbluelifepos); //draw the next life at it's position

                // theoretically you could split this to update using an array but that wouldn't be quicker
                nextbluelifepos = new Vector2(nextbluelifepos.X + ballwidth, 0); // calculate the next position
            }
            for (int i = 0; i < rodespelerlevens; i++)
            {
                spriteBatch.Draw(bal, nextredlifepos);
                nextredlifepos = new Vector2(nextredlifepos.X - ballwidth, 0); //calculate the next position in reverse X
            }
            spriteBatch.End();
        }

        if (GameState == 1) //draw the GAMEOVER-state
        {
            GraphicsDevice.Clear(Color.Black);//make screen black

            spriteBatch.Begin();
            spriteBatch.DrawString(font, "GAME OVER", gameovermesloc, Color.White);
            spriteBatch.DrawString(font, outmes, winmesloc, mesColor); // draw the haswon message on screen
            spriteBatch.End();
        }
    }

    //hello
}