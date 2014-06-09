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

        public Enemy(Texture2D t, GameObjectManager go, Point p, Grid g, Direction d, SubSquares sub, bool s, double speed, double m, bool a)
            : base(t, go, p, g, d, sub, s, m, speed, a)
        {
            this.waitTime = .75;

        }
        
        //meant to be overriden for each enemy
        public virtual void EnemyUpdate(GameTime gameTime, Player player)
        {

        }

        #region Movement

        /// <summary>
        /// Handles all movement
        /// </summary>
        public virtual void Move()
        {
            //Move Up
            if (isFacing(Direction.Up) && canMove(Direction.Up))
            {
                PosY--;
                RectY = GridPos.getPosition(Position, SubSquares.TopLeft).Y;
            }
            //Move left
            else if (isFacing(Direction.Left) && canMove(Direction.Left))
            {
                PosX--;
                RectX = GridPos.getPosition(Position, SubSquares.TopLeft).X;
            }
            //Move Down
            else if (isFacing(Direction.Down) && canMove(Direction.Down))
            {
                PosY++;
                RectY = GridPos.getPosition(Position, SubSquares.TopLeft).Y;
            }
            //Move Right
            else if (isFacing(Direction.Right) && canMove(Direction.Right))
            {
                PosX++;
                RectX = GridPos.getPosition(Position, SubSquares.TopLeft).X;
            }

            //Move up and to the left
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
            waitTime = .75;
        }
        #endregion
        

        public virtual void Draw(SpriteBatch batch)
        {
            if (IsActive)
            {
                //--------------------------------------------------- This has been fixed with enemy animation
                //Glitch Report:
                //The enemy image is being drawn in the correct location.
                //The rotation code in spritebatch's draw is moving where the drawing point is
                //so we are having the drawing point at the bottom right instead of top left like
                //it should.
                //We have a temporary fix in place right now that moves where the drawing point
                //is so that it will be in the top left again.
                //This problem should be fixed permanently when Karen uploads her drawing stuff.
                //END REPORT
                //---------------------------------------------------
                /*if (Facing == Direction.Up)
                {
                    batch.Draw(this.ObjTexture, this.ObjRectangle, null, Color.White, 1.57f, new Vector2(32, 32), SpriteEffects.None, 0f);
                }
                else if (Facing == Direction.Left)
                {
                    batch.Draw(this.ObjTexture, this.ObjRectangle, Color.White);
                }
                else if (Facing == Direction.Right)
                {
                    batch.Draw(this.ObjTexture, this.ObjRectangle, null, Color.White, 3.14f, new Vector2(32, 32), SpriteEffects.None, 0f);
                }
                else if (Facing == Direction.Down)
                {
                    batch.Draw(this.ObjTexture, this.ObjRectangle, null, Color.White, 4.71f, new Vector2(32, 32), SpriteEffects.None, 0f);
                }
                else if (Facing == Direction.UpRight)
                {
                    batch.Draw(this.ObjTexture, this.ObjRectangle, null, Color.White, 2.36f, new Vector2(32, 32), SpriteEffects.None, 0f);
                }
                else if (Facing == Direction.UpLeft)
                {
                    batch.Draw(this.ObjTexture, this.ObjRectangle, null, Color.White, .8f, new Vector2(32, 32), SpriteEffects.None, 0f);
                }
                else if (Facing == Direction.DownRight)
                {
                    batch.Draw(this.ObjTexture, this.ObjRectangle, null, Color.White, 3.94f, new Vector2(32, 32), SpriteEffects.None, 0f);
                }
                else if (Facing == Direction.DownLeft)
                {
                    batch.Draw(this.ObjTexture, this.ObjRectangle, null, Color.White, 5.51f, new Vector2(32, 32), SpriteEffects.None, 0f);
                }*/

                
            }
        }
    }
}

