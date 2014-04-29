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
    class Mobiles : GameObject 
    {
        private double speed;
        bool isActive;
        double moveTime;

        /// <summary>
        /// checks to make sure that you are still in the grid
        /// </summary>
        /// <param name="dir">the direction you are facing</param>
        /// <returns>true or false as to whether you can move in your current direction</returns>
        public bool canMove(Direction dir)
        {
            if (dir == Direction.Up)
            {
                if (this.PosY - 1 <= -1)
                {
                    return false;
                }
            }
            else if (dir == Direction.UpLeft)
            {
                if (this.PosY - 1 <= -1 || this.PosX - 1 <= -1)
                {
                    return false;
                }
            }
            else if (dir == Direction.Left)
            {
                if (this.PosX - 1 <= -1)
                {
                    return false;
                }
            }
            else if (dir == Direction.DownLeft)
            {
                if (this.PosY + 1 >= GridPos.TileHeightCount || this.PosX - 1 <= -1)
                {
                    return false;
                }
            }
            else if (dir == Direction.Down)
            {
                if (this.PosY + 1 >= GridPos.TileHeightCount)
                {
                    return false;
                }
            }
            else if (dir == Direction.DownRight)
            {
                if (this.PosY + 1 >= GridPos.TileHeightCount || this.PosX + 1 >= GridPos.TileWidthCount)
                {
                    return false;
                }
            }
            else if (dir == Direction.Right)
            {
                if (this.PosX + 1 >= GridPos.TileWidthCount)
                {
                    return false;
                }
            }
            else
            {
                if (this.PosY - 1 <= -1 || this.PosX + 1 >= GridPos.TileWidthCount)
                {
                    return false;
                }

            }
            return true;
        }

        /// <summary>
        /// moves the object in the direction given
        /// </summary>
        /// <param name="dir">the direction you want to go</param>
        /// <param name="sub">the top left sub square that you occupy</param>
        public void Move(Direction dir, SubSquares sub)
        {
                RectX = GridPos.getNextSquare(dir, GridPos.getGridSquare(Position), sub).X;
                RectY = GridPos.getNextSquare(dir, GridPos.getGridSquare(Position), sub).Y;
        }

        public double Speed
        {
            get { return this.speed; }
            set { this.speed = value; }
        }

        public bool IsActive
        {
            get { return this.isActive; }
            set { this.isActive = value; }
        }

        public double MoveTime
        {
            get
            {
                return moveTime;
            }
            set
            {
                moveTime = value;
            }
        }

        //instantiate for projectiles
        public Mobiles(Texture2D t, GameObjectManager go, Point p, Grid g, Direction d, SubSquares sub, bool s, double speed, double m, bool a)
            : base(t, go, p, g, d, sub, s)
        {
            this.speed = speed;
            this.moveTime = m;
            this.isActive = a;
        }
    }
}
