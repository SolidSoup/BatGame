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
                if (this.PosY - 1 <= -1 && !HalfY)
                {
                    return false;
                }
            }
            else if (dir == Direction.UpLeft)
            {
                if ((this.PosY - 1 <= -1 && !HalfY) || (this.PosX - 1 <= -1 && !HalfX))
                {
                    return false;
                }
            }
            else if (dir == Direction.Left)
            {
                if ((this.PosX - 1 <= -1 && !HalfX))
                {
                    return false;
                }
            }
            else if (dir == Direction.DownLeft)
            {
                if ((this.PosY + 1 >= GridPos.TileHeightCount && !HalfY) || (this.PosX - 1 <= -1 && !HalfX))
                {
                    return false;
                }
            }
            else if (dir == Direction.Down)
            {
                if ((this.PosY + 1 >= GridPos.TileHeightCount && !HalfY))
                {
                    return false;
                }
            }
            else if (dir == Direction.DownRight)
            {
                if ((this.PosY + 1 >= GridPos.TileHeightCount && !HalfY) || (this.PosX + 1 >= GridPos.TileWidthCount && !HalfX))
                {
                    return false;
                }
            }
            else if (dir == Direction.Right)
            {
                if ((this.PosX + 1 >= GridPos.TileWidthCount && !HalfX))
                {
                    return false;
                }
            }
            else
            {
                if ((this.PosY - 1 <= -1 && !HalfY) || (this.PosX + 1 >= GridPos.TileWidthCount && !HalfX))
                {
                    return false;
                }
            }
            return true;
        }

        public Rectangle checkForCollision(Direction dir, SubSquares sub, Rectangle rect)
        {
            Rectangle tempRect = rect;
            tempRect = Move(dir, sub, tempRect);

            if (!GManager.collidesWith<Wall>(tempRect))
            {
                Console.WriteLine("here");
                return rect = Move(dir, sub, rect);
            }
            return rect;
        }

        /// <summary>
        /// moves the object in the direction given
        /// </summary>
        /// <param name="dir">the direction you want to go</param>
        /// <param name="sub">the top left sub square that you occupy</param>
        public Rectangle Move(Direction dir, SubSquares sub, Rectangle rect)
        {
            //finds out where the next square is and makes this square = that squares location
            rect.X = GridPos.getNextSquare(dir, GridPos.getGridSquare(Position), sub, this).X;
            rect.Y = GridPos.getNextSquare(dir, GridPos.getGridSquare(Position), sub, this).Y;
            //if moving to the right && you are not fully in a square, 
            //but instead halfway through the square in the x-direction
            if (dir == Direction.Right && HalfX)
            {
                PosX++;
            }
                //if moving up and to the right
            else if (dir == Direction.UpRight)
            {
                //if the top left of the square is halfway through the square in the x and y direction
                if (HalfX && HalfY)
                {
                    //you will move into the square to your right when you move
                    PosX++;
                }
                    //if you are fully in the square
                else if (!HalfY && !HalfX)
                {
                    //you will end up moving into the square above you
                    PosY--;
                }
                    //if you are halfway through the square in the x direciton
                else if (HalfX && !HalfY)
                {
                    //you will end up moving into the square to the right and above you
                    PosX++;
                    PosY--;
                }
                    //empty if for when you are halfway through the square in the y-direction
                else if (!HalfX && HalfY)
                {
                    //you shouldnt end up changing squares so nothing is neccesary to do here
                }
            }
            else if (dir == Direction.Up && !HalfY)
            {
                PosY--;
            }
            else if (dir == Direction.UpLeft)
            {
                if (!HalfY && !HalfX)
                {
                    PosX--;
                    PosY--;
                }
                else if (HalfY && !HalfX)
                {
                    PosX--;
                }
                else if (HalfX && !HalfY)
                {
                    PosY--;
                }
            }
            else if (dir == Direction.Left && !HalfX)
            {
                PosX--;
            }
            else if (dir == Direction.DownLeft)
            {
                if (HalfX && HalfY)
                {
                    PosY++;
                }
                else if (!HalfY && !HalfX)
                {
                    PosX--;
                }
                else if (HalfY)
                {
                    PosX--;
                    PosY++;
                }
            }
            else if (dir == Direction.Down && HalfY)
            {
                PosY++;
            }
            else if(dir == Direction.DownRight)
            {
                //Console.WriteLine("here");
                if (HalfX&&HalfY)
                {
                    PosX++;
                    PosY++;
                }
                else if (HalfY)
                {
                    PosY++;
                }
                else if(HalfX)
                {
                    PosX++;
                }
            }
            if (dir == Direction.Up || dir == Direction.UpLeft || dir == Direction.UpRight ||
                dir == Direction.Down || dir == Direction.DownLeft || dir == Direction.DownRight)
            {
                this.HalfY = !this.HalfY;
            }
            if (dir == Direction.Left || dir == Direction.UpLeft || dir == Direction.DownLeft ||
                dir == Direction.Right || dir == Direction.UpRight || dir == Direction.DownRight)
            {
                this.HalfX = !this.HalfX;
            }
            return rect;
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
