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
    class AnimationFarm
    {
        //figure out which kind of animation to use
        bool isFlying;
        bool isIdle;
        Direction dir;
        Flying FlyingDirection;
        Idle IdleDirection;


        //constants DON'T MESS WITH EM
        //double ScaleDown = .01;
        //double ElapsedTime;

        //which sprite am I using?
        Texture2D spriteTexture;
        //time it will take before moving on to next frm
        float timer = 0f;
        //how often until it moves frm
        float interval = 200f;
        //what frame we on? (current 0-2 || 1-3)
        int currentFrame = 0;
        //how many frames per second
        int FramesPerSecond = 3;
        //how big are my sprites?
        int spriteWidth = 64;
        int spriteHeight = 64;
        //how fast it moves across the screen?
        float spriteSpeed = .6f;
        //sprite's rect
        Rectangle drawRect;
        //where will I draw the sprite and the center of the sprite
        Vector2 drawPosition;
        Vector2 origin;

        KeyboardState currentKB;
        KeyboardState previousKB;

        //Plugged in and taken from the parent class
        SpriteBatch sp;

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
        }

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

        /// <summary>
        /// Update to figure out what needs to be drawn
        /// </summary>
        /// <param name="gameTime">gametime for animation purposes</param>
        public void AnimationUpdate(GameTime gameTime)
        {
            previousKB = currentKB;
            currentKB = Keyboard.GetState();

            drawRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);

            if (currentKB.GetPressedKeys().Length==0)
            {
                currentFrame=0;
            }

            if (currentKB.IsKeyDown(Keys.A) == true)
            {
                FlyingDirection = Flying.West;
                
                drawPosition.X += spriteSpeed;
            }

            if (currentKB.IsKeyDown(Keys.D) == true)
            {
                FlyingDirection = Flying.East;

                drawPosition.X -= spriteSpeed;
            }

            

        }

        public void UpdateFrames(double timeElapsed)
        {

        }
        private void PlayerDraw(Direction dir)
        {
        }

    }//end class
}//end namespace


//http://www.dreamincode.net/forums/topic/194878-xna-animated-sprite/
//help number one
//http://msdn.microsoft.com/en-us/library/bb203866.aspx
//help number two