using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Pong
{
    class Bar
    {
        private Texture2D texture;
        private Vector2 velocity;
        public Vector2 position;

        public float Width => texture.Width;
        public float Height => texture.Height;

        public Bar (Texture2D texture, Vector2 position, float speed)
        {
            this.texture = texture;
            this.position = position;
            velocity = new Vector2(0, speed);
        }

        public void Update(GameTime deltaTime)
        {
            var keyBoard = Keyboard.GetState();
            if (position.Y < 0)
            {
                position.Y = 0;
            }
            else if (position.Y + texture.Height > Game1.Height)
            {
                position.Y = Game1.Height - texture.Height;
            }

            if (keyBoard.IsKeyDown(Keys.S))
            {
                position += velocity * (float) deltaTime.ElapsedGameTime.TotalSeconds;
            }
            else if (keyBoard.IsKeyDown(Keys.W))
            {
                position -= velocity * (float)deltaTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(texture, position, Color.LightGray);
        }
    }
}
