using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Pong
{
    class EnemyBar
    {
        private Texture2D texture;
        private Vector2 velocity;
        public Vector2 position;

        public Vector2 Velocity => velocity;

        public float Width => texture.Width;
        public float Height => texture.Height;

        public EnemyBar (Texture2D texture, Vector2 position, float speed)
        {
            this.texture = texture;
            this.position = position;
            velocity = new Vector2(0, speed);            
        }

        public void Update(GameTime deltaTime)
        {
            position.X = Game1.Width - texture.Width * 1.1f;

            if (position.Y < 0)
            {
                position.Y = 0;
            }
            else if (position.Y + texture.Height > Game1.Height)
            {
                position.Y = Game1.Height - texture.Height;
            }     
        }

        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(texture, position, Color.White);
        }
    }
}
