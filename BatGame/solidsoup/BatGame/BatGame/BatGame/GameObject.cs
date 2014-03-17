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
    class GameObject
    {
        private Texture2D objTexture;
        private Rectangle objRectangle;
        Point position;
        Grid gridPos;

        //false for background images or non-active objects
        bool isSolid;

        public Texture2D ObjTexture
        {
            get { return this.objTexture; }
            set { this.objTexture = value; }
        }

        public Rectangle ObjRectangle
        {
            get { return this.objRectangle; }
            set { this.objRectangle = value; }
        }

        //keep this public
        public bool IsSolid
        {
            get { return this.isSolid; }
            set { this.isSolid = value; }
        }

        //DON'T CHANGE!!!
        public int RectY
        {
            get { return this.objRectangle.Y; }
            set
            {
                objRectangle.Y = value;
                /*Rectangle temp = new Rectangle(this.objRectangle.X, value, this.objRectangle.Width, this.objRectangle.Height);
                this.objRectangle = temp;*/
            }
        }

        //DON'T CHANGE!!!
        public int RectX
        {
            get { return this.objRectangle.X; }
            set
            {
                objRectangle.X = value;
                /*Rectangle temp = new Rectangle(value, this.objRectangle.Y, this.objRectangle.Width, this.objRectangle.Height);
                this.objRectangle = temp;*/
            }
        }

        public Point Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        public int PosX
        {
            get
            {
                return position.X;
            }
            set
            {
                position.X = value;
            }
        }

        public int PosY
        {
            get
            {
                return position.Y;
            }
            set
            {
                position.Y = value;
            }
        }

        public Grid GridPos
        {
            get
            {
                return gridPos;
            }
            set
            {
                gridPos = value;
            }
        }

        //instantiate for walls or inactive objects
        public GameObject(Texture2D t, Rectangle r, Point p, Grid g, bool s)
        {
            this.objTexture = t;
            this.objRectangle = r;
            this.position = p;
            this.gridPos = g;
            this.isSolid = s;
        }
    }
}
