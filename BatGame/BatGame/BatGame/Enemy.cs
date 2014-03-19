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

        public Enemy(Texture2D t, Point p, Grid g, Direction d, bool s, double speed, double m, bool a, int time, bool detect)
            : base(t, p, g, d, s, m, speed, a)
        {
            this.actionTime = time;
            this.detected = false;
        }

        public void Update()
        {

        }
    }
}
