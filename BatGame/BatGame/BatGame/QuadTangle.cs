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
    class QuadTangle
    {
        Rectangle rect;
        Rectangle tLeft;
        Rectangle tRight;
        Rectangle bLeft;
        Rectangle bRight;

        public QuadTangle(int x, int y, int width, int height)
        {
            rect = new Rectangle(x, y, width, height);
            tLeft = new Rectangle(x, y, width / 2, height / 2);
            tRight = new Rectangle(x + (width / 2), y, width / 2, height / 2);
            bLeft = new Rectangle(x, y + (width / 2), width / 2, height / 2);
            bRight = new Rectangle(x + (width / 2), y + (width / 2), width / 2, height / 2);
        }

        public static implicit operator Rectangle(QuadTangle q)
        {
            return q.Rect;
        }

        //Accessors
        public Rectangle Rect
        {
            get
            {
                return rect;
            }
            set
            {
                rect = value;
            }
        }

        public Rectangle TLeft
        {
            get
            {
                return tLeft;
            }
            set
            {
                tLeft = value;
            }
        }

        public Rectangle TRight
        {
            get
            {
                return tRight;
            }
            set
            {
                tRight = value;
            }
        }

        public Rectangle BLeft
        {
            get
            {
                return bLeft;
            }
            set
            {
                bLeft = value;
            }
        }

        public Rectangle BRight
        {
            get
            {
                return bRight;
            }
            set
            {
                bRight = value;
            }
        }
    }
}
