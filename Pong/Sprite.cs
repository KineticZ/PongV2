using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pong
{
    public abstract class Sprite
    {
        public Vector2 Position;
        public string Text { get; set; }
        public Texture2D Texture { get; set; }
        public SpriteFont Font { get; set; }
        public Color Tint { get; set; }
        public Rectangle HitBox 
            => new Rectangle((int)Position.X, (int)Position.Y, (int)(Texture.Width), (int)(Texture.Height));
        public Sprite(Vector2 position, Texture2D texture, Color tint)
        {
            Position = position;
            Texture = texture;
            Tint = tint;
        }

        public Sprite(Vector2 position, SpriteFont font, Color color)
        {
            Position = position;
            Font = font;
            Tint = color;
            Text = "";
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Tint);
        }

        public void DrawString(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, Text, Position, Tint); 
        }
    }
}
