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
        int tileHeight;
        int tileWidth;
        int tileHeightCount;
        int tileWidthCount;

        public Grid(int width, int height)
        {
            grid = new Rectangle[width/32, height/32];
            this.tileHeight = 32;
            this.tileWidth = 32;
            tileHeightCount = height/32;
            tileWidthCount = width/32;
            //loops through to create the grid that has tiles that are 32x32 the size of the grid is based on the window size
            for (int i = 0; i < tileWidthCount; i++)
            {
                for (int j = 0; j < tileHeightCount; j++)
                {
                    grid[i, j] = new Rectangle(i * 32, j * 32, 32, 32);
                }
            }
        }

        public Point getPosition(Point pos)
        {
            Point p = new Point(grid[pos.X, pos.Y].X, grid[pos.X, pos.Y].Y);
            return p;
        }

        //properties
        public int TileWidth
        {
            get
            {
                return tileWidth;
            }
            set
            {
                tileWidth = value;
            }
        }

        public int TileHeight
        {
            get
            {
                return tileHeight;
            }
            set
            {
                tileHeight = value;
            }
        }

        public int TileWidthCount
        {
            get
            {
                return tileWidthCount;
            }
            set
            {
                tileWidthCount = value;
            }
        }

        public int TileHeightCount
        {
            get
            {
                return tileHeightCount;
            }
            set
            {
                tileWidthCount = value;
            }
        }
    }
}
