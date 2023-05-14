using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Data
{
    public abstract class BallApi
    {
        public abstract double xCordinate { get; set; }
        public abstract double yCordinate { get; set; }
        public abstract int Mass { get; set; }
        public abstract double XSpeed { get; set; }
        public abstract double YSpeed { get; set; }
        public abstract int Radius { get; set; }
        public abstract bool CollisionCheck { get; set; }
        public abstract void Move();

        public abstract event EventHandler<DataEventArgs> ChangedPosition;

        public static BallApi CreateBall(int X, int Y, int radius, int Mass, int xSpeed, int ySpeed)
        {
            return new Ball(X, Y, radius, Mass, xSpeed, ySpeed);
        }
    }
}
