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
    class Player : Mobiles
    {
        //not currently being used - if have extra time
        bool[] items;
        int screechTime;
        int hits = 0;
        KeyboardState state;


        //possibly add check for something to decide length between screeches
        public Player(Texture2D t, GameObjectManager go, Point p, Grid g, Direction d, SubSquares sub, bool hx, bool hy, bool s, double speed, double m, bool a, int time)
            : base(t, go, p, g, d, sub, hx, hy, s, speed, m, a)
        {
            this.screechTime = time;
        }

        public void PlayerUpdate()
        {
            state = Keyboard.GetState();

            //if the animation has finished you can move
            if (this.Speed >= MoveTime)
            {
                BadCollide();
                //cardinal movement
                //if you want to move up only and you are looking up
                if (state.IsKeyDown(Keys.W) && state.IsKeyUp(Keys.A) && state.IsKeyUp(Keys.D)
                    && isFacing(Direction.Up) && canMove(Direction.Up) && willMove(new Point(Position.X, Position.Y - 1)))
                {
                    //Console.WriteLine(willMove(new Point(Position.X, Position.Y - 1)));
                    Move(Facing, MiniSquare);
                    /*PosY--;
                    RectY = GridPos.getPosition(Position, SubSquares.TopLeft).Y;*/
                    Speed = 0;
                    //this.RectY -= this.Speed;
                }
                //look up if you aren't and you want to
                else if (state.IsKeyDown(Keys.W) && state.IsKeyUp(Keys.A) && state.IsKeyUp(Keys.D))
                {
                    Facing = Direction.Up;
                    Speed = MoveTime / 2;
                }
                //if you want to move left only and you are looking left
                if (state.IsKeyDown(Keys.A) && state.IsKeyUp(Keys.W) && state.IsKeyUp(Keys.S)
                    && isFacing(Direction.Left) && canMove(Direction.Left) && willMove(new Point(Position.X - 1, Position.Y)))
                {
                    Move(Facing, MiniSquare);
                    //PosX--;
                    //RectX = GridPos.getPosition(Position, SubSquares.TopLeft).X;
                    Speed = 0;
                    //this.RectX -= this.Speed;
                }
                //look left if you aren't and you want to
                else if (state.IsKeyDown(Keys.A) && state.IsKeyUp(Keys.W) && state.IsKeyUp(Keys.S))
                {
                    Facing = Direction.Left;
                    Speed = MoveTime / 2;
                }
                //if you want to move down only and you are looking down
                if (state.IsKeyDown(Keys.S) && state.IsKeyUp(Keys.A) && state.IsKeyUp(Keys.D)
                    && isFacing(Direction.Down) && canMove(Direction.Down) && willMove(new Point(Position.X, Position.Y + 1)))
                {
                    Move(Facing, MiniSquare);
                    //PosY++;
                    //RectY = GridPos.getPosition(Position, SubSquares.TopLeft).Y;
                    Speed = 0;
                    //this.RectY += this.Speed;
                }
                //look down if you aren't and you want to
                else if (state.IsKeyDown(Keys.S) && state.IsKeyUp(Keys.A) && state.IsKeyUp(Keys.D))
                {
                    Facing = Direction.Down;
                    Speed = MoveTime / 2;
                }
                //if you want to move right only and you are looking right
                if (state.IsKeyDown(Keys.D) && state.IsKeyUp(Keys.W) && state.IsKeyUp(Keys.S)
                    && isFacing(Direction.Right) && canMove(Direction.Right) && willMove(new Point(Position.X + 1, Position.Y)))
                {
                    Move(Facing, MiniSquare);
                    //PosX++;
                    //RectX = GridPos.getPosition(Position, SubSquares.TopLeft).X;
                    Speed = 0;
                    //this.RectX += this.Speed;
                }
                //look right if you aren't and you want to
                else if (state.IsKeyDown(Keys.D) && state.IsKeyUp(Keys.W) && state.IsKeyUp(Keys.S))
                {
                    Facing = Direction.Right;
                    Speed = MoveTime / 2;
                }
                //diagonal movement
                //move up and to the left
                if (state.IsKeyDown(Keys.W) && state.IsKeyDown(Keys.A) && isFacing(Direction.UpLeft) && canMove(Direction.UpLeft)
                    && willMove(new Point(Position.X - 1, Position.Y - 1)))
                {
                    Move(Facing, MiniSquare);
                    //PosY--;
                    //RectY = GridPos.getPosition(Position, SubSquares.TopLeft).Y;
                    //PosX--;
                    //RectX = GridPos.getPosition(Position, SubSquares.TopLeft).X;
                    Speed = 0;
                    //this.RectY -= this.Speed;
                }
                //face up left if you aren't
                else if (state.IsKeyDown(Keys.W) && state.IsKeyDown(Keys.A))
                {
                    Facing = Direction.UpLeft;
                    Speed = MoveTime / 2;
                }
                //move up and to the right
                if (state.IsKeyDown(Keys.W) && state.IsKeyDown(Keys.D) && isFacing(Direction.UpRight) && canMove(Direction.UpRight)
                    && willMove(new Point(Position.X + 1, Position.Y - 1)))
                {
                    Move(Facing, MiniSquare);
                    //PosY--;
                    //RectY = GridPos.getPosition(Position, SubSquares.TopLeft).Y;
                    //PosX++;
                    //RectX = GridPos.getPosition(Position, SubSquares.TopLeft).X;
                    Speed = 0;
                    //this.RectY -= this.Speed;
                }
                //face up right if you aren't
                else if (state.IsKeyDown(Keys.W) && state.IsKeyDown(Keys.D))
                {
                    Facing = Direction.UpRight;
                    Speed = MoveTime / 2;
                }
                //move down and to the left
                if (state.IsKeyDown(Keys.S) && state.IsKeyDown(Keys.A) && isFacing(Direction.DownLeft)
                    && canMove(Direction.DownLeft) && willMove(new Point(Position.X - 1, Position.Y + 1)))
                {
                    Move(Facing, MiniSquare);
                    //PosY++;
                    //RectY = GridPos.getPosition(Position, SubSquares.TopLeft).Y;
                    //PosX--;
                    //RectX = GridPos.getPosition(Position, SubSquares.TopLeft).X;
                    Speed = 0;
                    //this.RectY -= this.Speed;
                }
                //face down left if you aren't
                else if (state.IsKeyDown(Keys.S) && state.IsKeyDown(Keys.A))
                {
                    Facing = Direction.DownLeft;
                    Speed = MoveTime / 2;
                }
                //move down and right
                if (state.IsKeyDown(Keys.S) && state.IsKeyDown(Keys.D)
                    && isFacing(Direction.DownRight) && canMove(Direction.DownRight)
                    && willMove(new Point(Position.X + 1, Position.Y + 1)))
                {
                    Move(Facing, MiniSquare);
                    //PosY++;
                    //RectY = GridPos.getPosition(Position, SubSquares.TopLeft).Y;
                    //PosX++;
                    //RectX = GridPos.getPosition(Position, SubSquares.TopLeft).X;
                    Speed = 0;
                    //this.RectY -= this.Speed;
                }
                //face down right if you aren't
                else if (state.IsKeyDown(Keys.S) && state.IsKeyDown(Keys.D))
                {
                    Facing = Direction.DownRight;
                    Speed = MoveTime / 2;
                }

            }
        }

        /// <summary>
        /// increments hits if hits a bad thingy
        /// </summary>
        public void BadCollide()
        {
            if(GManager.inSpot(Position) is Enemy)
            {
                this.hits++;
            }
        }

        public int Hits { get { return hits; } }
    }
}
