using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Raid
{
    public abstract class Entity : Sprite
    {
        private string name;

        public Entity(Texture2D texture, Rectangle physic) : base(texture, physic)
        {
            name = "Unnamed";
        }

        public Entity(Texture2D texture, Rectangle physic, string name) : base(texture, physic)
        {
            this.name = name;
        }

        public abstract void Update(GameTime gameTime);

        public string Name { get { return name; } set { name = value; } }
    }
}
