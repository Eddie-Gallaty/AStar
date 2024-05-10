using System.Numerics;
using Microsoft.Xna.Framework;

namespace DevBox.Tiles
{
    public class Node
    {
        public Point Position {get;}
        public int GCost {get; set;} //cost from this node to starting node
        public int HCost {get; set;} // Heuristic cost from this node to goal
        public int FCost => GCost + HCost; //total cost (g + h)
        public Node Parent {get; set;} //parent node in the path

        public bool IsPath {get; set;}

        public Node(int x, int y)
        {
            Position = new Point(x, y);
            GCost = int.MaxValue; // initially set to infinity
            HCost = 0;
            Parent = null;
        }
    }
}