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
    class ImmobilesManager
    {
        List<Immobiles> immobiles = new List<Immobiles>();

        public ImmobilesManager()
        {

        }

        public void AddImmobile(Immobiles i)
        {
            immobiles.Add(i);
        }

        /// <summary>
        /// Draws all immobilies
        /// </summary>
        /// <param name="batch">spritebatch object</param>
        public void IManagerDraw(SpriteBatch batch)
        {
            foreach (Immobiles i in immobiles)
            {
                batch.Draw(i.ObjTexture, i.ObjRectangle, Color.White);
            }
        }

        public int Count
        {
            get
            {
                return immobiles.Count;
            }
        }

        public List<Immobiles> Immobiles
        {
            get { return this.immobiles; }
        }

    }
}
