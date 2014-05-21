//Wes and Joel
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
        List<AnimationFarm> enemyAnimations = new List<AnimationFarm>();

        public EnemyManager()
        {

        }

        public void AddEnemy(Enemy e)
        {
            enemies.Add(e);
            enemyAnimations.Add(new AnimationFarm(e.ObjTexture,0,48,48));
        }


        /// <summary>
        /// calls update on each of the enemies in the list
        /// </summary>
        public void EManagerUpdate(GameTime gameTime, Player player)
        {
            foreach (Enemy e in enemies)
            {
                e.EnemyUpdate(gameTime, player);
                foreach (AnimationFarm a in enemyAnimations)
                {
                    a.EnemyAnimationUpdate(gameTime, e.Facing);
                    a.EnemyFrameUpdate(gameTime, e.Facing);
                }
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
                //e.Draw(batch);
                foreach (AnimationFarm a in enemyAnimations)
                {
                    batch.Draw(e.ObjTexture, new Vector2((e.RectX + a.Origin.X), (e.RectY + a.Origin.Y)),
                         a.DrawRectangle, Color.White,
                         0f, a.Origin, 1, SpriteEffects.None, 0);
                }
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

        public void Clear()
        {
            enemies.Clear();
        }

    }
}
