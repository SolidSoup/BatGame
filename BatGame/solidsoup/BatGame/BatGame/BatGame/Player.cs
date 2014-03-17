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
        KeyboardState state;


        //possibly add check for something to decide length between screeches
        public Player(Texture2D t, Rectangle r, Point p, Grid g, bool s, double speed, bool a, int time)
            : base(t, r, p, g, s, speed, a)
        {
            this.screechTime = time;
        }

        public void PlayerUpdate()
        {
            state = Keyboard.GetState();

            if (this.Speed >= 10)
            {
                if (state.IsKeyDown(Keys.W))
                {
                    PosY--;
                    RectY = GridPos.getPosition(Position).Y;
                    Speed = 0;
                    //this.RectY -= this.Speed;
                }
                if (state.IsKeyDown(Keys.A))
                {
                    PosX--;
                    RectX = GridPos.getPosition(Position).X;
                    Speed = 0;
                    //this.RectX -= this.Speed;
                }
                if (state.IsKeyDown(Keys.S))
                {
                    PosY++;
                    RectY = GridPos.getPosition(Position).Y;
                    Speed = 0;
                    //this.RectY += this.Speed;
                }
                if (state.IsKeyDown(Keys.D))
                {
                    PosX++;
                    RectX = GridPos.getPosition(Position).X;
                    Speed = 0;
                    //this.RectX += this.Speed;
                }
            }
        }
    }
}
