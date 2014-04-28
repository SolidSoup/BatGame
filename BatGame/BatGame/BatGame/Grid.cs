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
        QuadTangle[,] grid;
        int tileHeight;
        int tileWidth;
        int tileHeightCount;
        int tileWidthCount;

        public Grid(int width, int height)
        {
            grid = new QuadTangle[width/32, height/32];
            this.tileHeight = 32;
            this.tileWidth = 32;
            tileHeightCount = height/32;
            tileWidthCount = width/32;
            //loops through to create the grid that has tiles that are 32x32 the size of the grid is based on the window size
            for (int i = 0; i < tileWidthCount; i++)
            {
                for (int j = 0; j < tileHeightCount; j++)
                {
                    grid[i, j] = new QuadTangle(i * 32, j * 32, 32, 32, false, false, false, false);
                }
            }
        }

        public Point getPosition(Point pos)
        {
            Point p = new Point(grid[pos.X, pos.Y].Rect.X, grid[pos.X, pos.Y].Rect.Y);
            return p;
        }

        //properties
        /// <summary>
        /// how wide each tile is
        /// </summary>
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

        /// <summary>
        /// how tall each tile is
        /// </summary>
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

        /// <summary>
        /// how many tiles are in the grid in each row
        /// </summary>
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

        /// <summary>
        /// how many tiles are in the grid in each column
        /// </summary>
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
