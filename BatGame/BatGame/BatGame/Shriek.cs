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
                }
                waitTime -= (double)gameTime.ElapsedGameTime.TotalSeconds;
                if (waitTime <= 0)
                {
                    waitTime = .05;
                    this.ObjRectangle = this.checkForCollision(this.Facing, this.MiniSquare, this.ObjRectangle);
                    //using check for collision for movement because it doesn't have to change directions

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
