using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    class Ball
    {
        private Texture2D texture;
        public Vector2 velocity;
        public Vector2 position;

        public float Width => texture.Width;
        public float Height => texture.Height;

        public Ball(Texture2D texture, Vector2 position, float speed)
        {
            this.texture = texture;
            this.position = position;
            this.velocity = new Vector2(speed, speed);
        }

        public void Update(GameTime deltaTime)
        {
            if (position.Y < 0)
            {
                position.Y = 0;
                velocity.Y *= -1;
            }
            else if (position.Y + texture.Height * 1.5f > Game1.Height)
            {
                position.Y = Game1.Height - texture.Height * 1.5f;
                velocity.Y *= -1;
            }

            if (position.X < 0)
            {
                position.X = 0;
                velocity.X *= -1;
            }
            else if (position.X + texture.Width * 1.5f > Game1.Width)
            {
                position.X = Game1.Width - texture.Width * 1.5f;
                velocity.X *= -1;
            }

            position += velocity * (float)deltaTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(texture, position, Color.Red);
        }
    }
}
