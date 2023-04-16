using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class Ball
    {
        public int Xpos { get; set; }
        public int Ypos { get; set; }
        public int Radius { get; set; }
        public int Mass { get; set; }
        public int SpeedX { get; set; }
        public int SpeedY { get; set; }



        public Ball(int X, int Y, int radius)
        {
            this.Xpos = X;
            this.Ypos = Y;
            this.Radius = radius;
            this.Mass = 10;
        }

        public bool CheckColission(int BoardWidth, int BoardHeight)
        {
            if (this.Xpos + this.SpeedX + this.Radius < BoardWidth && this.Xpos + this.SpeedX - this.Radius > 0
                && this.Ypos + this.SpeedY + this.Radius < BoardHeight && this.Ypos + this.SpeedY - this.Radius > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal void RandSpeed(int min, int max)
        {
            Random rnd = new Random();
            this.SpeedY = rnd.Next(min, max);
            this.SpeedX = rnd.Next(min, max);
        }
        public void MoveBall()
        {
            this.Xpos += SpeedX;
            this.Ypos += SpeedY;
        }
    }
}
