using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public abstract class LogicBallApi
    {
        public static LogicBallApi CreateBall(int xPosition, int yPosition)
        {
            return new LogicBall(xPosition, yPosition);
        }

        public abstract double PosX { get; set; }
        public abstract double PosY { get; set; }
        public abstract event EventHandler<LogicEventArgs>? ChangedPosition;
    }
}
