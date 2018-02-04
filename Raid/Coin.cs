using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Raid
{
    public class Coin : Entity
    {
        private int value;
        private bool isTaken = false;

        public Coin(Texture2D texture, Rectangle physic, int value) : base(texture, physic)
        {
            this.value = value >= 0? value : 0;
        }

        public bool IsTaken { get { return isTaken; } set { isTaken = value; } }

        public int Value { get { return value; } set { this.value = value; } }

        public override void Update(GameTime gameTime)
        {            
        }
    }
}
