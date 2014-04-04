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
        double spriteSpeed = .6;
        //sprite's rect
        Rectangle drawRect;
        //where will I draw the sprite and the center of the sprite
        Vector2 drawPosition;
        Vector2 origin;

        //Plugged in and taken from the parent class
        SpriteBatch sp;

        /// <summary>
        /// Initialize the Animation class
        /// </summary>
        /// <param name="sprite">sprite batch to have access to sprite sheets</param>
        public AnimationFarm(SpriteBatch sprite)
        {
            sp = sprite;
            isFlying = false;
            isIdle = false;
        }

        public void AnimationUpdate(GameTime gameTime)
        {

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