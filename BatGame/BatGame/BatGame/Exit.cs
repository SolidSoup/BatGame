using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BatGame
{
    class Exit : Interactable
    {
        ExitType type;
        public enum ExitType
        {
            Up,
            Down,
            Right,
            Left,
            Finish
        }
        public Exit(Texture2D t, GameObjectManager go, Point p, Grid g, Direction d, SubSquares sub, bool s, bool active, ExitType type)
            : base(t, go, p, g, d, sub, s, active)
        {
            this.IsSolid = false;
            this.type = type;
        }

        public ExitType Type
        {
            get { return type; }
            set { type = value; }
        }



    }
}