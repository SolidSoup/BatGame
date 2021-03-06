﻿//Everyone at some point
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
        bool screech; //to keep track of when bat echolocation wave should start
        bool screeching; //to keep track of screech movement
        Direction screechdirection;
        PlayerState playerState;
        bool shriek; //keep track if bat can shriek
        bool shrieking; //shriek is handled in a similar way to screech
        bool finishedLevel;

        bool hitBottom;
        bool hitTop;
        bool hitLeft;
        bool hitRight;
        bool hitFinish;
        Level currentLevel;

        private Grid grid;
        private QuadTangle currentQuadtangle;

        #region Properties
        public QuadTangle CurrentQuadTangle
        {
            get { return currentQuadtangle; }
        }

        public bool Shrieking
        {
            get { return shrieking; }
            set { shrieking = value; }
        }

        public bool Shriek
        {
            get { return shriek; }
            set { shriek = value; }
        }

        internal Direction Screechdirection
        {
            get { return screechdirection; }
            set { screechdirection = value; }
        }

        public int ScreechTime
        {
            get { return screechTime; }
            set { screechTime = value; }
        }

        public bool Screeching
        {
            get { return screeching; }
            set { screeching = value; }
        }

        public bool Screech
        {
            get { return screech; }
            set { screech = value; }
        }

        public bool FinishedLevel
        {
            get
            {
                return finishedLevel;
            }
        }

        public bool HitLeft
        {
            get
            {
                return hitLeft;
            }
        }

        public bool HitTop
        {
            get
            {
                return hitTop;
            }
        }

        public bool HitBottom
        {
            get
            {
                return hitBottom;
            }
        }

        public bool HitRight
        {
            get
            {
                return hitRight;
            }
        }

        public bool HitFinish
        {
            get
            {
                return hitFinish;
            }
        }

        public Level CurrentLevel
        {
            get
            {
                return currentLevel;
            }
            set
            {
                currentLevel = value;
            }
        }

        #endregion


        //possibly add check for something to decide length between screeches
        /// <summary>
        /// Constructor for the player
        /// </summary>
        /// <param name="t">texture of the player</param>
        /// <param name="onjMngr">gameObjectManager for the player</param>
        /// <param name="p">x y coordinate for the player in the grid.</param>
        /// <param name="g">the grid that the player is in</param>
        /// <param name="d">the direction the obj is currently facing</param>
        /// <param name="sub">The subsquare that you are in for the grid square that you are in</param>
        /// <param name="s">a bool representing whether the object is solid aka: a mobile is solid</param>
        /// <param name="speed">a timer that increments and has to be greater than the movetime to move</param>
        /// <param name="m">the time that it takes for this obj to move and animate from one square to the next</param>
        /// <param name="a">a bool that says if it is active which can mean alive or on screen</param>
        /// <param name="time">time is how long the player must wait before they can screech again</param>
        public Player(Texture2D t, GameObjectManager objMngr, Point p, Grid g, Direction d, SubSquares sub, bool s, double speed, double m, bool a, int time)
            : base(t, objMngr, p, g, d, sub, s, speed, m, a)
        {
            screechTime = 0;
            screech = false;
            screeching = false;
            shriek = false;
            finishedLevel = false;

            grid = g;
            
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
                    && isFacing(Direction.Up) && canMove(Direction.Up) /*&& willMove(new Point(Position.X, Position.Y - 1))*/)
                {
                    ObjRectangle = checkForCollision(Facing, MiniSquare, ObjRectangle);
                    /*PosY--;
                    RectY = GridPos.getPosition(Position, SubSquares.TopLeft).Y;*/
                    Speed = 0;
                    //this.RectY -= this.Speed;
                    playerState = PlayerState.Flying;

                }
                //look up if you aren't and you want to
                else if (state.IsKeyDown(Keys.W) && state.IsKeyUp(Keys.A) && state.IsKeyUp(Keys.D))
                {
                    Facing = Direction.Up;
                    Speed = MoveTime / 2;
                    playerState = PlayerState.Idle;

                }
                //if you want to move left only and you are looking left
                if (state.IsKeyDown(Keys.A) && state.IsKeyUp(Keys.W) && state.IsKeyUp(Keys.S)
                    && isFacing(Direction.Left) && canMove(Direction.Left) /*&& willMove(new Point(Position.X - 1, Position.Y))*/)
                {
                    ObjRectangle = checkForCollision(Facing, MiniSquare, ObjRectangle);
                    //PosX--;
                    //RectX = GridPos.getPosition(Position, SubSquares.TopLeft).X;
                    Speed = 0;
                    //this.RectX -= this.Speed;
                    playerState = PlayerState.Flying;
                }
                //look left if you aren't and you want to
                else if (state.IsKeyDown(Keys.A) && state.IsKeyUp(Keys.W) && state.IsKeyUp(Keys.S))
                {
                    Facing = Direction.Left;
                    Speed = MoveTime / 2;
                    playerState = PlayerState.Idle;
                }
                //if you want to move down only and you are looking down
                if (state.IsKeyDown(Keys.S) && state.IsKeyUp(Keys.A) && state.IsKeyUp(Keys.D)
                    && isFacing(Direction.Down) && canMove(Direction.Down) /*&& willMove(new Point(Position.X, Position.Y + 1))*/)
                {
                    ObjRectangle = checkForCollision(Facing, MiniSquare, ObjRectangle);
                    //PosY++;
                    //RectY = GridPos.getPosition(Position, SubSquares.TopLeft).Y;
                    Speed = 0;
                    //this.RectY += this.Speed;
                    playerState = PlayerState.Flying;
                }
                //look down if you aren't and you want to
                else if (state.IsKeyDown(Keys.S) && state.IsKeyUp(Keys.A) && state.IsKeyUp(Keys.D))
                {
                    Facing = Direction.Down;
                    Speed = MoveTime / 2;
                    playerState = PlayerState.Idle;
                }
                //if you want to move right only and you are looking right
                if (state.IsKeyDown(Keys.D) && state.IsKeyUp(Keys.W) && state.IsKeyUp(Keys.S)
                    && isFacing(Direction.Right) && canMove(Direction.Right) /*&& willMove(new Point(Position.X + 1, Position.Y))*/)
                {
                    ObjRectangle = checkForCollision(Facing, MiniSquare, ObjRectangle);
                    //PosX++;
                    //RectX = GridPos.getPosition(Position, SubSquares.TopLeft).X;
                    Speed = 0;
                    //this.RectX += this.Speed;
                    playerState = PlayerState.Flying;
                }
                //look right if you aren't and you want to
                else if (state.IsKeyDown(Keys.D) && state.IsKeyUp(Keys.W) && state.IsKeyUp(Keys.S))
                {
                    Facing = Direction.Right;
                    Speed = MoveTime / 2;
                    playerState = PlayerState.Idle;
                }
                //diaobjMngrnal movement
                //move up and to the left
                if (state.IsKeyDown(Keys.W) && state.IsKeyDown(Keys.A) && isFacing(Direction.UpLeft) && canMove(Direction.UpLeft)
                    /*&& willMove(new Point(Position.X - 1, Position.Y - 1))*/)
                {
                    ObjRectangle = checkForCollision(Facing, MiniSquare, ObjRectangle);
                    //PosY--;
                    //RectY = GridPos.getPosition(Position, SubSquares.TopLeft).Y;
                    //PosX--;
                    //RectX = GridPos.getPosition(Position, SubSquares.TopLeft).X;
                    Speed = 0;
                    //this.RectY -= this.Speed;
                    playerState = PlayerState.Flying;
                }
                //face up left if you aren't
                else if (state.IsKeyDown(Keys.W) && state.IsKeyDown(Keys.A))
                {
                    Facing = Direction.UpLeft;
                    Speed = MoveTime / 2;
                    playerState = PlayerState.Idle;
                }
                //move up and to the right
                if (state.IsKeyDown(Keys.W) && state.IsKeyDown(Keys.D) && isFacing(Direction.UpRight) && canMove(Direction.UpRight)
                    /*&& willMove(new Point(Position.X + 1, Position.Y - 1))*/)
                {
                    ObjRectangle = checkForCollision(Facing, MiniSquare, ObjRectangle);
                    //PosY--;
                    //RectY = GridPos.getPosition(Position, SubSquares.TopLeft).Y;
                    //PosX++;
                    //RectX = GridPos.getPosition(Position, SubSquares.TopLeft).X;
                    Speed = 0;
                    //this.RectY -= this.Speed;
                    playerState = PlayerState.Flying;
                }
                //face up right if you aren't
                else if (state.IsKeyDown(Keys.W) && state.IsKeyDown(Keys.D))
                {
                    Facing = Direction.UpRight;
                    Speed = MoveTime / 2;
                    playerState = PlayerState.Idle;
                }
                //move down and to the left
                if (state.IsKeyDown(Keys.S) && state.IsKeyDown(Keys.A) && isFacing(Direction.DownLeft)
                    && canMove(Direction.DownLeft) /*&& willMove(new Point(Position.X - 1, Position.Y + 1))*/)
                {
                    ObjRectangle = checkForCollision(Facing, MiniSquare, ObjRectangle);
                    //PosY++;
                    //RectY = GridPos.getPosition(Position, SubSquares.TopLeft).Y;
                    //PosX--;
                    //RectX = GridPos.getPosition(Position, SubSquares.TopLeft).X;
                    Speed = 0;
                    //this.RectY -= this.Speed;
                    playerState = PlayerState.Flying;
                }
                //face down left if you aren't
                else if (state.IsKeyDown(Keys.S) && state.IsKeyDown(Keys.A))
                {
                    Facing = Direction.DownLeft;
                    Speed = MoveTime / 2;
                    playerState = PlayerState.Idle;
                }
                //move down and right
                if (state.IsKeyDown(Keys.S) && state.IsKeyDown(Keys.D)
                    && isFacing(Direction.DownRight) && canMove(Direction.DownRight)
                    /*&& willMove(new Point(Position.X + 1, Position.Y + 1))*/)
                {
                    ObjRectangle = checkForCollision(Facing, MiniSquare, ObjRectangle);
                    //PosY++;
                    //RectY = GridPos.getPosition(Position, SubSquares.TopLeft).Y;
                    //PosX++;
                    //RectX = GridPos.getPosition(Position, SubSquares.TopLeft).X;
                    Speed = 0;
                    //this.RectY -= this.Speed;
                    playerState = PlayerState.Flying;
                }
                //face down right if you aren't
                else if (state.IsKeyDown(Keys.S) && state.IsKeyDown(Keys.D))
                {
                    Facing = Direction.DownRight;
                    Speed = MoveTime / 2;
                    playerState = PlayerState.Idle;
                }

            }
            //screech if it's off cooldown
            if (state.IsKeyDown(Keys.Space) && screech == false && screechTime == 0)
            {
                screech = true;
            }
            //shriek if off cooldown
            if (shriek == true)
            {
                shrieking = true;
                shriek = false;
            }
            if (state.IsKeyDown(Keys.LeftShift) && shriek == false && screechTime == 0)
            {
                shriek = true;
            }

            currentQuadtangle = grid.getGridSquare(Position);
        }

        public void PlayerDraw(Texture2D playerText)
        {
            //IDLE Animations?

        }

        public void PartyMode()
        {
            PlayerUpdate();
        }


        /// <summary>
        /// increments hits if hits a bad thingy
        /// </summary>
        public void BadCollide()
        {
            List<GameObject> objects = GManager.inSpot(Position);
            foreach (GameObject g in objects)
            {
                if (g is Enemy)
                {
                    Enemy temp = (Enemy)(g);
                    if (temp.IsActive)
                    {
                        this.hits++;
                    }
                }
                if (g is SpiderWeb)
                {
                    SpiderWeb temp = (SpiderWeb)(g);
                    if (temp.IsActive)
                    {
                        this.hits++;
                    }
                }
                if (g is Exit)
                {
                    Exit temp = (Exit)(g);
                    if (temp.Type == Exit.ExitType.Up)
                    {
                        hitTop = true;
                    }
                    else if (temp.Type == Exit.ExitType.Down)
                    {
                        hitBottom = true;
                    }
                    else if (temp.Type == Exit.ExitType.Left)
                    {
                        hitLeft = true;
                    }
                    else if (temp.Type == Exit.ExitType.Right)
                    {
                        hitRight = true;
                    }
                    else if (temp.Type == Exit.ExitType.Finish)
                    {
                        hitFinish = true;
                    }
                }
            }
        }

        public int Hits { get { return hits; } }

    }
}
