//Madison
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

    public class Button
    {
        private Rectangle rect;
        private Texture2D texture;
        bool selected;

        public Button(int x, int y, int width, int height, Texture2D texture)
        {
            rect = new Rectangle(x, y, width, height);
            this.texture = texture;
            selected = false;

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (selected == false)
            {
                spriteBatch.Draw(texture, rect, Color.White);
            }
            else
            {
                spriteBatch.Draw(texture, rect, Color.Red);
            }
        }
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

        public Texture2D Texture
        {
            get
            {
                return texture;
            }
            set
            {
                texture = value;
            }
        }

        public int X
        {
            get
            {
                return rect.X;
            }
            set
            {
                rect.X = value;
            }
        }

        public int Y
        {
            get
            {
                return rect.Y;
            }
            set
            {
                rect.Y = value;
            }
        }
        public bool Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;
            }
        }

    }
}
