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
    class Ball : GameObject
    {
        public Ball(Pong game)
        {
            Speed = 1;
            Position = new Vector2(game.center);
        }

    }
}
