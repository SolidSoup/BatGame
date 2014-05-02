//Wes and Joel, Madison
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
    //currently exists for organizational purposes
     class Immobiles : GameObject
    {
         int drawWeight;
        //simply calls base constructor
        public Immobiles(Texture2D t, GameObjectManager go, Point p, Grid g, Direction d, SubSquares sub, bool s)
            : base(t, go, p, g, d, sub, s)
        {
            drawWeight = 1;
        }
        public virtual void Update()
        {
        }
        public virtual void Draw(SpriteBatch batch)
        {
            batch.Draw(this.ObjTexture, this.ObjRectangle, Color.White);
        }

        public int DrawWeight
        {
            get
            {
                return drawWeight;
            }
            set
            {
                drawWeight = value;
            }
        }
    }
}

