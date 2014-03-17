using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BatGame
{
    class Grid
    {
        //fields
        Rectangle[,] grid;
        int height;
        int width;

        public Grid(int width, int height)
        {
            grid = new Rectangle[16, 16];
            this.height = height/16;
            this.width = width/16;
            //loops through to create the grid 16x16 based on the width and height of the window
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    grid[i, j] = new Rectangle(i * (width / 16), j * (height / 16), width / 16, height / 16);
                    Console.WriteLine(grid[i, j].X + ", " + grid[i, j].Y);
                }
            }
        }

        public Point getPosition(Point pos)
        {
            Point p = new Point(grid[pos.X, pos.Y].X, grid[pos.X, pos.Y].Y);
            return p;
        }

        //properties
        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }

        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }

        /*public int X
        {
            get
            {
                return 
            }
        }*/
    }
}
