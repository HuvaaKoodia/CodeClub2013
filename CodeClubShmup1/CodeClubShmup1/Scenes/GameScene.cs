using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeClubShmup1.Components;
using CodeClubShmup1.Engine;
using Microsoft.Xna.Framework;
using CodeClubShmup1.Objects;
using Microsoft.Xna.Framework.Input;

namespace CodeClubShmup1.Scenes
{
    class GameScene : SceneParent
    {
        ScrollingBackground background1;
        ScrollingBackground background2;

        List<Bullet> bullets = new List<Bullet>();
        List<Enemy> enemies = new List<Enemy>();

        Random random = new Random();
        Timer enemy_spawn_timer;

        Player player;

        HUDScene hud;

        int points = 0;

        public override void  Start()
        {
            enemy_spawn_timer = new Timer(3000);

            player = new Player(Resources.GetTexture("Ship"), new Vector2(100, 100), 5);

            background1 = new ScrollingBackground(new Vector2(-100,-100), 40, new Sprite(Resources.GetTexture("StarWars")));
            background2 = new ScrollingBackground(new Vector2(0,0), 30, new Sprite(Resources.GetTexture("StarWars")));

            camera.setZoom(1.3f);

            Vector2 offset = 
                new Vector2(Game1.screen_size.Width, Game1.screen_size.Height) * 0.5f;

            camera.PositionOffset = offset;
            camera.setOffset(offset);

            hud = new HUDScene();

            BackgroundColor = Color.Aqua;

            SceneSys.OpenScene(hud);

        }

        public override void Update(float dt)
        {
            base.Update(dt);

            if (Input.IsKeyPressed(Keys.M))
            {
                Paused = true;
                SceneSys.OpenScene(new MenuScene());
            }

            background1.Update(dt);
            background2.Update(dt);

            if (Input.IsKeyPressed(Keys.Space) && !player.IsDead)
                bullets.Add(new Bullet(Resources.GetTexture("Bullet"), player.Position, 700));

            if (enemy_spawn_timer.Update(dt))
            {
                enemies.Add(new Enemy(Resources.GetTexture("Enemy"),
                    new Vector2(Game1.screen_size.Width,
                        random.Next(Game1.screen_size.Height)),
                        100));

                if (enemy_spawn_timer.Delay >= 200)
                    enemy_spawn_timer.Delay -= 100;
            }

            // Game Objects Updates
            if (!player.IsDead)
                player.Update(dt);
            else
                hud.SetGameOver();

            foreach (Bullet item in bullets)
            {
                item.Update(dt);

                foreach (Enemy e in enemies)
                {
                    if (item.CollisionRect.Intersects(e.CollisionRect))
                    {
                        e.IsDead = true;
                        item.IsDead = true;

                        points += 1000;
                        hud.SetScore(points);
                    }
                }

                if (item.Position.X > Game1.screen_size.Width)
                {
                    item.IsDead = true;
                }
            }

            foreach (Enemy item in enemies)
            {
                item.Update(dt);

                if (item.Position.X < 0)
                {
                    item.IsDead = true;
                }

                if (!player.IsDead)
                {
                    if (item.CollisionRect.Intersects(player.CollisionRect))
                    {
                        item.IsDead = true;
                        player.HP -= 10;

                        hud.SetPlayerHP(player.HP);
                    }
                }
            }

            //deleting objects
            for (int i = 0; i < enemies.Count; i++)
            {
                Enemy e = enemies[i];

                if (e.IsDead)
                {
                    enemies.Remove(e);
                    i--;
                }
            }

            for (int i = 0; i < bullets.Count; i++)
            {
                Bullet e = bullets[i];

                if (e.IsDead)
                {
                    bullets.Remove(e);
                    i--;
                }
            }

            camera.Position = player.Position;

            if (Input.IsKeyDown(Keys.L))
                camera.addZoom(dt);
            if (Input.IsKeyDown(Keys.K))
                camera.addZoom(-dt);

            camera.addRotation(500000);

            Vector2 offset = camera.PositionOffset / camera.getZoom();

            if (camera.Position.X < offset.X)
                camera.Position.X = offset.X;
            if (camera.Position.X > Game1.screen_size.Width - offset.X)
                camera.Position.X = Game1.screen_size.Width - offset.X;
               
            if (camera.Position.Y < offset.Y)
                camera.Position.Y = offset.Y;
            if (camera.Position.Y > Game1.screen_size.Height - offset.Y)
                camera.Position.Y = Game1.screen_size.Height - offset.Y;



        }

        public override void Draw()
        {
           
            background1.Draw();
            background2.Draw();

            DrawSys.DrawText("wow", Resources.GetFont("CSfont"), new Vector2(25, 25), Color.LimeGreen);
            DrawSys.DrawText("much graphics", Resources.GetFont("CSfont"), new Vector2(100, 100), Color.BlueViolet);
            DrawSys.DrawText("such goty", Resources.GetFont("CSfont"), new Vector2(300, 300), Color.Gray);
            DrawSys.DrawText("wow", Resources.GetFont("CSfont"), new Vector2(400, 411), Color.RosyBrown);

            if (!player.IsDead)
                player.Draw();

            foreach (Bullet item in bullets)
            {
                item.Draw();
            }
            foreach (Enemy item in enemies)
            {
                item.Draw();
            }
        }
    }
}
