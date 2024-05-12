using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DevBox.Global;

namespace DevBox.Sprites
{
    public class Sprite
    {
        public Texture2D Texture;
        public Vector2 Position;
        public Vector2 Origin;

        public Sprite(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;    
        }

        public virtual void Update()
        {

        }

        public virtual void Draw()
        {
            Globals.SpriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}