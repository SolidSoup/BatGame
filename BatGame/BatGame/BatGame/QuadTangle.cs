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
    enum SubSquares
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }

    class QuadTangle
    {
        Rectangle rect;
        Rectangle tLeft;
        Rectangle tRight;
        Rectangle bLeft;
        Rectangle bRight;

        bool tLeftFull;
        bool tRightFull;
        bool bLeftFull;
        bool bRightFull;

        Point locInGrid;

        public QuadTangle(int x, int y, int width, int height, bool tl, bool tr, bool bl, bool br, Point loc)
        {
            rect = new Rectangle(x, y, width, height);
            tLeft = new Rectangle(x, y, width / 2, height / 2);
            tRight = new Rectangle(x + (width / 2), y, width / 2, height / 2);
            bLeft = new Rectangle(x, y + (width / 2), width / 2, height / 2);
            bRight = new Rectangle(x + (width / 2), y + (width / 2), width / 2, height / 2);

            tLeftFull = tl;
            tRightFull = tr;
            bLeftFull = bl;
            bRightFull = br;

            locInGrid = loc;
        }

        public QuadTangle(Rectangle rect, bool tl, bool tr, bool bl, bool br, Point loc)
        {
            this.rect = rect;
            tLeft = new Rectangle(rect.X, rect.Y, rect.Width / 2, rect.Height / 2);
            tRight = new Rectangle(rect.X + (rect.Width / 2), rect.Y, rect.Width / 2, rect.Height / 2);
            bLeft = new Rectangle(rect.X, rect.Y + (rect.Width / 2), rect.Width / 2, rect.Height / 2);
            bRight = new Rectangle(rect.X + (rect.Width / 2), rect.Y + (rect.Width / 2), rect.Width / 2, rect.Height / 2);

            tLeftFull = tl;
            tRightFull = tr;
            bLeftFull = bl;
            bRightFull = br;

            locInGrid = loc;
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

        public bool TLeftFull
        {
            get
            {
                return tLeftFull;
            }
            set
            {
                tLeftFull = value;
            }
        }

        public bool TRightFull
        {
            get
            {
                return tRightFull;
            }
            set
            {
                tRightFull = value;
            }
        }

        public bool BLeftFull
        {
            get
            {
                return bLeftFull;
            }
            set
            {
                bLeftFull = value;
            }
        }

        public bool BRightFull
        {
            get
            {
                return bRightFull;
            }
            set
            {
                bRightFull = value;
            }
        }

        public Point LocInGrid
        {
            get
            {
                return locInGrid;
            }
            set
            {
                locInGrid = value;
            }
        }
    }
}
