//Karen
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
    enum Flying
    {
        North,
        East,
        South,
        West,
        Northeast,
        Northwest,
        Southeast,
        Southwest
    }
    enum Idle
    {
        North,
        East,
        South,
        West,
        Northeast,
        Northwest,
        Southeast,
        Southwest
    }
    class AnimationFarm : Game1
    {

        #region VARIABLES
        //figure out which kind of animation to use
        bool isFlying;
        bool isIdle;
        Direction dir;
        Flying flyingDirection;
        Idle IdleDirection;


        //constants DON'T MESS WITH EM
        //double ScaleDown = .01;
        //double ElapsedTime;

        //which sprite am I using?
        Texture2D spriteTexture;
        //time it will take before moving on to next frm
        double timer = 0.0;
        //what frame we on? (current 0-2 || 1-3)
        int currentFrame = 0;
        //how often until it moves frm
        double interval;
        //how many frames per second
        int FramesPerSecond = 60;
        //how big are my sprites?
        int spriteWidth = 32;
        int spriteHeight = 32;
        //how fast it moves across the screen?
        float spriteSpeed;
        //sprite's rect
        Rectangle drawRect;
        //where will I draw the sprite and the center of the sprite
        Vector2 drawPosition;
        Vector2 origin;

        //rotation float
        private float rotationAngle = 45.0F;

        KeyboardState currentKB;
        KeyboardState previousKB;

        //Plugged in and taken from the parent class
        SpriteBatch sp;

        #endregion

        /// <summary>
        /// Initialize the Animation class
        /// </summary>
        /// <param name="sprite">sprite batch to have access to sprite sheets</param>
        public AnimationFarm(Texture2D text, int curfrm, int sprWid, int sprHgt)
        {
            this.SpriteTexture = text;
            this.currentFrame = curfrm;
            this.spriteWidth = sprWid;
            this.spriteHeight = sprHgt;
            isFlying = false;
            isIdle = false;
            interval = 10.0 / FramesPerSecond;
            spriteSpeed = FramesPerSecond / spriteWidth;
        }

        #region PROPERTIES

        //where the sprite will be drawn
        public Vector2 DrawPosition
        {
            get
            {
                return drawPosition;
            }
            set
            {
                drawPosition = value;
            }
        }

        //the center of a sprite
        public Vector2 Origin
        {
            get
            {
                return origin;
            }
            set
            {
                origin = value;
            }
        }

        //current sprite in use
        public Texture2D SpriteTexture
        {
            get
            {
                return spriteTexture;
            }
            set
            {
                spriteTexture = value;
            }
        }

        //will draw the rectangle around the sprite necessary to get from spr sheet
        public Rectangle DrawRectangle
        {
            get
            {
                return drawRect;
            }
            set
            {
                drawRect = value;
            }
        }

        public Flying FlyDirection
        {
            get
            {
                return flyingDirection;
            }
            set
            {
                flyingDirection = value;
            }
        }

        #endregion

        /// <summary>
        /// Update to figure out what needs to be drawn
        /// </summary>
        /// <param name="gameTime">gametime for animation purposes</param>
        public void AnimationUpdate(GameTime gameTime, Direction d)
        {
            previousKB = currentKB;
            currentKB = Keyboard.GetState();

            drawRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);
            Origin = new Vector2(spriteWidth / 2, spriteHeight / 2);

            if (currentKB.GetPressedKeys().Length == 0)
            {
                if (currentFrame > 0 && currentFrame < 2)//up
                {
                    currentFrame = 0;
                    //interval = 15.0 / FramesPerSecond;
                }
                else if (currentFrame > 3 && currentFrame < 5)//down
                {
                    currentFrame = 3;
                    //interval = 15.0 / FramesPerSecond;
                }
                else if (currentFrame > 6 && currentFrame < 8)//left
                {
                    currentFrame = 6;
                    //interval = 15.0 / FramesPerSecond;
                }
                else if (currentFrame > 9 && currentFrame < 11)//right
                {
                    currentFrame = 9;
                    //interval = 15.0 / FramesPerSecond;
                }
                else if (currentFrame > 12 && currentFrame < 14)//upright
                {
                    currentFrame = 12;
                    //interval = 15.0 / FramesPerSecond;
                }
                else if (currentFrame > 15 && currentFrame < 17)//upleft
                {
                    currentFrame = 15;
                    //interval = 15.0 / FramesPerSecond;
                }
                else if (currentFrame > 18 && currentFrame < 20)//downright
                {
                    currentFrame = 18;
                    //interval = 15.0 / FramesPerSecond;
                }
                else if (currentFrame > 21 && currentFrame < 23)//downleft
                {
                    currentFrame = 21;
                    //interval = 15.0 / FramesPerSecond;
                }
            }

            if (d == Direction.Left)
            {
                currentFrame = 6;
                interval = 15.0 / FramesPerSecond;
                //PlayerFlyingAnimation(FlyingDirection, gameTime);
                //drawPosition.X += spriteSpeed;
            }

            else if (d == Direction.Right)
            {
                currentFrame = 9;
                interval = 15.0 / FramesPerSecond;
                //PlayerFlyingAnimation(FlyingDirection, gameTime);
                //drawPosition.X -= spriteSpeed;
            }
            else if (d == Direction.Down)
            {
                currentFrame = 3;
                interval = 15.0 / FramesPerSecond;
                //PlayerFlyingAnimation(FlyingDirection, gameTime);
            }
            else if (d == Direction.Up)
            {
                currentFrame = 0;
                interval = 15.0 / FramesPerSecond;
                //PlayerFlyingAnimation(FlyingDirection, gameTime);
            }
            else if (d == Direction.UpRight)
            {
                currentFrame = 12;
                interval = 15.0 / FramesPerSecond;
            }
            else if (d == Direction.UpLeft)
            {
                currentFrame = 15;
                interval = 15.0 / FramesPerSecond;
            }
            else if (d == Direction.DownRight)
            {
                currentFrame = 18;
                interval = 15.0 / FramesPerSecond;
            }
            else if (d == Direction.DownLeft)
            {
                currentFrame = 21;
                interval = 15.0 / FramesPerSecond;
            }

        }

        public void PlayerFlyingAnimation(Direction d, GameTime gameTime)
        {
            if (d == Direction.Up)//
            {
                if (currentKB != previousKB)
                {
                    currentFrame = 0;// if the key is no longer held
                }

                timer += gameTime.ElapsedGameTime.TotalSeconds;

                if (timer > interval)
                {
                    currentFrame++; //continue the animation

                    if (currentFrame > 2) //beyond the animation count held for flying (3)
                        currentFrame = 0; //reset the animation

                    timer = 0;
                }
            }

            else if (d == Direction.Down)//
            {
                if (currentKB != previousKB)
                {
                    currentFrame = 3;// if the key is no longer held
                }

                timer += gameTime.ElapsedGameTime.TotalSeconds;

                if (timer > interval)
                {
                    currentFrame++; //continue the animation

                    if (currentFrame > 5)
                        currentFrame = 3; //reset the animation

                    timer = 0;
                }
            }

            else if (d == Direction.Left)
            {
                if (currentKB != previousKB)
                {
                    currentFrame = 6;// if the key is no longer held
                }

                timer += gameTime.ElapsedGameTime.TotalSeconds;

                if (timer > interval)
                {
                    currentFrame++; //continue the animation

                    if (currentFrame > 8)
                        currentFrame = 6; //reset the animation

                    timer = 0;
                }
            }

            else if (d == Direction.Right)
            {
                if (currentKB != previousKB)
                {
                    currentFrame = 9;// if the key is no longer held
                }

                timer += gameTime.ElapsedGameTime.TotalSeconds;

                if (timer > interval)
                {
                    currentFrame++; //continue the animation

                    if (currentFrame > 11)
                        currentFrame = 9; //reset the animation

                    timer = 0;
                }
            }

            else if (d == Direction.UpRight)
            {
                if (currentKB != previousKB)
                {
                    currentFrame = 12;// if the key is no longer held
                }

                timer += gameTime.ElapsedGameTime.TotalSeconds;

                if (timer > interval)
                {
                    currentFrame++; //continue the animation

                    if (currentFrame > 14)
                        currentFrame = 12; //reset the animation

                    timer = 0;
                }
            }

            else if (d == Direction.UpLeft)
            {
                if (currentKB != previousKB)
                {
                    currentFrame = 15;// if the key is no longer held
                }

                timer += gameTime.ElapsedGameTime.TotalSeconds;

                if (timer > interval)
                {
                    currentFrame++; //continue the animation

                    if (currentFrame > 17)
                        currentFrame = 15; //reset the animation

                    timer = 0;
                }
            }

            else if (d == Direction.DownRight)
            {
                if (currentKB != previousKB)
                {
                    currentFrame = 18;// if the key is no longer held
                }

                timer += gameTime.ElapsedGameTime.TotalSeconds;

                if (timer > interval)
                {
                    currentFrame++; //continue the animation

                    if (currentFrame > 20)
                        currentFrame = 18; //reset the animation

                    timer = 0;
                }
            }

            else if (d == Direction.DownLeft)
            {
                if (currentKB != previousKB)
                {
                    currentFrame = 21;// if the key is no longer held
                }

                timer += gameTime.ElapsedGameTime.TotalSeconds;

                if (timer > interval)
                {
                    currentFrame++; //continue the animation

                    if (currentFrame > 23)
                        currentFrame = 21; //reset the animation

                    timer = 0;
                }
            }

        }//end method

    }//end class
}//end namespace


//http://www.dreamincode.net/forums/topic/194878-xna-animated-sprite/
//help number one
//http://msdn.microsoft.com/en-us/library/bb203866.aspx
//help number two