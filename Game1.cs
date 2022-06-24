using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Pong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D textureBall;
        private Texture2D textureBar;
        private Texture2D textureLine;

        private SpriteFont spriteFont;

        private EnemyBar enemyBar;
        private Bar playerBar;
        private Ball ball;
        private Score score;

        public static int Width, Height;
        private bool finish;
        private bool playerWin;

        public Game1()
        {
            Exiting += Exit;

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Width = _graphics.PreferredBackBufferWidth;
            Height = _graphics.PreferredBackBufferHeight;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            textureBall = Content.Load<Texture2D>("PongBall");
            textureBar = Content.Load<Texture2D>("PongBar");
            textureLine = Content.Load<Texture2D>("Line");

            spriteFont = Content.Load<SpriteFont>("Score");

            enemyBar = new EnemyBar(textureBar, new Vector2(1, Height / 2), 145f);
            playerBar = new Bar(textureBar, new Vector2(1, Height / 2), 150f);
            ball = new Ball(textureBall, new Vector2(Width / 2f, Height / 2f), 150f);
            score = new Score(spriteFont, new Vector2((Width / 2f) - 45f, 0));

            GameSaver.Load(out score.scorePlayer, out score.scoreEnemy);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            KeyboardState keyboard = Keyboard.GetState();

            if (!finish)
            {
                ball.Update(gameTime);
                playerBar.Update(gameTime);
                enemyBar.Update(gameTime);
                score.Update(gameTime);

                #region PlayerBar
                if (ball.position.X < playerBar.position.X + playerBar.Width && ball.position.Y > playerBar.position.Y && ball.position.Y + ball.Height < playerBar.position.Y + playerBar.Height)
                {
                    ball.position.X = playerBar.position.X + playerBar.Width;
                    ball.velocity.X *= -1;

                    score.scorePlayer++;
                }
                #endregion

                #region EnemyBar
                if (ball.position.X + ball.Width > enemyBar.position.X && ball.position.Y > enemyBar.position.Y && ball.position.Y + ball.Height < enemyBar.position.Y + enemyBar.Height)
                {
                    ball.position.X = enemyBar.position.X - ball.Width;
                    ball.velocity.X *= -1;

                    score.scoreEnemy++;
                }

                if (ball.position.X > Width / 2)
                {
                    if (ball.position.Y > enemyBar.position.Y)
                    {
                        enemyBar.position += enemyBar.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else
                    {
                        enemyBar.position -= enemyBar.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                }
                #endregion
            }

            if (score.scorePlayer > 20)
            {
                playerWin = true;
                finish = true;
            }
            else if (score.scoreEnemy > 20)
            {
                finish = true;
            }

            if (finish)
            {
                if (keyboard.IsKeyDown(Keys.R))
                    Restart();              
            }
            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(textureLine, new Vector2((Width / 2) - 5, 0), Color.White);

            ball.Draw(_spriteBatch);
            playerBar.Draw(_spriteBatch);
            enemyBar.Draw(_spriteBatch);

            score.DrawString(_spriteBatch);
            if (finish)
            {
                var text = playerWin ? "You win!" : "You lose!";
                var size = spriteFont.MeasureString(text);
                _spriteBatch.DrawString(spriteFont, text, new Vector2(Width / 2 - size.X / 2, Height / 2 - size.Y / 2), Color.Red);

                text = "press: \"r\" to restart";
                size = spriteFont.MeasureString(text);
                _spriteBatch.DrawString(spriteFont, text, new Vector2(Width / 2 - size.X / 2, Height / 2 + size.Y), Color.Red);
            }            
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void Restart()
        {
            finish = false;
            ball.position = new Vector2(Width / 2, Height / 2);
            enemyBar.position = new Vector2(1, Height / 2);
            playerBar.position = new Vector2(1, Height / 2);
            score.scorePlayer = 0;
            score.scoreEnemy = 0;
        } 

        private void Exit(object sender, EventArgs eventArgs)
        {
            GameSaver.Save(score.scorePlayer, score.scoreEnemy);
        }
    }
}