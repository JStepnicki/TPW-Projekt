using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Text;

namespace Data
{
    public abstract class BallApi
    {
        public abstract Vector2 Position {get;}
        public abstract Vector2 Speed { get; set; }
        public abstract float Mass { get; set; }
        public abstract int Radius { get; set; }
        public abstract bool CollisionCheck { get; set; }
        public abstract bool isRunning { get; set; }

        public abstract event EventHandler<DataEventArgs> ChangedPosition;

        public static BallApi CreateBall(float X, float Y, int radius, float Mass, float xSpeed, float ySpeed, BallLoggerApi logger)
        {
            return new Ball(X, Y, radius, Mass, xSpeed, ySpeed,logger);
        }
    }
}
