using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class Ball : BallApi
    {
        private object lockSpeed = new object();
        private object lockPosition = new object();
        private Vector2 _position { get; set; }
        private Vector2 _speed { get; set; }
        public override Boolean isRunning { get; set; }

        public override event EventHandler<DataEventArgs>? ChangedPosition;
        public override int Mass { get; set; }
        public override int Radius { get; set; }
        public override bool CollisionCheck { get; set; }
        public Ball(float X, float Y, int radius, int mass, int xSpeed, int ySpeed)
        {
            _position = new Vector2(X, Y);
            _speed = new Vector2(xSpeed, ySpeed);
            Mass = mass;
            Radius = radius;
            Task.Run(StartMovement);
            CollisionCheck = false;
            isRunning = true;
        }

        public async void StartMovement()
        {
            while (this.isRunning)
            {
                lock (this)
                {
                    Move();
                }
                CollisionCheck = false;
                await Task.Delay(10);
            }
        }

        public override void Move()
        {
            Vector2 movedPos = new Vector2(Position.X + Speed.X, Position.Y + Speed.Y);
            Position = movedPos;
            DataEventArgs args = new DataEventArgs(this);
            ChangedPosition?.Invoke(this, args);
        }


        public override Vector2 Speed
        {
            get
            {
                lock (lockSpeed)
                {
                    return _speed;
                }
            }

            set
            {
                lock (lockSpeed)
                {
                    if (_speed != value)
                    {
                        _speed = value;
                    }
                }
            }
        }

        public override Vector2 Position
        {
            get
            {
                lock (lockPosition)
                {
                    return _position;
                }
            }

            set
            {
                lock (lockPosition)
                {
                    if (_position != value)
                    {
                        _position = value;
                    }
                }
            }
        }
    }

}

