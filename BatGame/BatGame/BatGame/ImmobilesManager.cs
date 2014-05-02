//Wes and Joel, Madison
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
        public void IManagerDrawBack(SpriteBatch batch)
        {
            foreach (Immobiles i in immobiles)
            {
                if (i.DrawWeight == 1)
                {
                    i.Draw(batch);
                }
            }
        }

        public void IManagerDrawFront(SpriteBatch batch)
        {
            foreach (Immobiles i in immobiles)
            {
                if (i.DrawWeight == 2)
                {
                    i.Draw(batch);
                }
            }
        }

        public void IManagerUpdate()
        {
            foreach (Immobiles i in immobiles)
            {
                if (i is Interactable)
                {
                    i.Update();
                }
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

        public void Clear()
        {
            immobiles.Clear();
        }

    }
}

