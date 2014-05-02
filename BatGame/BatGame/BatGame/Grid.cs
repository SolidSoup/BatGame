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
                    grid[i, j] = new QuadTangle(i * 32, j * 32, 32, 32, false, false, false, false, new Point(i,j));
                }
            }
        }

        /// <summary>
        /// finds out the pixel position at the position given
        /// </summary>
        /// <param name="pos">a coordiante position in the grid</param>
        /// <param name="sub">the sub square that the position is at</param>
        /// <returns>returns pixel position of the quadtangle at the position</returns>
        public Point getPosition(Point pos, SubSquares sub)
        {
            Point p;
            if (sub == SubSquares.TopLeft)
            {
                p = new Point(grid[pos.X, pos.Y].Rect.X, grid[pos.X, pos.Y].Rect.Y);
            }
            else if (sub == SubSquares.TopRight)
            {
                p = new Point(grid[pos.X, pos.Y].TRight.X, grid[pos.X, pos.Y].TRight.Y);
            }
            else if (sub == SubSquares.BottomLeft)
            {
                p = new Point(grid[pos.X, pos.Y].BLeft.X, grid[pos.X, pos.Y].BLeft.Y);
            }
            else
            {
                p = new Point(grid[pos.X, pos.Y].BRight.X, grid[pos.X, pos.Y].BRight.Y);
            }
            return p;
        }

        /// <summary>
        /// figures out where you will be stepping when you move into another square
        /// </summary>
        /// <param name="dir">direction you are facing</param>
        /// <param name="rect">the quadtangle that you are in</param>
        /// <param name="sub">the subsquare of the quadtangle that you are in</param>
        /// <returns>a point of where you will be when you move</returns>
        public Point getNextSquare(Direction dir, QuadTangle rect, SubSquares sub, GameObject go)
        {
            if (dir == Direction.Right)
            {
                if (sub == SubSquares.TopLeft)
                {
                    go.MiniSquare = SubSquares.TopRight;
                    return rect.TRight.Location;
                }
                else if(sub == SubSquares.BottomLeft)
                {
                    go.MiniSquare = SubSquares.BottomRight;
                    return rect.BRight.Location;
                }
                else if(sub == SubSquares.TopRight)
                {
                    go.MiniSquare = SubSquares.TopLeft;
                    return this.grid[rect.LocInGrid.X + 1, rect.LocInGrid.Y].TLeft.Location;
                }
                else
                {
                    go.MiniSquare = SubSquares.BottomLeft;
                    return this.grid[rect.LocInGrid.X + 1, rect.LocInGrid.Y].BLeft.Location;
                }
            }
            else if(dir == Direction.UpRight)
            {
                if (sub == SubSquares.TopLeft)
                {
                    go.MiniSquare = SubSquares.BottomRight;
                    return this.grid[rect.LocInGrid.X, rect.LocInGrid.Y-1].BRight.Location;
                }
                else if (sub == SubSquares.BottomLeft)
                {
                    go.MiniSquare = SubSquares.TopRight;
                    return rect.TRight.Location;
                }
                else if (sub == SubSquares.TopRight)
                {
                    go.MiniSquare = SubSquares.BottomLeft;
                    return this.grid[rect.LocInGrid.X + 1, rect.LocInGrid.Y-1].BLeft.Location;
                }
                else
                {
                    go.MiniSquare = SubSquares.TopLeft;
                    return this.grid[rect.LocInGrid.X + 1, rect.LocInGrid.Y].TLeft.Location;
                }
            }
            else if (dir == Direction.Up)
            {
                if (sub == SubSquares.TopLeft)
                {
                    go.MiniSquare = SubSquares.BottomLeft;
                    return this.grid[rect.LocInGrid.X, rect.LocInGrid.Y-1].BLeft.Location;
                }
                else if (sub == SubSquares.BottomLeft)
                {
                    go.MiniSquare = SubSquares.TopLeft;
                    return rect.TLeft.Location;
                }
                else if (sub == SubSquares.TopRight)
                {
                    go.MiniSquare = SubSquares.BottomRight;
                    return this.grid[rect.LocInGrid.X, rect.LocInGrid.Y-1].BRight.Location;
                }
                else
                {
                    go.MiniSquare = SubSquares.TopRight;
                    return rect.TRight.Location;
                }
            }
            else if (dir == Direction.UpLeft)
            {
                if (sub == SubSquares.TopLeft)
                {
                    go.MiniSquare = SubSquares.BottomRight;
                    return this.grid[rect.LocInGrid.X - 1, rect.LocInGrid.Y-1].BRight.Location;
                }
                else if (sub == SubSquares.BottomLeft)
                {
                    go.MiniSquare = SubSquares.TopRight;
                    return this.grid[rect.LocInGrid.X - 1, rect.LocInGrid.Y].TRight.Location;
                }
                else if (sub == SubSquares.TopRight)
                {
                    go.MiniSquare = SubSquares.BottomLeft;
                    return this.grid[rect.LocInGrid.X, rect.LocInGrid.Y-1].BLeft.Location;
                }
                else
                {
                    go.MiniSquare = SubSquares.TopLeft;
                    return rect.TLeft.Location;
                }
            }
            else if (dir == Direction.Left)
            {
                if (sub == SubSquares.TopLeft)
                {
                    go.MiniSquare = SubSquares.TopRight;
                    return this.grid[rect.LocInGrid.X - 1, rect.LocInGrid.Y].TRight.Location;
                }
                else if (sub == SubSquares.BottomLeft)
                {
                    go.MiniSquare = SubSquares.BottomRight;
                    return this.grid[rect.LocInGrid.X - 1, rect.LocInGrid.Y].BRight.Location;
                }
                else if (sub == SubSquares.TopRight)
                {
                    go.MiniSquare = SubSquares.TopLeft;
                    return rect.TLeft.Location;
                }
                else
                {
                    go.MiniSquare = SubSquares.BottomLeft;
                    return rect.BLeft.Location;
                }
            }
            else if (dir == Direction.DownLeft)
            {
                if (sub == SubSquares.TopLeft)
                {
                    go.MiniSquare = SubSquares.BottomRight;
                    return this.grid[rect.LocInGrid.X - 1, rect.LocInGrid.Y].BRight.Location; ;
                }
                else if (sub == SubSquares.BottomLeft)
                {
                    go.MiniSquare = SubSquares.TopRight;
                    return this.grid[rect.LocInGrid.X - 1, rect.LocInGrid.Y+1].TRight.Location;
                }
                else if (sub == SubSquares.TopRight)
                {
                    go.MiniSquare = SubSquares.BottomLeft;
                    return rect.BLeft.Location;
                }
                else
                {
                    go.MiniSquare = SubSquares.TopLeft;
                    return this.grid[rect.LocInGrid.X, rect.LocInGrid.Y+1].TLeft.Location;
                }
            }
            else if (dir == Direction.Down)
            {
                if (sub == SubSquares.TopLeft)
                {
                    go.MiniSquare = SubSquares.BottomLeft;
                    return rect.BLeft.Location;
                }
                else if (sub == SubSquares.BottomLeft)
                {
                    go.MiniSquare = SubSquares.TopLeft;
                    return this.grid[rect.LocInGrid.X, rect.LocInGrid.Y+1].TLeft.Location;
                }
                else if (sub == SubSquares.TopRight)
                {
                    go.MiniSquare = SubSquares.BottomRight;
                    return rect.BRight.Location;
                }
                else
                {
                    go.MiniSquare = SubSquares.TopRight;
                    return this.grid[rect.LocInGrid.X, rect.LocInGrid.Y+1].TRight.Location;
                }
            }
            else
            {
                if (sub == SubSquares.TopLeft)
                {
                    go.MiniSquare = SubSquares.BottomRight;
                    return rect.BRight.Location;
                }
                else if (sub == SubSquares.BottomLeft)
                {
                    go.MiniSquare = SubSquares.TopRight;
                    return this.grid[rect.LocInGrid.X, rect.LocInGrid.Y+1].TRight.Location;
                }
                else if (sub == SubSquares.TopRight)
                {
                    go.MiniSquare = SubSquares.BottomLeft;
                    return this.grid[rect.LocInGrid.X + 1, rect.LocInGrid.Y].BLeft.Location;
                }
                else
                {
                    go.MiniSquare = SubSquares.TopLeft;
                    return this.grid[rect.LocInGrid.X + 1, rect.LocInGrid.Y+1].TLeft.Location;
                }
            }
        }

        /// <summary>
        /// gets the grid square that is in that location
        /// </summary>
        /// <param name="p">a grid coordinate</param>
        /// <returns>the quadtangle in that location</returns>
        public QuadTangle getGridSquare(Point p)
        {
            return grid[p.X, p.Y];
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

        public QuadTangle[,] GetGrid
        {
            get { return grid; }
        }
    }
}
