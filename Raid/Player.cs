using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Raid
{
    public class Player : Entity, IControl
    {
        public enum Direction
        {
            right,
            bottom,
            left,
            top
        }
        public Direction dir;
        Direction stateDir = Direction.right;
        Weapon equip;
        public Life[] life;
        bool fire = false, dropItem = false, hasWeapon;
        bool isDead = false;
        int speed = 4, health = 3;
        float bulletTravel = 0;

        public Bullet bullet;        

        public Player(Texture2D texture, Rectangle rectangle) : base(texture, rectangle)
        {            
            Name = "Player 1";
            life = new Life[health];
        }

        public override void Update(GameTime gameTime)
        {
            Move();
            Action(gameTime);
        }

        public void Move()
        {
            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Right) && physic.Right < Main.winWidth)
            {
                Velocity = new Vector2(Velocity.X + speed, Velocity.Y);
                dir = Direction.right;
            }            
            else if (keyboard.IsKeyDown(Keys.Up) && physic.Top > 64)
            {
                Velocity = new Vector2(Velocity.X, Velocity.Y - speed);
                dir = Direction.top;
            }
            else if (keyboard.IsKeyDown(Keys.Left) && physic.Left > 0)
            {
                Velocity = new Vector2(Velocity.X - speed, Velocity.Y);
                dir = Direction.left;
            }
            else if (keyboard.IsKeyDown(Keys.Down) && physic.Bottom < Main.winHeight)
            {
                Velocity = new Vector2(Velocity.X, Velocity.Y + speed);
                dir = Direction.bottom;
            }
            
        }

        public void Action(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();            

            if (keyboard.IsKeyDown(Keys.X) && hasWeapon && !dropItem)
            {
                hasWeapon = false;
                equip = null;
                dropItem = true;
            }
            if (keyboard.IsKeyUp(Keys.X))
            {
                dropItem = false;
            }

            if (keyboard.IsKeyDown(Keys.Z) && !fire)
            {
                if (hasWeapon && equip.Bullet > 0)
                {
                    stateDir = dir;
                    bulletTravel = 180f;
                    Console.WriteLine("Ammo : {0}", --equip.Bullet);
                    fire = true;                    
                    bullet = new Bullet(Main.srcBullet, new Rectangle((int)Velocity.X, (int)Velocity.Y,
                        Main.srcBullet.Width, Main.srcBullet.Height));                    
                }                 
            }
            if (bulletTravel > 0)
            {
                var bulletMove = bullet.Velocity;
                var bulletSpeed = bullet.BulletSpeed;

                switch (stateDir)
                {
                    case Direction.right:
                        bulletMove = new Vector2(bulletMove.X + bulletSpeed * Main.deltaTime, bulletMove.Y);
                        break;
                    case Direction.left:
                        bulletMove = new Vector2(bulletMove.X - bulletSpeed * Main.deltaTime, bulletMove.Y);
                        break;
                    case Direction.top:
                        bulletMove = new Vector2(bulletMove.X, bulletMove.Y - bulletSpeed * Main.deltaTime);
                        break;
                    case Direction.bottom:
                        bulletMove = new Vector2(bulletMove.X, bulletMove.Y + bulletSpeed * Main.deltaTime);
                        break;
                }
                bullet.Velocity = bulletMove;
                bulletTravel -= Main.deltaTime;
            }
            else if (bulletTravel < 0)
                bullet.IsShooted = true;
            if (keyboard.IsKeyUp(Keys.Z))
            {
                fire = false;
            }            
        }

        public Weapon Equipment
        {
            get { return hasWeapon == true ? equip : null; }
            set
            {
                if (!hasWeapon)
                {
                    Console.WriteLine("Get weapon!");
                    equip = value;
                    hasWeapon = true;
                }
                else
                    Console.WriteLine("{0} has weapon!", Name);
            }
        }       

        public bool HasWeapon { get { return hasWeapon; } set { hasWeapon = value; } }

        public bool IsDead { get { return isDead; } }

        public void Damaged(int val)
        {
            if (health - val > 0)
            {
                health -= val;
            }
            else
            {
                isDead = true;
                health = 0;
            }
        }

        public int HP { get { return health; } }
        
    }
}