using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pong
{
    public class ScoreKeeper : Sprite
    {
        public ScoreKeeper(Vector2 position, SpriteFont font, Color color)
            : base(position, font, color)
        {

        }

        public void UpdateScore(int player1Score, int player2Score)
        {
            Text = $"{player1Score.ToString()}  :  {player2Score.ToString()}";            
        }
    }
}
