﻿//Wes and Joel, Madison
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
    class Interactable : Immobiles
    {
        bool isActive;

        public Interactable(Texture2D t, GameObjectManager go, Point p, Grid g, Direction d, SubSquares sub, bool s, bool a)
            : base(t, go, p, g, d, sub, s)
        {
            this.isActive = a;
        }

        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;
            }
        }

    }
}
