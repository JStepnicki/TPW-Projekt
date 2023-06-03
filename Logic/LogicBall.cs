using Data;
using System;
using System.Numerics;

namespace Logic
{
    internal class LogicBall:LogicBallApi
    {
        private Vector2 _position { get; set; }

        public override event EventHandler<LogicEventArgs>? ChangedPosition;


        internal LogicBall(float xPosition, float yPosition)
        {
            _position = new Vector2(xPosition, yPosition);
        }



        public void UpdateBall(Object s, DataEventArgs e)
        {
            BallApi ball = (BallApi)s;
            Position = ball.Position;
            LogicEventArgs args = new LogicEventArgs(this);
            ChangedPosition?.Invoke(this, args);
        }

        public override Vector2 Position
        {
            get => _position;
            set
            {
                if (_position != value)
                {
                    _position = value;
                }
            }
        }

    }


}


