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

        #region Astar fields
        private List<QuadTangle> neighborList;
        private QuadTangle parent;
        private QuadTangle upNeighbor;
        private QuadTangle downNeighbor;
        private QuadTangle leftNeighbor;
        private QuadTangle rightNeighbor;
        private QuadTangle upleftNeighbor;
        private QuadTangle uprightNeighbor;
        private QuadTangle downleftNeighbor;
        private QuadTangle downrightNeighbor;

        private int distanceToEnd;
        private int distanceFromStart;
        private int pathcost;

        private int rank;


        private GameObject objInTangle;
        #endregion

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

            rank = 0;

            parent = null;
            upNeighbor = null;
            downNeighbor = null;
            leftNeighbor = null;
            rightNeighbor = null;
            upleftNeighbor = null;
            uprightNeighbor = null;
            downleftNeighbor = null;
            downrightNeighbor = null;

            neighborList = new List<QuadTangle>();
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

            rank = 0;

            parent = null;
            upNeighbor = null;
            downNeighbor = null;
            leftNeighbor = null;
            rightNeighbor = null;
            upleftNeighbor = null;
            uprightNeighbor = null;
            downleftNeighbor = null;
            downrightNeighbor = null;

            neighborList = new List<QuadTangle>();
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

        #region Astar properties
        public List<QuadTangle> NeighborList
        {
            get { return neighborList; }
            set { neighborList = value; }
        }

        public QuadTangle Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public QuadTangle UpNeighbor
        {
            get { return upNeighbor; }
            set { upNeighbor = value; }
        }

        public QuadTangle DownNeighbor
        {
            get { return downNeighbor; }
            set { downNeighbor = value; }
        }

        public QuadTangle LeftNeighbor
        {
            get { return leftNeighbor; }
            set { leftNeighbor = value; }
        }

        public QuadTangle RightNeighbor
        {
            get { return rightNeighbor; }
            set { rightNeighbor = value; }
        }

        public QuadTangle UpLeftNeighbor
        {
            get { return upleftNeighbor; }
            set { upleftNeighbor = value; }
        }

        public QuadTangle DownRightNeighbor
        {
            get { return downrightNeighbor; }
            set { downrightNeighbor = value; }
        }

        public QuadTangle DownLeftNeighbor
        {
            get { return downleftNeighbor; }
            set { downleftNeighbor = value; }
        }

        public QuadTangle UpRightNeighbor
        {
            get { return uprightNeighbor; }
            set { uprightNeighbor = value; }
        }

        public int DistanceFromStart
        {
            get { return distanceFromStart; }
            set { distanceFromStart = value; }
        }

        public int DistanceToEnd
        {
            get { return distanceToEnd; }
            set { distanceToEnd = value; }
        }

        public int PathCost
        {
            get { return pathcost; }
            set { pathcost = value; }
        }

        public int Rank
        {
            get { return rank; }
            set { rank = value; }
        }


        public GameObject ObjInTangle
        {
            get { return objInTangle; }
            set { objInTangle = value; }
        }
        #endregion
    }
}
