using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Raid
{
    public class Level : Entity
    {
        public static int level;
        public static List<Obstacle> walls;
        public static List<Coin> coins;
        public static List<Weapon> weapons;
        public static List<Enemy> enemies;
        public Player player;
        public Obstacle ladder;        

        public Level(Texture2D texture, Rectangle physic, Player player) : base(texture, physic)
        {
            level++;
            this.player = player;
            walls = new List<Obstacle>();
            coins = new List<Coin>();
            enemies = new List<Enemy>();
            weapons = new List<Weapon>();
        }

        public Player Player { get { return player; } }

        public void AddWall(Texture2D image, Rectangle phisyc)
        {
            walls.Add(new Obstacle(image,phisyc));
        }

        public void AddCoin(Texture2D image, Rectangle physic, int val)
        {
            coins.Add(new Coin(image, physic, val));
        }

        public void AddEnemy(Texture2D image, Rectangle physic)
        {
            enemies.Add(new Enemy(image, physic));
        }

        public void AddEnemy(Texture2D image, Rectangle physic, float spd)
        {
            enemies.Add(new Enemy(image, physic,spd));
        }

        public void AddEnemy(Texture2D image, Rectangle physic, float spd, int value)
        {
            enemies.Add(new Enemy(image, physic, spd, value));
        }

        public void AddWeapon(Texture2D image, Rectangle physic, string name, int ammo)
        {
            weapons.Add(new Weapon(image, physic, name, ammo));
        }

        public void AddWeapon(Texture2D image, Rectangle physic, int ammo)
        {
            weapons.Add(new Weapon(image, physic, ammo));
        }

        public Coin GetCoin(int i)
        {
            return (i < coins.Capacity && i >= 0) ? coins[i] : null;
        }

        public Weapon GetWeapon(int i)
        {
            return (i < weapons.Capacity && i >= 0) ? weapons[i] : null;
        }

        public Enemy GetEnemy(int i)
        {
            return (i < enemies.Capacity && i >= 0) ? enemies[i] : null;
        }        

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            if (player.IsDead)
            {
                Main.gameState = Main.GameState.GameOver;
            }            

            foreach (Weapon w in weapons)
            {
                if (player.physic.Intersects(w.physic) && !w.IsTaken && !player.HasWeapon)
                {
                    player.Equipment = w;
                    w.IsTaken = true;
                }
            }

            foreach (Coin c in coins)
            {
                if (player.physic.Intersects(c.physic) && !c.IsTaken)
                {
                    c.IsTaken = true;
                    Main.gameScore += c.Value;
                }
            }

            foreach (Enemy e in enemies)
            {
                if (player.physic.Intersects(e.physic) && !e.IsDead)
                {
                    player.Damaged(1);
                    player.Velocity = new Vector2(0, 64);
                    player.dir = Player.Direction.right;
                }
            }

            if (player.physic.Intersects(ladder.physic))
            {
                Main.gameState = Main.GameState.GameWin;
            }

            if (player.bullet != null)
            {
                player.bullet.Update(gameTime);
            }

            foreach(Obstacle wall in walls)
            {
                if (player.physic.Intersects(wall.physic))
                {
                    switch (player.dir)
                    {
                        case Player.Direction.bottom:
                            player.Velocity = new Vector2(player.Velocity.X, wall.physic.Top - 64);
                            break;
                        case Player.Direction.top:
                            player.Velocity = new Vector2(player.Velocity.X, wall.physic.Bottom);
                            break;
                        case Player.Direction.left:
                            player.Velocity = new Vector2(wall.physic.Right, player.Velocity.Y);
                            break;
                        case Player.Direction.right:
                            player.Velocity = new Vector2(wall.physic.Left - 32, player.Velocity.Y);
                            break;
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            foreach (Obstacle wall in walls)
            {
                wall.Draw(spriteBatch);
            }

            foreach (Weapon weapon in weapons)
            {
                if (!weapon.IsTaken)
                {
                    weapon.Draw(spriteBatch);
                }
            }

            foreach (Coin coin in coins)
            {
                if (!coin.IsTaken)
                {
                    coin.Draw(spriteBatch);
                }                
            }

            var str = "No equipment";
            if (player.HasWeapon)
            {
                var bullet = player.Equipment.Bullet;
                str = "Ammo [" + player.Equipment.Name + "] : " + bullet;

                if (bullet <= 0)
                {
                    str = "No Ammo";
                }                

                if (player.bullet != null && (!player.bullet.IsShooted && !player.bullet.IsDestroy))                    
                {
                    player.bullet.Draw(spriteBatch);
                }
            }
            spriteBatch.DrawString(Main.ftAmmo, str, new Vector2(200, 14), Color.White);
            ladder.Draw(spriteBatch);            
            
            if (player.HP > 0)
            {
                player.Draw(spriteBatch);
            }

            foreach (Enemy enemy in enemies)
            {
                if (!enemy.IsDead)
                {
                    enemy.Draw(spriteBatch);
                }
            }

            for (int i = 0; i < player.HP; i++)
            {
                player.life[i].Draw(spriteBatch);
            }
        }
    }
}
