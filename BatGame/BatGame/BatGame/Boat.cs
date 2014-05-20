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
    class Boat
    {
        private Texture2D objTexture;
        private Rectangle objRectangle;
        Direction dir;
        GraphicsDevice graphics;

        public Boat(Texture2D texture, Rectangle rect, GraphicsDevice graphic)
        {
            objTexture = texture;
            objRectangle = rect;
            dir = Direction.Right;
            graphics = graphic;
        }

        public void Update()
        {
            if (objRectangle.X + objRectangle.Width > graphics.Viewport.Width)
            {
                dir = Direction.Left;
            }
            if (objRectangle.X < 0)
            {
                dir = Direction.Right;
            }
            if (dir == Direction.Right)
            {
                objRectangle.X += 1;
            }
            else
            {
                objRectangle.X -= 1;
            }
        }

        public void Draw(SpriteBatch batch)
        {
            //batch.Begin();
            batch.Draw(objTexture, objRectangle, Color.White);
            //batch.End();
        }
    }
}
