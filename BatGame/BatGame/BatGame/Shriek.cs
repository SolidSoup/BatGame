//Greg
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
    //PLAN: Frankenstein stuff from enemy and Spiderweb in order to make this moving collidable
    class Shriek : Mobiles
    {
        double waitTime;
        public Shriek(Texture2D t, GameObjectManager go, Point p, Grid g, Direction d, SubSquares sub, bool s, double speed, double m, bool a, int time, Direction direct)
            : base(t, go, p, g, d, sub, s, m, speed, a)
        {
            this.Facing = direct;
            this.waitTime = .05;
        }
        public virtual void Update(GameTime gameTime) //this update is similar to Spiderweb's in that it goes inactive when it touches enemies
        {
            
            if (IsActive)
            {
                List<GameObject> objects = GManager.inSpot(Position);
                foreach (GameObject g in objects)
                {
                    if (g is Enemy && g.ObjRectangle.Intersects(this.ObjRectangle)) //|| GManager.Player.Position.Equals(this.Position))
                    //if (GManager.Player.Position.Equals(this.Position))
                    {
                        IsActive = false;
                        //IsSolid = true;
                    }
                    if (g is Exit && g.ObjRectangle.Intersects(this.ObjRectangle))
                    {
                        Exit temp = (Exit)(g);
                        if (temp.Type == Exit.ExitType.Up)
                        {
                            this.PosY += 2;
                            this.IsActive = false;
                        }
                        else if (temp.Type == Exit.ExitType.Down)
                        {
                            this.PosY -= 2;
                            this.IsActive = false;
                        }
                        else if (temp.Type == Exit.ExitType.Left)
                        {
                            this.PosX += 2;
                            this.IsActive = false;
                        }
                        else if (temp.Type == Exit.ExitType.Right)
                        {
                            this.PosX -= 2;
                            this.IsActive = false;
                        }
                        else if (temp.Type == Exit.ExitType.Finish)
                        {
                            this.PosX = 1;
                            this.PosY = 1;
                            this.IsActive = false;
                        }
                    }
                }
                waitTime -= (double)gameTime.ElapsedGameTime.TotalSeconds;
                if (waitTime <= 0)
                {
                    waitTime = .05;
                    //Uses a tentative Rectangle for movement based a little bit on half squares because the cardinal directions sometimes mess up the flight pattern
                    Rectangle move = this.checkForCollision(this.Facing, this.MiniSquare, this.ObjRectangle);
                    if (Facing == Direction.Right && this.HalfY)
                    {
                        move = this.checkForCollision(this.Facing, SubSquares.BottomRight, this.ObjRectangle);
                    }
                    else if (Facing == Direction.Left && this.HalfY)
                    {
                        move = this.checkForCollision(this.Facing, SubSquares.BottomLeft, this.ObjRectangle);
                    }
                    else if (Facing == Direction.Down && this.HalfX)
                    {
                        move = this.checkForCollision(this.Facing, SubSquares.BottomRight, this.ObjRectangle);
                    }
                    else if (Facing == Direction.Up && this.HalfX)
                    {
                        move = this.checkForCollision(this.Facing, SubSquares.TopRight, this.ObjRectangle);
                    }
                   
                    if (move.Equals(this.ObjRectangle)) //if it can't move, go inactive
                    {
                        IsActive = false;
                    }
                    this.ObjRectangle = this.checkForCollision(this.Facing, this.MiniSquare, this.ObjRectangle);
                }

            }
        }
        public virtual void Draw(SpriteBatch batch)
        {

            if (IsActive)
            {
                batch.Draw(this.ObjTexture, this.ObjRectangle, Color.Blue);
            }
        }
    }
}
