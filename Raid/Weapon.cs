using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Raid
{
    public class Weapon : Entity
    {
        private int bullet;
        private bool isTaken = false;

        public Weapon(Texture2D texture, Rectangle rectangle, string name, int bullet) : base(texture, rectangle, name)
        {
            this.bullet = bullet > 0 ? bullet : 0;
        }

        public Weapon(Texture2D texture, Rectangle rectangle, int bullet) : base(texture, rectangle)
        {            
            this.bullet = bullet > 0 ? bullet : 0;
        }

        public bool IsTaken { get { return isTaken; } set { isTaken = value; } }        

        public int Bullet { get { return bullet; } set { bullet = bullet > 0 ? value : 0; } }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
