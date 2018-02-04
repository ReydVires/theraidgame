using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raid
{
    public abstract class Sprite
    {
        protected Texture2D texture;
        private Vector2 velocity;
        public Rectangle physic;

        public Sprite(Texture2D texture, Rectangle physic)
        {
            this.texture = texture;
            this.physic = physic;
            velocity = new Vector2(physic.X, physic.Y);
        }

        public Vector2 Velocity
        {
            get
            {
                return velocity;
            }
            set
            {
                velocity = value;
                physic.X = (int)value.X;
                physic.Y = (int)value.Y;
            }
        }

        public Texture2D Image { get { return texture; } set { texture = value; } }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, physic, Color.White);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Texture2D image)
        {
            spriteBatch.Draw(image, physic, Color.White);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Texture2D image, Color color)
        {
            spriteBatch.Draw(image, physic, color);
        }
    }
}
