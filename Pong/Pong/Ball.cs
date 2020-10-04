using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Pong
{
    class Ball
    {

        public Texture2D sprite;
        public float Speed { get; protected set; }
        public Vector2 Position;
        public Vector2 Size;

        public Ball ball;


        public Ball()
        {
            ball.sprite = Pong.content.Load<Texture2D>("bal");
            Speed = 1;
        }

    }
}