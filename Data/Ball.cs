using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    internal class Ball : BallApi
    {
        private Vector2 _position { get; set; }
        private Vector2 _speed { get; set; }
        public override Boolean isRunning { get; set; }

        public override event EventHandler<DataEventArgs>? ChangedPosition;
        public override float Mass { get; set; }
        public override int Radius { get; set; }
        public override bool CollisionCheck { get; set; }
        Stopwatch stopwatch;
        private DataLoggerApi _logger;

        public override int ID { get; }




        internal Ball(int id,float X, float Y, int radius, float mass, float xSpeed, float ySpeed, DataLoggerApi logger)
        {
            ID = id;
            _position = new Vector2(X, Y);
            _speed = new Vector2(xSpeed, ySpeed);
            Mass = mass;
            Radius = radius;
            Task.Run(StartMovement);
            CollisionCheck = false;
            isRunning = true;
            stopwatch = new Stopwatch();
            this._logger = logger;
        }

        private async void StartMovement()
        {
            while (this.isRunning)
            {
                float time = stopwatch.ElapsedMilliseconds/10;
                stopwatch.Restart();
                stopwatch.Start();
                Move(time);
                _logger.addBallToQueue(this);
                Vector2 tempSpeed = Speed;
                int sleepTime = (int)(1 / Math.Abs(tempSpeed.X) + Math.Abs(tempSpeed.Y));
                if(sleepTime < 10)
                {
                    sleepTime = 10;
                }
                await Task.Delay(sleepTime);
                stopwatch.Stop();
            }
        }


        private  void Move(float time)
        {
            Vector2 tempPos = _position;
            Vector2 tempSpeed = Speed;
            tempPos = new Vector2(tempPos.X + tempSpeed.X * time, tempPos.Y + tempSpeed.Y * time);
            _position = tempPos;
            DataEventArgs args = new DataEventArgs(this);
            ChangedPosition?.Invoke(this, args);
        }



        public override Vector2 Position
        {
            get => _position;
        }



        public override Vector2 Speed
        {
            get => _speed;

            set
            {
                if (_speed != value)
                {
                    _speed = value;
                }
            }
        }


    }

}

