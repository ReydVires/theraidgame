using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Raid
{
    public class Bullet : Entity
    {
        private bool shooted = false, destroy = false;
        private float bulletSpd = 2f;

        public Bullet(Texture2D texture, Rectangle physic) : base(texture, physic)
        {            
        }

        public override void Update(GameTime gameTime)
        {            
            foreach (Obstacle w in Level.walls)
            {
                if (physic.Intersects(w.physic))
                {
                    destroy = true;
                }
            }

            foreach (Enemy e in Level.enemies)
            {
                if (physic.Intersects(e.physic) &&
                    !e.IsDead && !destroy && !shooted)
                {
                    destroy = true;
                    e.IsDead = true;
                    Main.gameScore += e.Value;
                }
            }
        }

        public float BulletSpeed { get { return bulletSpd; } set { bulletSpd = value; } }

        public bool IsShooted { get { return shooted; } set { shooted = value; } }

        public bool IsDestroy { get { return destroy; } set { destroy = value; } }
    }
}
