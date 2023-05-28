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
        private object lockSpeed = new object();
        private object lockPosition = new object();
        private Vector2 _position { get; set; }
        private Vector2 _speed { get; set; }
        public override Boolean isRunning { get; set; }

        public override event EventHandler<DataEventArgs>? ChangedPosition;
        public override float Mass { get; set; }
        public override int Radius { get; set; }
        public override bool CollisionCheck { get; set; }

        private static object lockObject = new object();



        internal Ball(float X, float Y, int radius, float mass, float xSpeed, float ySpeed)
        {
            _position = new Vector2(X, Y);
            _speed = new Vector2(xSpeed, ySpeed);
            Mass = mass;
            Radius = radius;
            Task.Run(StartMovement);
            CollisionCheck = false;
            isRunning = true;

    }

        private async void StartMovement()
        {
            while (this.isRunning)
            {
                Move();
                CollisionCheck = false;
                await Task.Delay(10);
            }
        }


        private  void Move()
        {
            Monitor.Enter(lockObject);
            try
            {
                _position += _speed;
                DataEventArgs args = new DataEventArgs(this);
                ChangedPosition?.Invoke(this, args);
            }
            catch (SynchronizationLockException exception)
            {
                throw new Exception("Synchronization lock not working", exception);
            }
            finally
            {
                Monitor.Exit(lockObject);
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

        public override object getCommonLock()
        {
            return Ball.lockObject;
        }
    }

}

