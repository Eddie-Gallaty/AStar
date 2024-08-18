using Microsoft.Xna.Framework.Graphics;
using DevBox.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DevBox.Sprites
{
    public class Enemy : Sprite
    {
        public Vector2 Velocity;
        public float Speed;
   
        public Enemy(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Speed = 3;
            Origin = new Vector2(texture.Width / 2, texture.Height /2);
        }

        public  void Update()
        {
            Position += Velocity;
        }
    }
}