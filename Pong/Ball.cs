using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
//using System.Numerics;
using System.Text;

namespace Pong
{
    class Ball : Sprite
    {
        public Vector2 Speed;

        public Ball(Microsoft.Xna.Framework.Vector2 position, Texture2D texture, Microsoft.Xna.Framework.Color tint, Vector2 speed)
            : base(position, texture, tint)
        {
            Speed = speed;
        }
        public void JumpToMiddle(Rectangle boundary)
        {
            Position = new Vector2(boundary.Center.X - Texture.Width / 4, boundary.Center.Y - Texture.Height / 4);
        }
        public bool HitLeftOfScreen(Rectangle boundary) => Position.X + HitBox.Width <= boundary.Y;
        public bool HitRightOfScreen(Rectangle boundary) => Position.X >= boundary.Width;
        public void Move(Rectangle boundary)
        {
            Position += Speed;
            if (Position.Y <= boundary.Y)
            {
                Speed.Y = Math.Abs(Speed.Y);
            }
            else if(Position.Y + Texture.Height >= boundary.Height)
            {
                Speed.Y = -Math.Abs(Speed.Y);
            }

            if (HitLeftOfScreen(boundary))
            {
                Speed.X = Math.Abs(Speed.X);
            }
            else if(HitRightOfScreen(boundary))
            {
                Speed.X = -Math.Abs(Speed.X);
            }
        }
    }
}
