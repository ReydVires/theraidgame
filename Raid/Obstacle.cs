using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Raid
{
    public class Obstacle : Entity
    {
        public Obstacle(Texture2D texture, Rectangle physic) : base(texture, physic)
        {
        }

        public override void Update(GameTime gameTime)
        {            
        }
    }
}
