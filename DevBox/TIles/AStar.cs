using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;


namespace DevBox.Tiles
{
    public class AStar
    {
        private Map map;

        public AStar(Map map)
        {
            this.map = map;
        }

        public List<Point> FindPath(Point start, Point goal)
        {
            //initialize open and closed lists
            List<Node> openList = new List<Node>();
            List<Node> closedList = new List<Node>();

            //create start and goal nodes
            Node startNode = new Node(start.X, start.Y);
            Node goalNode = new Node(goal.X, goal.Y);

            // add start node to open list
            openList.Add(startNode);

            while (openList.Count > 0)
            {
                //find the node with the lowest f cost in the open list
                Node currentNode = openList[0];
                for (int i = 1; i < openList.Count; i++)
                {
                    if (openList[i].FCost < currentNode.FCost || (openList[i].FCost == currentNode.FCost && openList[i].HCost < currentNode.HCost ))
                    {
                        //add the current node to the open list
                        currentNode = openList[i];
                    }

                }

               // Console.WriteLine($"Evaluating node at position: ({currentNode.Position.X}, {currentNode.Position.Y})");
                // move the current node from open to closed list
                openList.Remove(currentNode);
                closedList.Add(currentNode);

                //check if the goal has been reached
                if (currentNode.Position == goalNode.Position)
                {
                    //construct the path then return it

                    return ConstructPath(currentNode);
                }

                //get the neighboring nodes
                List<Node> neighbors = GetNeighbors(currentNode);

                //process the neighboring nodes
                foreach (Node neighbor in neighbors)
                {
                    //skip if neighbor is unwalkable or in a closed list
                    if (!map.IsWalkable(neighbor.Position.X, neighbor.Position.Y) || closedList.Contains(neighbor))
                    {
                        continue;
                    }

                    //calculate new costs.
                    int newGCost = currentNode.GCost + CalculateDistance(currentNode, neighbor);
                    int newHCost = CalculateDistance(neighbor, goalNode);
                    
                    //update the neighbors cost if its not in the open list or if new path is better
                    if (newGCost < neighbor.GCost || !openList.Contains(neighbor))
                    {
                        neighbor.GCost = newGCost;
                        neighbor.HCost = newHCost;
                        neighbor.Parent = currentNode;

                        if ( !openList.Contains(neighbor))
                        {
                            openList.Add(neighbor);
                        }
                    }
                }
            }
            //no path found
            return null;
        }

        private List<Node> GetNeighbors(Node node)
        {
            List<Node> neighbors = new List<Node>();

            //define offsets for neighboring nodes  

            Point[] offsets = new Point[]
            {
                new Point(1, 0), //right
                new Point(-1, 0), //left
                new Point(0, 1), //down
                new Point(0, -1) //up

            };

            //check each offset to determine neighboring nodes
            foreach (Point offset in offsets)
            {
                Point neighborPos = new Point(node.Position.X + offset.X, node.Position.Y + offset.Y);
                //check if neighbor position is within the map bounds

                if(neighborPos.X >= 0 && neighborPos.X < map.GetWidth() &&
                   neighborPos.Y >= 0 && neighborPos.Y <map.GetHeight())
                {
                    //create a new node for the neighbor
                    Node  neighborNode = new Node(neighborPos.X, neighborPos.Y);
                    neighbors.Add(neighborNode);
                }

            }
            return neighbors;
        }
        
        private int CalculateDistance(Node from, Node to)
        {
            //below im calculating Manhattan distance between two nodes
            return Math.Abs(from.Position.X - to.Position.X) + Math.Abs(from.Position.Y - to.Position.Y);

        }

        private List<Point> ConstructPath(Node node)
        {
            List<Point> path = new List<Point>();

            //constructing path by following parent nodes from end to start
            while (node != null)
            {
                path.Add(node.Position);
                node = node.Parent;
            }
            //reverse the path to get it in the correct order which is start to end
            path.Reverse();

            return path;


        }
    }
}