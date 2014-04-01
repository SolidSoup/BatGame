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
    class GameObjectManager
    {
        List<GameObject> gameObjects = new List<GameObject>();

        public GameObjectManager()
        {

        }

        public void AddGameObject(GameObject g)
        {
            gameObjects.Add(g);
        }

        public void GManagerUpdate(GameTime gameTime, Player player)
        {
            foreach (GameObject g in gameObjects)
            {
                if (g is Enemy)
                {
                    Enemy e = (Enemy)g;
                    e.EnemyUpdate(gameTime, player);
                }
                else
                {

                    //g.Update(gameTime, player);
                }
            }
        }

        /// <summary>
        /// Draws all gameObjects
        /// </summary>
        /// <param name="batch">spritebatch object</param>
        public void GManagerDraw(SpriteBatch batch)
        {
            foreach (GameObject g in gameObjects)
            {
                batch.Draw(g.ObjTexture, g.ObjRectangle, Color.White);
            }
        }

        public bool spotTaken(Point moveLocation)
        {
            //Console.WriteLine("here");
            foreach (GameObject g in gameObjects)
            {
                if (g.Position == moveLocation && g.IsSolid)
                {
                    return true;
                }
            }
            return false;
        }

        public int Count
        {
            get
            {
                return gameObjects.Count;
            }
        }

        public List<GameObject> GameObjects
        {
            get { return this.gameObjects; }
        }

    }
}
