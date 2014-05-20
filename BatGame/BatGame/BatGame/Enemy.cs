//Everyone but Karen at this point
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
        int steps;
        double waitTime;
        double distance;
        int stunTime;
        double savedSpeed;

        Grid grid;
        GameObjectManager gom;

        QuadTangle locInGrid;

        Point startPoint;
        bool needToFindStart;

        #region Astar fields
        QuadTangle current;
        List<QuadTangle> open = new List<QuadTangle>();
        List<QuadTangle> closed = new List<QuadTangle>();
        Player playerPos;

        QuadTangle[,] graph;

        #endregion

        public Enemy(Texture2D t, GameObjectManager go, Point p, Grid g, Direction d, SubSquares sub, bool s, double speed, double m, bool a, int time, bool detect)
            : base(t, go, p, g, d, sub, s, m, speed, a)
        {
            this.actionTime = time;
            this.detected = detect;
            this.Facing = Direction.Right;
            this.waitTime = .75;
            this.stunTime = 1000;
            savedSpeed = speed;

            grid = g;
            gom = go;

            startPoint = p;
            needToFindStart = false;
            steps = 0;

        }

        //meant to overriden for each enemy
        public virtual void EnemyUpdate(GameTime gameTime, Player player)
        {
            graph = grid.GetGrid;
            playerPos = player; // location of current quadtangle stays as spawn location
            locInGrid = grid.getGridSquare(Position);
            List<GameObject> objects = GManager.inSpot(Position);
            foreach (GameObject g in objects)
            {
                if (g is SpiderWeb)
                {
                    IsActive = false;
                }
                else if (g is Shriek)
                {
                    Shriek temp = (Shriek)(g);
                    if (temp.IsActive)
                    {
                        waitTime = 1.5;
                    }
                }
            }
            List<GameObject> playerObjects = GManager.inSpot(playerPos.Position);
            foreach (GameObject g in playerObjects)
            {
                if (g is Skull)
                    detected = false;
            }
            if (IsActive == true)
            {
                waitTime -= (double)gameTime.ElapsedGameTime.TotalSeconds;
                if (waitTime <= 0)
                {
                    waitTime = .75;

                    distance = Math.Pow((player.PosX - this.PosX), 2) + Math.Pow((player.PosY - this.PosY), 2);
                    if (distance < 10)
                    {
                        detected = true;
                        steps = 0;
                    }
                    else if (distance > 100)
                    {
                        if (detected)
                            needToFindStart = true;
                        detected = false;
                    }

                    if (detected == true)
                    {
                        Astar();
                    }
                    else if (needToFindStart)
                        Astart();
                    UndetectedMovement();
                    Move();


                }
            }
        }
        public void UndetectedMovement()
        {
            //Move Up
            if (isFacing(Direction.Up) && canMove(Direction.Up))
            {
                PosY--;
                steps++;
                RectY = GridPos.getPosition(Position, SubSquares.TopLeft).Y;
                if (steps > 2 && detected == false && !needToFindStart)
                {
                    steps = 0;
                    Facing = Direction.Right;
                }

            }
            //Move left
            else if (isFacing(Direction.Left) && canMove(Direction.Left))
            {
                PosX--;
                steps++;
                RectX = GridPos.getPosition(Position, SubSquares.TopLeft).X;
                if (steps > 2 && detected == false && !needToFindStart)
                {
                    steps = 0;
                    Facing = Direction.Up;
                }

            }
            //Move Down
            else if (isFacing(Direction.Down) && canMove(Direction.Down))
            {
                PosY++;
                steps++;
                RectY = GridPos.getPosition(Position, SubSquares.TopLeft).Y;
                if (steps > 2 && detected == false && !needToFindStart)
                {
                    steps = 0;
                    Facing = Direction.Left;
                }

            }
            //Move Right
            else if (isFacing(Direction.Right) && canMove(Direction.Right))
            {
                waitTime = .75;
                PosX++;
                steps++;
                RectX = GridPos.getPosition(Position, SubSquares.TopLeft).X;
                if (steps > 2 && detected == false && !needToFindStart)
                {
                    steps = 0;
                    Facing = Direction.Down;
                }

            }
        }

        public void Move()
        {
            //Move left
            if (isFacing(Direction.UpLeft) && canMove(Direction.UpLeft))
            {
                PosY--;
                RectY = GridPos.getPosition(Position, SubSquares.TopLeft).Y;
                PosX--;
                RectX = GridPos.getPosition(Position, SubSquares.TopLeft).X;
            }
            //move up and to the right
            else if (isFacing(Direction.UpRight) && canMove(Direction.UpRight))
            {
                PosY--;
                RectY = GridPos.getPosition(Position, SubSquares.TopLeft).Y;
                PosX++;
                RectX = GridPos.getPosition(Position, SubSquares.TopLeft).X;
            }
            //move down and to the left
            else if (isFacing(Direction.DownLeft) && canMove(Direction.DownLeft))
            {
                PosY++;
                RectY = GridPos.getPosition(Position, SubSquares.TopLeft).Y;
                PosX--;
                RectX = GridPos.getPosition(Position, SubSquares.TopLeft).X;
            }
            //move down and right
            else if (isFacing(Direction.DownRight) && canMove(Direction.DownRight))
            {
                PosY++;
                RectY = GridPos.getPosition(Position, SubSquares.TopLeft).Y;
                PosX++;
                RectX = GridPos.getPosition(Position, SubSquares.TopLeft).X;
            }
        }

        public void Astar()
        {


            Reset();

            SetUpGraph();



            SetUpNeighbors();


            #region Astar main loop for player
            //adds the start to the beginning
            Enqueue(locInGrid);

            //The main A* loop
            while (!IsEmpty() && (Peek().LocInGrid.X != playerPos.CurrentQuadTangle.LocInGrid.X || Peek().LocInGrid.Y != playerPos.CurrentQuadTangle.LocInGrid.Y))
            {
                current = Dequeue(0);
                closed.Add(current);

                float cost = G(current) + 10;

                //goes through the currents neighbor list to compare costs
                foreach (QuadTangle q in current.NeighborList)
                {
                    //compares cuurents cost to the tiles cost if the tile is still in open
                    if (open.Contains(q) && cost < G(q))
                    {
                        Dequeue(open.IndexOf(q));
                    }
                    //compares cost of current and tile if the tile is in closed
                    if (closed.Contains(q) && cost < G(q))
                    {
                        closed.Remove(q);
                    }
                    //if the tile isnt in open or in closed at this point, then set its parent to current, making it a part of the path, and move on
                    if (!open.Contains(q) && !closed.Contains(q))
                    {
                        q.DistanceFromStart = cost;
                        q.Rank = Fplayer(q);
                        q.Parent = current;
                        Enqueue(q);

                    }

                }

            }
            #endregion


            QuadTangle Pathq = playerPos.CurrentQuadTangle;
            List<QuadTangle> AstarList = new List<QuadTangle>();
            foreach (QuadTangle q in graph)
            {
                if (playerPos.CurrentQuadTangle.LocInGrid == q.LocInGrid)
                {
                    Pathq = q;
                    break;
                }
            }

            //makes a list of the path through parent nodes
            while (Pathq.Parent != null)
            {
                AstarList.Add(Pathq.Parent);
                Pathq = Pathq.Parent;
            }

            //reverses the list
            List<QuadTangle> AstarList2 = new List<QuadTangle>();
            for (int i = AstarList.Count - 1; i > -1; i--)
            {
                AstarList2.Add(AstarList[i]);
            }

            if (AstarList2.Count > 1)
                SetDirectionToMove(AstarList2[1]);
            else if (AstarList2.Count == 0)
            {
                detected = false;
                needToFindStart = true;
            }

        }
        #region Astar helper methods
        #region Most A star helper methods

        /// <summary>
        /// Resets all of the nodes for A*
        /// </summary>
        public void Reset()
        {
            foreach (QuadTangle q in graph)
            {

                q.Parent = null;
                q.NeighborList.Clear();
                q.ObjInTangle = null;
                q.Rank = 0;
                q.DistanceFromStart = 0;
                q.DistanceToEnd = 0;
                q.DownLeftNeighbor = null;
                q.DownNeighbor = null;
                q.DownRightNeighbor = null;
                q.LeftNeighbor = null;
                q.PathCost = 0;
                q.RightNeighbor = null;
                q.UpLeftNeighbor = null;
                q.UpNeighbor = null;
                q.UpRightNeighbor = null;

            }
            current = null;
            open.Clear();
            closed.Clear();
        }

        /// <summary>
        /// Sets up the graph 2d array of nodes
        /// </summary>
        public void SetUpGraph()
        {
            List<GameObject> objinspot = new List<GameObject>();

            foreach (QuadTangle Q in graph)
            {
                objinspot = gom.inSpot(Q.LocInGrid);
                for (int i = 0; i < objinspot.Count; i++)
                {
                    if (objinspot[i] is Player)
                    {
                        Q.ObjInTangle = objinspot[i];
                        //playerPos = Q;
                    }
                    else if (objinspot[i] is Wall && !(Q.ObjInTangle is Player))
                        Q.ObjInTangle = objinspot[i];
                    else if (objinspot[i] is Stalagmite)
                        Q.ObjInTangle = objinspot[i];
                    else if (objinspot[i] is Boulder && !(Q.ObjInTangle is Player))
                        Q.ObjInTangle = objinspot[i];
                    else if (objinspot[i] is Skull)
                        Q.ObjInTangle = objinspot[i];
                    else if (objinspot[i] is Enemy)
                        Q.ObjInTangle = objinspot[i];
                    else if (!(Q.ObjInTangle is Player))
                        Q.ObjInTangle = objinspot[i];
                }


            }
        }

        /// <summary>
        /// Sets up the neighbors of all of the nodes
        /// </summary>
        public void SetUpNeighbors()
        {
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                for (int j = 0; j < graph.GetLength(1); j++)
                {
                    if (i > 0)
                    {
                        //sets the neighbor to the left
                        if (graph[i - 1, j] != null && !(graph[i - 1, j].ObjInTangle is Wall) && !(graph[i - 1, j].ObjInTangle is Stalagmite) && !(graph[i - 1, j].ObjInTangle is Boulder) && !(graph[i - 1, j].ObjInTangle is Skull))
                        {
                            graph[i, j].LeftNeighbor = graph[i - 1, j];
                            graph[i, j].NeighborList.Add(graph[i, j].LeftNeighbor);
                        }
                    }
                    if (j > 0)
                    {
                        //sets the neighbor thats up
                        if (graph[i, j - 1] != null && !(graph[i, j - 1].ObjInTangle is Wall) && !(graph[i, j - 1].ObjInTangle is Stalagmite) && !(graph[i, j - 1].ObjInTangle is Boulder) && !(graph[i, j - 1].ObjInTangle is Skull))
                        {
                            graph[i, j].UpNeighbor = graph[i, j - 1];
                            graph[i, j].NeighborList.Add(graph[i, j].UpNeighbor);
                        }
                    }
                    if (i < graph.GetLength(0) - 1)
                    {
                        //sets the neighbor to the right
                        if (graph[i + 1, j] != null && !(graph[i + 1, j].ObjInTangle is Wall) && !(graph[i + 1, j].ObjInTangle is Stalagmite) && !(graph[i + 1, j].ObjInTangle is Boulder) && !(graph[i + 1, j].ObjInTangle is Skull))
                        {
                            graph[i, j].RightNeighbor = graph[i + 1, j];
                            graph[i, j].NeighborList.Add(graph[i, j].RightNeighbor);
                        }
                    }
                    if (j < graph.GetLength(1) - 1)
                    {
                        //sets the neighbor thats down
                        if (graph[i, j + 1] != null && !(graph[i, j + 1].ObjInTangle is Wall) && !(graph[i, j + 1].ObjInTangle is Stalagmite) && !(graph[i, j + 1].ObjInTangle is Boulder) && !(graph[i, j + 1].ObjInTangle is Skull))
                        {
                            graph[i, j].DownNeighbor = graph[i, j + 1];
                            graph[i, j].NeighborList.Add(graph[i, j].DownNeighbor);
                        }
                    }
                    if (i > 0 && j > 0)
                    {
                        if (graph[i - 1, j - 1] != null && !(graph[i - 1, j - 1].ObjInTangle is Wall) && !(graph[i - 1, j - 1].ObjInTangle is Stalagmite) && !(graph[i - 1, j - 1].ObjInTangle is Boulder) && !(graph[i - 1, j - 1].ObjInTangle is Skull))
                        {
                            graph[i, j].UpLeftNeighbor = graph[i - 1, j - 1];
                            graph[i, j].NeighborList.Add(graph[i, j].UpLeftNeighbor);
                        }
                    }
                    if (i > 0 && j < graph.GetLength(1) - 1)
                    {
                        if (graph[i - 1, j + 1] != null && !(graph[i - 1, j + 1].ObjInTangle is Wall) && !(graph[i - 1, j + 1].ObjInTangle is Stalagmite) && !(graph[i - 1, j + 1].ObjInTangle is Boulder) && !(graph[i - 1, j + 1].ObjInTangle is Skull))
                        {
                            graph[i, j].DownLeftNeighbor = graph[i - 1, j + 1];
                            graph[i, j].NeighborList.Add(graph[i, j].DownLeftNeighbor);
                        }
                    }
                    if (j > 0 && i < graph.GetLength(0) - 1)
                    {
                        if (graph[i + 1, j - 1] != null && !(graph[i + 1, j - 1].ObjInTangle is Wall) && !(graph[i + 1, j - 1].ObjInTangle is Stalagmite) && !(graph[i + 1, j - 1].ObjInTangle is Boulder) && !(graph[i + 1, j - 1].ObjInTangle is Skull))
                        {
                            graph[i, j].UpRightNeighbor = graph[i + 1, j - 1];
                            graph[i, j].NeighborList.Add(graph[i, j].UpRightNeighbor);
                        }
                    }
                    if (i < graph.GetLength(0) - 1 && j < graph.GetLength(1) - 1)
                    {
                        if (graph[i + 1, j + 1] != null && !(graph[i + 1, j + 1].ObjInTangle is Wall) && !(graph[i + 1, j + 1].ObjInTangle is Stalagmite) && !(graph[i + 1, j + 1].ObjInTangle is Boulder) && !(graph[i + 1, j + 1].ObjInTangle is Skull))
                        {
                            graph[i, j].DownRightNeighbor = graph[i + 1, j + 1];
                            graph[i, j].NeighborList.Add(graph[i, j].DownRightNeighbor);
                        }
                    }
                }
            }

        }

        /// <summary>
        /// The hueristic algorithm for player A*
        /// </summary>
        /// <param name="t">the tile</param>
        /// <returns></returns>
        private float Hplayer(QuadTangle q)
        {
            q.DistanceToEnd = (float)Math.Sqrt(Math.Pow(Math.Abs(playerPos.CurrentQuadTangle.LocInGrid.X - q.LocInGrid.X), 2) + Math.Pow(Math.Abs(playerPos.CurrentQuadTangle.LocInGrid.Y - q.LocInGrid.Y), 2));
            return q.DistanceToEnd;
        }

        /// <summary>
        /// The hueristic algorithm for start A*
        /// </summary>
        /// <param name="t">the tile</param>
        /// <returns></returns>
        private float Hstart(QuadTangle q)
        {
            q.DistanceToEnd = (float)Math.Sqrt(Math.Pow(Math.Abs(startPoint.X - q.LocInGrid.X), 2) + Math.Pow(Math.Abs(startPoint.Y - q.LocInGrid.Y), 2));
            return q.DistanceToEnd;
        }

        /// <summary>
        /// returns the cost from the start
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public float G(QuadTangle q)
        {
            return q.DistanceFromStart;
        }

        /// <summary>
        /// The total cost from the start to the end for player A*
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public float Fplayer(QuadTangle q)
        {
            current.PathCost = G(q) + Hplayer(q);
            return current.PathCost;
        }

        /// <summary>
        /// The total cost from the start to the end for start A*
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public float Fstart(QuadTangle q)
        {
            current.PathCost = G(q) + Hstart(q);
            return current.PathCost;
        }

        /// <summary>
        /// adds to the queue and sorts
        /// </summary>
        /// <param name="data">data being added</param>
        public void Enqueue(QuadTangle data)
        {
            open.Add(data);
            int index = open.Count - 1;
            int pIndex = (index - 1) / 2; ;
            while (open[pIndex].Rank > open[index].Rank)
            {
                QuadTangle temp = open[index];
                open[index] = open[pIndex];
                open[pIndex] = temp;
                index = pIndex;
                pIndex = (index - 1) / 2;
            }

        }

        /// <summary>
        /// removes from the heap and sorts
        /// </summary>
        /// <param name="index2"></param>
        /// <returns>index of data to remove</returns>
        public QuadTangle Dequeue(int index)
        {
            QuadTangle root = open[open.Count - 1];
            QuadTangle data = open[index];
            open[index] = root;
            open.RemoveAt(open.Count - 1);

            int child1 = index * 2 + 1;
            int child2 = index * 2 + 2;

            while (((child1 < open.Count) && open[index].Rank > open[child1].Rank) || ((child2 < open.Count) && (open[index].Rank) > (open[child2].Rank)))
            {
                child1 = index * 2 + 1;
                child2 = index * 2 + 2;
                if ((child2 >= open.Count) || (open[child1].Rank) < (open[child2].Rank))
                {
                    QuadTangle temp = open[index];
                    open[index] = open[child1];
                    open[child1] = temp;
                    index = child1;
                }
                else
                {
                    QuadTangle temp = open[index];
                    open[index] = open[child2];
                    open[child2] = temp;
                    index = child2;
                }
                child1 = index * 2 + 1;
                child2 = index * 2 + 2;

            }
            return data;

        }

        /// <summary>
        /// looks at first index of heap
        /// </summary>
        /// <returns></returns>
        public QuadTangle Peek()
        {
            return open[0];
        }

        public bool IsEmpty()
        {
            if (open.Count == 0)
                return true;

            return false;
        }
        #endregion
        public void SetDirectionToMove(QuadTangle q)
        {
            if (q != null)
            {
                if (q.LocInGrid.X < this.PosX && q.LocInGrid.Y < this.PosY)
                {
                    Facing = Direction.UpLeft;
                }
                else if (q.LocInGrid.X > this.PosX && q.LocInGrid.Y < this.PosY)
                {
                    Facing = Direction.UpRight;
                }
                else if (q.LocInGrid.X < this.PosX && q.LocInGrid.Y > this.PosY)
                {
                    Facing = Direction.DownLeft;
                }
                else if (q.LocInGrid.X > this.PosX && q.LocInGrid.Y > this.PosY)
                {
                    Facing = Direction.DownRight;
                }
                else if (q.LocInGrid.X < this.PosX)
                {
                    Facing = Direction.Left;
                }
                else if (q.LocInGrid.X > this.PosX)
                {
                    Facing = Direction.Right;
                }
                else if (q.LocInGrid.Y < this.PosY)
                {
                    Facing = Direction.Up;
                }
                else if (q.LocInGrid.Y > this.PosY)
                {
                    Facing = Direction.Down;
                }
            }
            else
            {

            }
        }
        #endregion

        public void Astart()
        {


            Reset();

            SetUpGraph();



            SetUpNeighbors();


            #region Astar main loop for start point
            //adds the start to the beginning
            Enqueue(locInGrid);

            //The main A* loop
            while (!IsEmpty() && (Peek().LocInGrid.X != startPoint.X || Peek().LocInGrid.Y != startPoint.Y))
            {
                current = Dequeue(0);
                closed.Add(current);

                float cost = G(current) + 10;

                //goes through the currents neighbor list to compare costs
                foreach (QuadTangle q in current.NeighborList)
                {
                    //compares cuurents cost to the tiles cost if the tile is still in open
                    if (open.Contains(q) && cost < G(q))
                    {
                        Dequeue(open.IndexOf(q));
                    }
                    //compares cost of current and tile if the tile is in closed
                    if (closed.Contains(q) && cost < G(q))
                    {
                        closed.Remove(q);
                    }
                    //if the tile isnt in open or in closed at this point, then set its parent to current, making it a part of the path, and move on
                    if (!open.Contains(q) && !closed.Contains(q))
                    {
                        q.DistanceFromStart = cost;
                        q.Rank = Fstart(q);
                        q.Parent = current;
                        Enqueue(q);

                    }

                }

            }
            #endregion


            QuadTangle Pathq = graph[startPoint.X, startPoint.Y];
            List<QuadTangle> AstarList = new List<QuadTangle>();


            //makes a list of the path through parent nodes
            while (Pathq.Parent != null)
            {
                AstarList.Add(Pathq.Parent);
                Pathq = Pathq.Parent;
            }

            //reverses the list
            List<QuadTangle> AstarList2 = new List<QuadTangle>();
            for (int i = AstarList.Count - 1; i > -1; i--)
            {
                AstarList2.Add(AstarList[i]);
            }

            if (AstarList2.Count > 1)
                SetDirectionToMove(AstarList2[1]);
            else if (AstarList2.Count == 0)
            {
                needToFindStart = false;
                Facing = Direction.Right;
            }

        }

        #region Unused pathfinding stuff
        public void Pathfinding(Player player)
        {
            Reset();
            SetUpGraph();
            SetUpNeighbors();

            int graphx = this.PosX;
            int graphy = this.PosY;
            bool done = false;
            #region find enemy in graph
            /*
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                for (int j = 0; j < graph.GetLength(1); j++)
                {
                    if (graph[i, j].LocInGrid == locInGrid.LocInGrid)
                    {
                        graphx = i;
                        graphy = j;
                        done = true;
                    }
                }
                if (done)
                    break;
            }
            */
            #endregion



            //up left
            if (player.PosX < this.PosX && player.PosY < this.PosY)
            {
                if (graph[graphx, graphy].UpLeftNeighbor != null)
                    Facing = Direction.UpLeft;
                else if (graph[graphx, graphy].UpNeighbor != null)
                    Facing = Direction.Up;
                else if (graph[graphx, graphy].LeftNeighbor != null)
                    Facing = Direction.Left;
                else if (graph[graphx, graphy].DownRightNeighbor != null)
                    Facing = Direction.DownRight;

            }   //up right
            else if (player.PosX > this.PosX && player.PosY < this.PosY)
            {
                if (graph[graphx, graphy].UpRightNeighbor != null)
                    Facing = Direction.UpRight;
                else if (graph[graphx, graphy].UpNeighbor != null)
                    Facing = Direction.Up;
                else if (graph[graphx, graphy].RightNeighbor != null)
                    Facing = Direction.Right;
                else if (graph[graphx, graphy].DownLeftNeighbor != null)
                    Facing = Direction.DownLeft;

            }   //down left
            else if (player.PosX < this.PosX && player.PosY > this.PosY)
            {
                if (graph[graphx, graphy].DownLeftNeighbor != null)
                    Facing = Direction.DownLeft;
                else if (graph[graphx, graphy].DownNeighbor != null)
                    Facing = Direction.Down;
                else if (graph[graphx, graphy].LeftNeighbor != null)
                    Facing = Direction.Left;
                else if (graph[graphx, graphy].UpRightNeighbor != null)
                    Facing = Direction.UpRight;

            }   //down right
            else if (player.PosX > this.PosX && player.PosY > this.PosY)
            {
                if (graph[graphx, graphy].DownRightNeighbor != null)
                    Facing = Direction.DownRight;
                else if (graph[graphx, graphy].DownNeighbor != null)
                    Facing = Direction.Down;
                else if (graph[graphx, graphy].RightNeighbor != null)
                    Facing = Direction.Right;
                else if (graph[graphx, graphy].UpLeftNeighbor != null)
                    Facing = Direction.UpLeft;

            }   //left
            else if (player.PosX < this.PosX)
            {
                if (graph[graphx, graphy].LeftNeighbor != null)
                    Facing = Direction.Left;
                else if (graph[graphx, graphy].UpLeftNeighbor != null)
                    Facing = Direction.UpLeft;
                else if (graph[graphx, graphy].DownLeftNeighbor != null)
                    Facing = Direction.DownLeft;
                else if (graph[graphx, graphy].UpNeighbor != null)
                    Facing = Direction.Up;
                else if (graph[graphx, graphy].DownNeighbor != null)
                    Facing = Direction.Down;
                else if (graph[graphx, graphy].RightNeighbor != null)
                    Facing = Direction.Right;

            }   //right
            else if (player.PosX > this.PosX)
            {
                if (graph[graphx, graphy].RightNeighbor != null)
                    Facing = Direction.Right;
                else if (graph[graphx, graphy].UpRightNeighbor != null)
                    Facing = Direction.UpRight;
                else if (graph[graphx, graphy].DownRightNeighbor != null)
                    Facing = Direction.DownRight;
                else if (graph[graphx, graphy].UpNeighbor != null)
                    Facing = Direction.Up;
                else if (graph[graphx, graphy].DownNeighbor != null)
                    Facing = Direction.Down;
                else if (graph[graphx, graphy].LeftNeighbor != null)
                    Facing = Direction.Left;

            }   //up
            else if (player.PosY < this.PosY)
            {
                if (graph[graphx, graphy].UpNeighbor != null)
                    Facing = Direction.Up;
                else if (graph[graphx, graphy].UpLeftNeighbor != null)
                    Facing = Direction.UpLeft;
                else if (graph[graphx, graphy].UpRightNeighbor != null)
                    Facing = Direction.UpRight;
                else if (graph[graphx, graphy].LeftNeighbor != null)
                    Facing = Direction.Left;
                else if (graph[graphx, graphy].RightNeighbor != null)
                    Facing = Direction.Right;
                else if (graph[graphx, graphy].DownNeighbor != null)
                    Facing = Direction.Down;

            }   //down
            else if (player.PosY > this.PosY)
            {
                if (graph[graphx, graphy].DownNeighbor != null)
                    Facing = Direction.Down;
                else if (graph[graphx, graphy].DownLeftNeighbor != null)
                    Facing = Direction.DownLeft;
                else if (graph[graphx, graphy].DownRightNeighbor != null)
                    Facing = Direction.DownRight;
                else if (graph[graphx, graphy].LeftNeighbor != null)
                    Facing = Direction.Left;
                else if (graph[graphx, graphy].RightNeighbor != null)
                    Facing = Direction.Right;
                else if (graph[graphx, graphy].UpNeighbor != null)
                    Facing = Direction.Up;
            }
        }

        public void ShortestPathAI(Player player)
        {
            if (player.PosX < this.PosX && player.PosY < this.PosY)
            {
                Facing = Direction.UpLeft;
            }
            else if (player.PosX > this.PosX && player.PosY < this.PosY)
            {
                Facing = Direction.UpRight;
            }
            else if (player.PosX < this.PosX && player.PosY > this.PosY)
            {
                Facing = Direction.DownLeft;
            }
            else if (player.PosX > this.PosX && player.PosY > this.PosY)
            {
                Facing = Direction.DownRight;
            }
            else if (player.PosX < this.PosX)
            {
                Facing = Direction.Left;
            }
            else if (player.PosX > this.PosX)
            {
                Facing = Direction.Right;
            }
            else if (player.PosY < this.PosY)
            {
                Facing = Direction.Up;
            }
            else if (player.PosY > this.PosY)
            {
                Facing = Direction.Down;
            }
        }
        #endregion

        public virtual void Draw(SpriteBatch batch)
        {
            if (IsActive)
            {
                if (Facing == Direction.Up)
                {
                    batch.Draw(this.ObjTexture, this.ObjRectangle, null, Color.White, 1.57f, new Vector2(0, 0), SpriteEffects.None, 0f);
                }
                else if (Facing == Direction.Left)
                {
                    batch.Draw(this.ObjTexture, this.ObjRectangle, Color.White);
                }
                else if (Facing == Direction.Right)
                {
                    batch.Draw(this.ObjTexture, this.ObjRectangle, null, Color.White, 3.14f, new Vector2(0, 0), SpriteEffects.None, 0f);
                }
                else if (Facing == Direction.Down)
                {
                    batch.Draw(this.ObjTexture, this.ObjRectangle, null, Color.White, 4.71f, new Vector2(0, 0), SpriteEffects.None, 0f);
                }
                else if (Facing == Direction.UpRight)
                {
                    batch.Draw(this.ObjTexture, this.ObjRectangle, null, Color.White, 2.36f, new Vector2(0, 0), SpriteEffects.None, 0f);
                }
                else if (Facing == Direction.UpLeft)
                {
                    batch.Draw(this.ObjTexture, this.ObjRectangle, null, Color.White, .8f, new Vector2(0, 0), SpriteEffects.None, 0f);
                }
                else if (Facing == Direction.DownRight)
                {
                    batch.Draw(this.ObjTexture, this.ObjRectangle, null, Color.White, 3.94f, new Vector2(0, 0), SpriteEffects.None, 0f);
                }
                else if (Facing == Direction.DownLeft)
                {
                    batch.Draw(this.ObjTexture, this.ObjRectangle, null, Color.White, 5.51f, new Vector2(0, 0), SpriteEffects.None, 0f);
                }
            }
        }
    }
}

