using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Pong
{
    class Player
    {
        public Texture2D sprite;
        public float Speed { get; protected set; }
        public Vector2 Position;
        public Vector2 Size;

        public Player p1, p2;

        public string direction;
        public byte Life;
        private const byte maxlife = 3;

        public bool isAlive
        {
            get
            {
                if (Life == 0) return false;
                else return true;
            }
        }

        public void reset()
        {
            this.Life = maxlife;
            this.Position = new Vector2(0, 0);
        }
        public void lose() { this.Life--; }

        public void move(string direction)
        {
            float newPos;
            float deepestPoint;
            if (direction == "down")
            {
                newPos = this.Position.Y - this.Speed;
                if (newPos >= Pong.WindowSize.Y)// check if player within bounds
                {
                    this.Position.Y = newPos;
                }
                else Console.Beep();
            }
            else if (direction == "up")
            {
                newPos = this.Position.Y + this.Speed;
                deepestPoint = newPos + this.Size.Y;
                if (deepestPoint <= Pong.WindowSize.Y)// check if player within bounds
                {
                    this.Position.Y = newPos;
                }
                else Console.Beep();
            }
            else Console.Beep();
        }

        public Player()
        {
           
            this.Speed = 10.0f;
            this.Position = new Vector2(0, 0);

            p1.Position = new Vector2(Pong.WindowSize.X - p1.Size.X, Pong.WindowSize.Y / 2 - p1.Size.Y / 2);
            p1.sprite = Pong.content.Load<Texture2D>("rodeSpeler");

            p2.Position = new Vector2(0, Pong.WindowSize.Y / 2 - p2.Size.Y / 2);
            p2.sprite = Pong.content.Load<Texture2D>("blauweSpeler");

            /* p1 =
             * {
             *      this.Position = new Vector2(Pong.WindowSize.X - p1.Size.X, Pong.WindowSize.Y / 2 - p1.Size.Y / 2);
             *      this.sprite = Pong.content.Load<Texture2D>("rodeSpeler");
             * }
             * p2 = 
             * {
             *      this.Position = new Vector2(0, Pong.WindowSize.Y / 2 - p2.Size.Y / 2);
             *      this.sprite = Pong.content.Load<Texture2D>("blauweSpeler");
             * }
             */
        }

    }
}