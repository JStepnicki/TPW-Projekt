using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;

namespace Data
{
    internal class Board : BoardApi
    {
        public override int Width { get; set; }
        public override int Height { get; set; }

        private List<BallApi> Balls = new List<BallApi>();

        public Board(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override BallApi AddBall(int X, int Y, int radius, int Mass, int xSpeed = 0, int ySpeed = 0)
        {
            BallApi ball = BallApi.CreateBall(X, Y, radius, Mass, xSpeed, ySpeed);
            Balls.Add(ball);
            return ball;
        }

        public override List<BallApi> GetAllBalls()
        {
            return Balls;
        }
        public override void RemoveAllBalls()
        {
            Balls.Clear();
        }

    }
}
