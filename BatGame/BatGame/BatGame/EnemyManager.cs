using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public void Update()
        {
            foreach (Enemy e in enemies)
            {
                e.Update();
            }
        }

        public int Count
        {
            get
            {
                return enemies.Count;
            }
        }
    }
}
