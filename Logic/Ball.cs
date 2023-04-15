using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    internal class Ball
    {
        private int Xpos { get; set; }
        private int Ypos { get; set; }
        private int Radius { get; set; }
        private int Mass { get; set; }
        private int SpeedX { get; set; }
        private int SpeedY { get; set; }



        public Ball(int posX, int posY, int radius,int mass)   // nie wiem czy potrzebujemy tutaj od razu podawac predkosc
        {                                                   // czy jednak powinniśmy dopiero potem to robic
            this.Xpos = posX;
            this.Ypos = posY;
            this.Radius = radius;
            this.Mass = mass;
            Random rnd = new Random();
            this.SpeedY = rnd.Next(0, 10);
            this.SpeedX = rnd.Next(0, 10);
        }

        public void moveBall()
        {
            this.Xpos += SpeedX;
            this.Ypos += SpeedY;
        }

        public bool wallColission(int BoardWidth, int BoardHeight)
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

    }
}
