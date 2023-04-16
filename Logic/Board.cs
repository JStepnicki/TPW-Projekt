using System.Collections.Generic;
using System;

namespace Logic
{
    public class Board
    {
        private int height;
        private int width;
        private bool isRunning = false;
        private List<Ball> balls = new List<Ball>();


        public Board(int height, int width)
        {
            this.height = height;
            this.width = width;
        }

        public void CreateBallsList(int ballsAmount, int ballsSize)
        {
            balls.Clear();
            Random random = new Random();
            for (int i = 0; i < ballsAmount; i++)
            {
                int x = random.Next(ballsSize, this.width - ballsSize);
                int y = random.Next(ballsSize, this.height - ballsSize);
                balls.Add(new Ball(x, y, ballsSize));
            }
        }


        public int Height { get { return height; } }
        public int Width { get { return width; } }
        public List<Ball> Balls { get { return balls; } }
        public bool IsRunning { get { return isRunning; } set { isRunning = value; } }
    }
}