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
    class Tile
    {
        private Rectangle rect;
        private Texture2D texture;
        bool highlight;
        TileType type;
        int gridX;
        int gridY;

        public Tile(int x, int y, int width, int height, Texture2D texture, int gridX, int gridY)
        {
            rect = new Rectangle(x, y, width, height);
            this.texture = texture;
            highlight = false;
            this.gridX = gridX;
            this.gridY = gridY;
            type = TileType.blank;

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (highlight == false)
            {
                spriteBatch.Draw(texture, rect, Color.White);
            }
            else
            {
                spriteBatch.Draw(texture, rect, Color.Red);
            }
        }
        public void Update()
        {

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
        public bool Highlight
        {
            get
            {
                return highlight;
            }
            set
            {
                highlight = value;
            }
        }
        public TileType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }
        public int GridX
        {
            get
            {
                return gridX;
            }
        }
        public int GridY
        {
            get
            {
                return gridY;
            }
        }

    }
}