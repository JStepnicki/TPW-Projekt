using System.Collections.Generic;
using System;

namespace Logic
{
    public class Board
    {
        private int height;
        private int width;
        private bool Enabled = false;
        private List<Ball> balls = new List<Ball>();


        public Board(int height, int width)
        {
            this.height = height;
            this.width = width;
        }

        public void FillBallList(int ballsQuantity, int ballRadius)
        {
            balls.Clear();
            Random random = new Random();
            for (int i = 0; i < ballsQuantity; i++)
            {
                int x = random.Next(ballRadius, this.width - ballRadius);
                int y = random.Next(ballRadius, this.height - ballRadius);
                balls.Add(new Ball(x, y, ballRadius));
            }
        }


        public int Height 
        {
            get { return height; } 
        }
        public int Width
        { get 
            { return width; } 
        }
        public List<Ball> Balls 
        { 
            get { return balls; } 
        }
        public bool IsRunning 
        { 
            get { return Enabled; } 
            set { Enabled = value; } 
        }
    }
}