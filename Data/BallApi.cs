using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Text;

namespace Data
{
    public abstract class BallApi
    {
        public abstract Vector2 Position { get; set; }
        public abstract Vector2 Speed { get; set; }
        public abstract int Mass { get; set; }
        public abstract int Radius { get; set; }
        public abstract bool CollisionCheck { get; set; }
        public abstract bool isRunning { get; set; }
        public abstract void Move();

        public abstract event EventHandler<DataEventArgs> ChangedPosition;

        public static BallApi CreateBall(float X, float Y, int radius, int Mass, int xSpeed, int ySpeed)
        {
            return new Ball(X, Y, radius, Mass, xSpeed, ySpeed);
        }
    }
}
