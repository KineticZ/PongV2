using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace Pong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ScoreKeeper ScoreKeeper;
        private Stopwatch stopwatch;
        private Random random;
        private Rectangle ScreenSize => GraphicsDevice.Viewport.Bounds;
        private Paddle player1 { get; set; }
        private Paddle player2 { get; set; }
        private Ball ball { get; set; }
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            random = new Random();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            stopwatch = new Stopwatch(); 
            stopwatch.Start();
                        
            var PaddleTexture = Content.Load<Texture2D>("Paddle");
            var BallTexture = Content.Load<Texture2D>("Ball");
            var defaultFont = Content.Load<SpriteFont>("Fonts/Default");
            // TODO: use this.Content to load your game content here
            int fontWidth = defaultFont.Texture.Width;
            ScoreKeeper = new ScoreKeeper(new Vector2((ScreenSize.Width / 2) - (fontWidth / 4), 10), defaultFont, Color.Black);
            int offsetX = 5;
            player1 = new Paddle
            (
                new Vector2(GraphicsDevice.Viewport.X + offsetX, GraphicsDevice.Viewport.Y),
                PaddleTexture,
                Color.White, 
                2
            );
            player2 = new Paddle
            (
                new Vector2(GraphicsDevice.Viewport.Width - PaddleTexture.Width - offsetX, GraphicsDevice.Viewport.Height - PaddleTexture.Height),
                PaddleTexture,
                Color.White,
                2
            );

            ball = new Ball(
                new Vector2(ScreenSize.Width / 2, ScreenSize.Height / 2),
                BallTexture,
                Color.White,
                new Vector2(2, 2));

        }
        private bool IsUnderTop(Ball ball, Paddle paddle) => ball.HitBox.Bottom >= paddle.HitBox.Top;
        private bool IsAboveBottom(Ball ball, Paddle paddle) => ball.HitBox.Top <= paddle.HitBox.Bottom;
        private void BallAndPaddleReaction(Ball ball, Paddle leftPaddle, Paddle rightPaddle)
        {
            if (IsUnderTop(ball, leftPaddle) && IsAboveBottom(ball, leftPaddle) && ball.HitBox.Left <= leftPaddle.HitBox.Right)
            {
                ball.Speed.X = Math.Abs(ball.Speed.X);
            }
            else if (ball.HitBox.Right >= rightPaddle.HitBox.Left && IsUnderTop(ball, rightPaddle) && IsAboveBottom(ball, rightPaddle))
            {
                ball.Speed.X = -Math.Abs(ball.Speed.X);
            }
        }
        private void CheckScore()
        {
            if (ball.HitLeftOfScreen(ScreenSize))
            {
                ball.JumpToMiddle(ScreenSize);
                player2.Score++;
            }
            else if (ball.HitRightOfScreen(ScreenSize))
            {
                ball.JumpToMiddle(ScreenSize);
                player1.Score++;
            }
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            // TODO: Add your update logic here
            if(ScreenSize.Contains(player2.HitBox) && stopwatch.ElapsedMilliseconds < 1000)
            {
                player2.Speed = (int)ball.Speed.Y;
            }
            else if (stopwatch.ElapsedMilliseconds > 5000)
            {
                stopwatch.Reset();
            }

            player1.MoveUpDown(Keyboard.GetState().IsKeyDown(Keys.W), Keyboard.GetState().IsKeyDown(Keys.S),
                GraphicsDevice.Viewport.Y, GraphicsDevice.Viewport.Height);
            player2.AutoMoveUpDown(GraphicsDevice.Viewport.Y, GraphicsDevice.Viewport.Height);
            ball.Move(ScreenSize);
            BallAndPaddleReaction(ball, player1, player2);

            CheckScore();
            ScoreKeeper.UpdateScore(player1.Score, player2.Score);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            ScoreKeeper.DrawString(_spriteBatch);
            player1.Draw(_spriteBatch);
            player2.Draw(_spriteBatch);
            ball.Draw(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
