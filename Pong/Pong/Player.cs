﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Pong
{
    class Player : GameObject
    {
        public Vector2 Size { get; set; }

        public enum direction { up, down }


        public byte Life
        { get; set { if (value <= 255) Life = value; } }

        public bool isAlive
        {
            get
            {
                if (Life == 0) return false;
                else return true;
            }
            private set;
        }

        public void reset()
        {
            this.Life = 3;
            this.Position = new Vector2(0, 0);
        }
        public void lose() { this.Life--; }

        public bool move(direction)
        {
            float newPos;
            float deepestPoint;
            if (direction == down)
            {
                newPos = this.Position.Y - this.Speed;
                if (newPos >= windowSize.Y)// check if player within bounds
                {
                    this.Position.Y = newPos;
                    return true;
                }
                else return false;
            }
            else if (direction == up)
            {
                newPos = this.Position.Y + this.Speed;
                deepestPoint = newPos + this.Size.Y;
                if (deepestPoint <= windowSize.Y)// check if player within bounds
                {
                    this.Position.Y = newPos;
                    return true;
                }
                else return false;
            }
            else return false;
        }

        public Player()
        {
            this.Speed = 10.0f;
            this.Position = new Vector2(0, 0);

        }
    }

}