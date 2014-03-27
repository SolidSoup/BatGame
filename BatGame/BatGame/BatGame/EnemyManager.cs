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
    class EnemyManager
    {
        List<Enemy> enemies = new List<Enemy>();

        public EnemyManager()
        {

        }

        public void AddEnemy(Enemy e)
        {
            enemies.Add(e);
        }


        /// <summary>
        /// calls update on each of the enemies in the list
        /// </summary>
        public void EManagerUpdate(GameTime gameTime, Player player)
        {
            foreach (Enemy e in enemies)
            {
                e.EnemyUpdate(gameTime, player);
            }
        }

        /// <summary>
        /// Draws all enemies in the enemies list
        /// </summary>
        /// <param name="batch">spritebatch object</param>
        public void EManagerDraw(SpriteBatch batch)
        {
            foreach (Enemy e in enemies)
            {
                batch.Draw(e.ObjTexture, e.ObjRectangle, Color.White);
            }
        }

        public int Count
        {
            get
            {
                return enemies.Count;
            }
        }

        public List<Enemy> Enemies
        {
            get { return this.enemies; }
        }

    }
}
