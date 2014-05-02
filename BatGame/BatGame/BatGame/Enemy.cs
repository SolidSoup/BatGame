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

    class Enemy : Mobiles
    {
        //time between specific actions that enemy has
        int actionTime;
        bool detected;
        int steps;
        double waitTime;
        double distance;
        int stunTime;
        double savedSpeed;

        public Enemy(Texture2D t, GameObjectManager go, Point p, Grid g, Direction d, SubSquares sub, bool s, double speed, double m, bool a, int time, bool detect)
            : base(t, go, p, g, d, sub, s, m, speed, a)
        {
            this.actionTime = time;
            this.detected = detect;
            this.Facing = Direction.Right;
            this.waitTime = .75;
            this.stunTime = 1000;
            savedSpeed = speed;
        }

        //meant to overriden for each enemy
        public virtual void EnemyUpdate(GameTime gameTime, Player player)
        {
            List<GameObject> objects = GManager.inSpot(Position);
            foreach (GameObject g in objects)
            {
                if (g is SpiderWeb)
                {
                    IsActive = false;
                }
                else if (g is Shriek)
                {
                    Shriek temp = (Shriek)(g);
                    if (temp.IsActive)
                    {
                        waitTime = 1.5;
                    }
                }
            }
            if (IsActive == true)
            {
                waitTime -= (double)gameTime.ElapsedGameTime.TotalSeconds;
                if (waitTime <= 0)
                {
                    waitTime = .75;

                    distance = Math.Pow((player.PosX - this.PosX), 2) + Math.Pow((player.PosY - this.PosY), 2);
                    if (distance < 10)
                    {
                        detected = true;
                    }

                    if (detected == true)
                    {

                        if (player.PosX < this.PosX && player.PosY < this.PosY)
                        {
                            Facing = Direction.UpLeft;
                        }
                        else if (player.PosX > this.PosX && player.PosY < this.PosY)
                        {
                            Facing = Direction.UpRight;
                        }
                        else if (player.PosX < this.PosX && player.PosY > this.PosY)
                        {
                            Facing = Direction.DownLeft;
                        }
                        else if (player.PosX > this.PosX && player.PosY > this.PosY)
                        {
                            Facing = Direction.DownRight;
                        }
                        else if (player.PosX < this.PosX)
                        {
                            Facing = Direction.Left;
                        }
                        else if (player.PosX > this.PosX)
                        {
                            Facing = Direction.Right;
                        }
                        else if (player.PosY < this.PosY)
                        {
                            Facing = Direction.Up;
                        }
                        else if (player.PosY > this.PosY)
                        {
                            Facing = Direction.Down;
                        }


                    }
                    //cardinal movement
                    //move Up
                    if (isFacing(Direction.Up) && canMove(Direction.Up))
                    {
                        PosY--;
                        steps++;
                        RectY = GridPos.getPosition(Position, SubSquares.TopLeft).Y;
                        if (steps > 3 && detected == false)
                        {
                            steps = 0;
                            Facing = Direction.Right;
                        }

                    }
                    //Move left
                    if (isFacing(Direction.Left) && canMove(Direction.Left))
                    {
                        PosX--;
                        steps++;
                        RectX = GridPos.getPosition(Position, SubSquares.TopLeft).X;
                        if (steps > 3 && detected == false)
                        {
                            steps = 0;
                            Facing = Direction.Up;
                        }

                    }
                    //Move Down
                    if (isFacing(Direction.Down) && canMove(Direction.Down))
                    {
                        PosY++;
                        steps++;
                        RectY = GridPos.getPosition(Position, SubSquares.TopLeft).Y;
                        if (steps > 3 && detected == false)
                        {
                            steps = 0;
                            Facing = Direction.Left;
                        }

                    }
                    //Move Right
                    if (isFacing(Direction.Right) && canMove(Direction.Right))
                    {
                        waitTime = .75;
                        PosX++;
                        steps++;
                        RectX = GridPos.getPosition(Position, SubSquares.TopLeft).X;
                        if (steps > 3 && detected == false)
                        {
                            steps = 0;
                            Facing = Direction.Down;
                        }

                    }
                    //Move left
                    if (isFacing(Direction.UpLeft) && canMove(Direction.UpLeft))
                    {
                        PosY--;
                        RectY = GridPos.getPosition(Position, SubSquares.TopLeft).Y;
                        PosX--;
                        RectX = GridPos.getPosition(Position, SubSquares.TopLeft).X;
                    }
                    //move up and to the right
                    if (isFacing(Direction.UpRight) && canMove(Direction.UpRight))
                    {
                        PosY--;
                        RectY = GridPos.getPosition(Position, SubSquares.TopLeft).Y;
                        PosX++;
                        RectX = GridPos.getPosition(Position, SubSquares.TopLeft).X;
                    }
                    //move down and to the left
                    if (isFacing(Direction.DownLeft) && canMove(Direction.DownLeft))
                    {
                        PosY++;
                        RectY = GridPos.getPosition(Position, SubSquares.TopLeft).Y;
                        PosX--;
                        RectX = GridPos.getPosition(Position, SubSquares.TopLeft).X;
                    }
                    //move down and right
                    if (isFacing(Direction.DownRight) && canMove(Direction.DownRight))
                    {
                        PosY++;
                        RectY = GridPos.getPosition(Position, SubSquares.TopLeft).Y;
                        PosX++;
                        RectX = GridPos.getPosition(Position, SubSquares.TopLeft).X;
                    }

                }
            }
        }

        public virtual void Draw(SpriteBatch batch)
        {
            if (IsActive)
            {
                batch.Draw(this.ObjTexture, this.ObjRectangle, Color.White);
            }
        }
    }
}
