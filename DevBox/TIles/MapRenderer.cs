
using DevBox.Global;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Collections.Generic;
using System;

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

public void Draw(List<Point> path)
{
    int cellSize = map.GetCellSize();

    for (int x = 0; x < map.GetWidth(); x++)
    {
        for (int y = 0; y < map.GetHeight(); y++)
        {
            int tileValue = map.GetCell(x, y);
            Vector2 position = new Vector2(x * cellSize, y * cellSize);

            // Check if the current position is in the path
            if (path != null && path.Contains(new Point(x, y)))
            {
                // Draw the position with a different color (e.g., red)
                Globals.SpriteBatch.Draw((tileValue == 0 ? walkable : blocked), position, Color.Red);
                //Console.WriteLine("here");
            }
            else
            {
                // Draw the position with the default color
                Globals.SpriteBatch.Draw((tileValue == 0 ? walkable : blocked), position, Color.White);
                //Console.WriteLine("Here");
            }
        }
    }
}
    }
}