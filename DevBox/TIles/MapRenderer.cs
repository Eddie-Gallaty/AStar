
using DevBox.Global;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.Diagnostics.Tracing;

namespace DevBox.Tiles
{
    public class MapRenderer    
    {
        private Texture2D walkable; // for walkable sprites
        private Texture2D blocked; //for non-walkable sprites
        private Map map;

        public MapRenderer(Texture2D walkable, Texture2D blocked, Map map)
        {
            this.walkable = walkable;
            this.blocked = blocked;
            this.map = map;
        }

        public void Draw()
        {
            int cellSize = map.GetCellSize();
            //map.GenerateRandomMap();
            for (int x = 0; x < map.GetWidth(); x++)
            {
                for (int y = 0; y < map.GetHeight(); y++)
                {
                    int tileValue = map.GetCell(x, y);
                    //Debug.WriteLine(tileValue);
                    Vector2 position = new Vector2(x * cellSize, y * cellSize);

                    //need to add something like  the below once ready:
                     Texture2D textureToDraw = tileValue == 0 ? walkable : blocked;

                    Globals.SpriteBatch.Draw(textureToDraw, position, Color.White);
                }
            }
        }
    }
}