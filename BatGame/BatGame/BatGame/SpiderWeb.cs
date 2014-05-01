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
    class SpiderWeb : Interactable
    {
        bool hasKilled;
        public SpiderWeb(Texture2D t, GameObjectManager go, Point p, Grid g, Direction d,SubSquares sub, bool s,bool active)
            : base(t, go, p, g, d, sub, s, active)
        {
            this.IsSolid = false;
        }


        public override void Update()
        {
            if (IsActive)
            {
                List<GameObject> objects = GManager.inSpot(Position);
                foreach (GameObject g in objects)
                {
                    if (g is Enemy) //|| GManager.Player.Position.Equals(this.Position))
                    //if (GManager.Player.Position.Equals(this.Position))
                    {
                        hasKilled = true;
                        //IsActive = false;
                        //IsSolid = true;
                    }
                }
            }
        }
        public override void Draw(SpriteBatch batch)
        {
            if (!hasKilled)
            {
                batch.Draw(this.ObjTexture, this.ObjRectangle, Color.White);
                //Animation for empty web
            }
            else
            {
                //batch.Draw(this.ObjTexture, this.ObjRectangle, Color.White);
                //Animation for full web
            }
        }
    }
}