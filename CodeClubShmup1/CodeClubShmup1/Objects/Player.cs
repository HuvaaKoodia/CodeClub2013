﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using CodeClubShmup1.Engine;
using Microsoft.Xna.Framework.Input;

namespace CodeClubShmup1.Objects
{
    public class Player:ObjectParent
    {

        int hp = 140;
        public int HP
        {
            get { return hp; }
            set
            {
                hp = value;
                if (hp <= 0)
                {
                    IsDead = true;
                }
            }
        }

        public Player(Texture2D texture, Vector2 position, float speed)
            : base(texture, position, speed)
        {
            _sprite = new Sprite(texture, 32, 32, 3, 350);
        }

        public void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (Input.IsKeyDown(Keys.D1))
                _speed = 1;
            if (Input.IsKeyDown(Keys.D2))
                _speed = 2.5f;
            if (Input.IsKeyDown(Keys.D3))
                _speed = 5;

            if (Input.IsKeyDown(Keys.D))
                _position += Vector2.UnitX * _speed;

            if (Input.IsKeyDown(Keys.A))
                _position -= Vector2.UnitX * _speed;

            if (Input.IsKeyDown(Keys.S))
                _position += Vector2.UnitY * _speed;

            if (Input.IsKeyDown(Keys.W))
                _position -= Vector2.UnitY * _speed;

            if (_position.X < 0)
            {
                _position.X = 0;
            }
            if (_position.Y < 0)
            {
                _position.Y = 0;
            }
            if (_position.X > Game1.screen_size.Width - _sprite.texture.Bounds.Width)
            {
                _position.X = Game1.screen_size.Width - _sprite.texture.Bounds.Width;
            }
            if (_position.Y > Game1.screen_size.Height - _sprite.texture.Bounds.Height)
            {
                _position.Y = Game1.screen_size.Height - _sprite.texture.Bounds.Height;
            }
        }

        public void Draw()
        {
            base.Draw();
        }
    }
}
