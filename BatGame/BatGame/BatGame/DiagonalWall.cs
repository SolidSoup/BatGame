﻿using System;
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
    enum DiagonalWallType
    {
        upTop,
        upBottom,
        downTop,
        downBottom
    }
    class DiagonalWall : Immobiles
    {
        DiagonalWallType type;

        public DiagonalWall(Texture2D t, GameObjectManager go, Point p, Grid g, Direction d, SubSquares sub, bool s, DiagonalWallType type)
            : base(t, go, p, g, d, sub, s)
        {
            this.type = type;

            switch (type)
            {
                case DiagonalWallType.upTop:
                    QuadTangle temp = g.getGridSquare(this.Position);
                    // temp.BLeftFull = false;
                    //temp.TRightFull = false;
                    break;
                case DiagonalWallType.upBottom:
                    QuadTangle temp2 = g.getGridSquare(this.Position);
                    //temp2.BRightFull = false;
                    //temp2.TRightFull = false;
                    break;
                case DiagonalWallType.downTop:
                    QuadTangle temp3 = g.getGridSquare(this.Position);
                    //temp3.TLeftFull = false;
                    //temp3.BRightFull = false;
                    break;
                case DiagonalWallType.downBottom:
                    QuadTangle temp4 = g.getGridSquare(this.Position);
                    // temp4.TRightFull = false;
                    //temp4.BRightFull = false;
                    break;



            }
        }
        public DiagonalWallType WallType
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            switch (WallType)
            {

            }
            base.Draw(batch);
        }
    }
}
