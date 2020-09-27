using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    class Ball : Pong
    {
        public Texture2D sprite;
        public float Speed { get; protected set; }
        public Vector2 Position;
        public Vector2 Size;
        public Ball()
        {
            Speed = 1;
        }

    }
}
