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

namespace LevelEditor2
{
    
    public class Option
    {
        private Rectangle rect;
        private Texture2D texture;
        TileType type;
        bool selected;

        public Option(int x, int y, int width, int height, Texture2D texture, TileType type)
        {
            rect = new Rectangle(x, y, width, height);
            this.texture = texture;
            this.type = type;
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
        public string Name
        {
            get
            {
                return "" + type;
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
        public TileType Type
        {
            get
            {
                return type;
            }
        }

    }
}
