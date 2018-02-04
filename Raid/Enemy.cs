using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Raid
{
    public class Enemy : Entity
    {
        float speed = 0.2f;
        bool facingRight = true, facingBot = true, dead = false;
        int value = 1;

        public Enemy(Texture2D texture, Rectangle physic) : base(texture, physic)
        {            
        }

        public Enemy(Texture2D texture, Rectangle physic, float speed) : base(texture, physic)
        {
            this.speed = speed;
        }

        public Enemy(Texture2D texture, Rectangle physic, float speed, int val) : base(texture, physic)
        {
            this.speed = speed;
            value = val;
        }

        public void Moving(float deltaTime, float Min, float Max, string dir)
        {
            switch (dir)
            {
                case "Horizontal":
                case "H":
                    if (facingRight)
                        Velocity = new Vector2(Velocity.X + speed * deltaTime, Velocity.Y);
                    else
                        Velocity = new Vector2(Velocity.X - speed * deltaTime, Velocity.Y);
                    if (Velocity.X <= Min && !facingRight)
                        facingRight = true;
                    else if (facingRight && Velocity.X >= Max)
                        facingRight = false;
                    break;
                case "Vertical":
                case "V":
                    if (facingBot)
                        Velocity = new Vector2(Velocity.X, Velocity.Y + speed * deltaTime);
                    else
                        Velocity = new Vector2(Velocity.X, Velocity.Y - speed * deltaTime);
                    if (Velocity.Y <= Min && !facingBot)
                        facingBot = true;
                    else if (facingRight && Velocity.Y >= Max)
                        facingBot = false;
                    break;
            }
        }

        public bool IsDead { get { return dead; } set { dead = value; } }

        public int Value { get { return value; } }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
