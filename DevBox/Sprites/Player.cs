
using Microsoft.Xna.Framework.Graphics;
using DevBox.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DevBox.Sprites
{
    public class Player : Sprite
    {
        public Vector2 Velocity;
        public float Speed;
        public Input Input;


        //only passing speed and origin for now
        public Player(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Speed = 3;
            Origin = new Vector2(texture.Width / 2, texture.Height /2);
        }

        public override void Update()
        {
            Position += Velocity;
            Velocity = Vector2.Zero;

            if (Keyboard.GetState().IsKeyDown(Input.Right))
            {
                Velocity.X += Speed;
            }
            else if (Keyboard.GetState().IsKeyDown(Input.Left))
            {
                Velocity.X -= Speed;
            }
            if (Keyboard.GetState().IsKeyDown(Input.Up))
            {
                Velocity.Y -= Speed;
            }
            else if (Keyboard.GetState().IsKeyDown(Input.Down))
            {
                Velocity.Y += Speed;
            }
        }
    }
}