using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Pong
{
    public class Paddle : Sprite
    {
        public int Score { get; set; }
        public int Speed { get; set; }
        public Paddle(Microsoft.Xna.Framework.Vector2 position, Texture2D texture, Microsoft.Xna.Framework.Color tint, int speed) 
            : base(position, texture, tint)
        {
            Speed = speed;
            Score = 0;
        }

        public void MoveUpDown(bool isUpActive, bool isDownActive, int topLimit, int bottomLimit)
        {
            if (isUpActive && Position.Y > topLimit)
            {
                Position.Y -= Speed;
            }
            else if(isDownActive && Position.Y + Texture.Height < bottomLimit)
            {
                Position.Y += Speed;
            }
        }
        public void AutoMoveUpDown(int topLimit, int bottomLimit)
        {
            Position.Y += Speed;
            if (Position.Y <= topLimit)
            {
                Speed = Math.Abs(Speed);
            }
            else if (Position.Y + Texture.Height >= bottomLimit)
            {
                Speed = -Math.Abs(Speed);
            }
        }

    }
}
