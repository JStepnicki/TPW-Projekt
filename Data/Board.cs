using System.Collections.Generic;
using System;

namespace Data
{
    public abstract class BoardApi
    {
        public abstract int height { get; }
        public abstract int width { get; }
        public abstract void FillBallList(int ballsQuantity, int ballRadius, int ballMass);
        public abstract List<BallApi> GetBalls();

        public BoardApi CreateBoard(int height, int width)
        {
            return new Board(height, width);
        }

        internal class Board : BoardApi
        {
            private readonly int _width;
            private readonly int _height;
            private readonly List<BallApi> _balls;

            public Board(int height, int width)
            {
                _height = height;
                _width = width;
            }

            public override void FillBallList(int ballsQuantity, int ballRadius, int ballMass)
            {
                Random random = new Random();
                for (int i = 0; i < ballsQuantity; i++)
                {
                    int x = random.Next(ballRadius, this.width - ballRadius);
                    int y = random.Next(ballRadius, this.height - ballRadius);
                    _balls.Add(new BallApi.Ball(x, y, ballRadius, ballMass));
                }
            }


            public override int height
            {
                get { return _height; }
            }
            public override int width
            {
                get
                { return _width; }
            }
            public override List<BallApi> GetBalls()
            {
                return _balls;
            }
        }
    }
}