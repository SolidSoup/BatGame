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

        //instantiate for projectiles
        public Mobiles(Texture2D t, Rectangle r, Point p, Grid g, bool s, double speed, bool a)
            : base(t, r, p, g, s)
        {
            this.speed = speed;
            this.isActive = a;
        }
    }
}
