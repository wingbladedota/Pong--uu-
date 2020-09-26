using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    class GameObject
    {
        public Texture2D sprite;
        public float Speed { get; protected set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
    }
}