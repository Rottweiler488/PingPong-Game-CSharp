using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    class Score
    {
        public int scorePlayer = 0, scoreEnemy = 0;
        private string scoreText;
        private Vector2 position;
        private SpriteFont spriteFont;

        public Score(SpriteFont spriteFont, Vector2 position)
        {
            this.spriteFont = spriteFont;
            this.position = position;
        }

        public void Update(GameTime deltaTime)
        {
            scoreText = $"{scorePlayer}   {scoreEnemy}";
        }

        public void DrawString(SpriteBatch sprite)
        {
            sprite.DrawString(spriteFont, scoreText, position, Color.Black);
        }
    }
}
