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
    enum WallType
    {
        Up,
        Down,
        Left,
        Right,
        UpRight,
        UpLeft,
        DownRight,
        DownLeft,
        DownLeftCorner,
        DownRightCorner,
        UpLeftCorner,
        UpRightCorner,
    }
    class Wall : Immobiles
    {
        WallType type;

        public Wall(Texture2D t, GameObjectManager go, Point p, Grid g, Direction d, SubSquares sub, bool s)
            : base(t, go, p, g, d, sub, s)
        {
        }
        public WallType WallType
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

        public override void Draw(SpriteBatch batch)
        {
            switch (WallType)
            {
                case WallType.Up:
                    break;
                case WallType.Down:
                    break;
                case WallType.Left:
                    break;
                case WallType.Right:
                    break;
                case WallType.UpRight:
                    break;
                case WallType.UpLeft:
                    break;
                case WallType.DownRight:
                    break;
                case WallType.DownLeft:
                    break;
                case WallType.DownLeftCorner:
                    break;
                case WallType.DownRightCorner:
                    break;
                case WallType.UpLeftCorner:
                    break;
                case WallType.UpRightCorner:
                    break;
            }
            base.Draw(batch);
        }
    }
}