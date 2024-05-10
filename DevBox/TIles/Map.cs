using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.CompilerServices;
using DevBox.Global;

namespace DevBox.Tiles
{
    public class Map
    {
        //public int width, height;
        private int[,] grid;
        private int cellSize;

        public Map(int width, int height, int cellSize)
        {
            grid = new int[width, height];
            this.cellSize = cellSize;
        }

        public void SetCell(int x, int y, int value)
        {
            grid[x, y] = value;
        }

        public bool IsWalkable(int x, int y)
        {
            return grid[x, y] == 0 || grid[x, y] == 0; //this assumes that 0 is a a walkable cell.
        }

        public int GetCellSize()
        {
            return cellSize;
        }

        public int GetWidth()
        {
            return grid.GetLength(0); //this will return the length for width or 'x' values
        }
        
        public int GetHeight()
        {
            return grid.GetLength(1); //this will return the length for height or 'y' values
        }

        // to access a cell
        public int GetCell(int x, int y)
        {
            return grid[x, y];
        }



        //generate a random map

        public void GenerateRandomMap()
        {
            Random random = new Random();
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    int value = 0;// random.Next(2); //this will generate either 0 or 1
                    SetCell(x, y, value);

                }
            }
        }





    }
}