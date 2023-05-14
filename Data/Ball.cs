using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class Ball : BallApi
    {
        private double _xCordinate;
        private double _yCordinate;

        public override event EventHandler<DataEventArgs>? ChangedPosition;

        public override double xCordinate
        {
            get => _xCordinate;
            set { _xCordinate = value; }
        }
        public override double yCordinate
        {
            get => _yCordinate;
            set { _yCordinate = value; }
        }
        public override int Mass { get; set; }
        public override double XSpeed { get; set; }
        public override double YSpeed { get; set; }
        public override int Radius { get; set; }
        public override bool CollisionCheck { get; set; }
        public Ball(int X, int Y, int radius, int mass, int xSpeed, int ySpeed)
        {
            xCordinate = X;
            yCordinate = Y;
            Mass = mass;
            XSpeed = xSpeed;
            YSpeed = ySpeed;
            Radius = radius;
            Task.Run(StartMovement);
            CollisionCheck = false;
        }

        public async void StartMovement()
        {
            while (true)
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
            xCordinate += XSpeed;
            yCordinate += YSpeed;
            DataEventArgs args = new DataEventArgs(this);
            ChangedPosition?.Invoke(this, args);
        }


    }
}
