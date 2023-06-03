using System;
using System.Numerics;

namespace Logic
{
    public abstract class LogicBallApi
    {
        public static LogicBallApi CreateBall(int xPosition, int yPosition)
        {
            return new LogicBall(xPosition, yPosition);
        }

        public abstract Vector2 Position { get; set; }
        public abstract event EventHandler<LogicEventArgs>? ChangedPosition;
    }
}
