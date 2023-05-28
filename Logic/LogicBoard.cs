using Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Numerics;
using System.Xml.Linq;

namespace Logic
{
    internal sealed class LogicBoard : LogicBoardApi
    {
        internal int SizeX { get; set; }
        internal int SizeY { get; set; }

        private int _BallRadius { get; set; }
        public List<LogicBallApi> Balls { get; set; }

        public BoardApi dataAPI;



        public LogicBoard(BoardApi api)
        {
            this.SizeX = api.Height;
            this.SizeY = api.Width;
            Balls = new List<LogicBallApi>();
            dataAPI = api;
        }

        public override void AddBalls(int number, int radius)
        {
            _BallRadius = radius;
            for (int i = 0; i < number; i++)
            {
                float x = random.Next(radius, SizeY - radius);
                float y = random.Next(radius,  SizeX- radius);
                int weight = random.Next(3, 3);

                float SpeedX;
                do
                {
                    SpeedX = random.Next(-3, 3);
                } while (SpeedX == 0);

                float SpeedY;
                do
                {
                    SpeedY = random.Next(-3, 3);
                } while (SpeedY == 0);

                BallApi dataBall = dataAPI.AddBall(x, y, _BallRadius, weight, SpeedX, SpeedY);
                LogicBall ball = new LogicBall(dataBall.Position.X, dataBall.Position.Y);


                dataBall.ChangedPosition += ball.UpdateBall;    
                dataBall.ChangedPosition += CheckCollisionWithWall;
                dataBall.ChangedPosition += CheckBallsCollision;

                Balls.Add(ball);
            }
        }

        private void CheckCollisionWithWall(Object s, DataEventArgs e)
        {

            BallApi ball = (BallApi)s;
            if (!ball.CollisionCheck)
            {
                if (ball.Position.X + ball.Speed.X + ball.Radius > dataAPI.Width || ball.Position.X + ball.Speed.X - ball.Radius < 0)
                {
                    ball.Speed = new Vector2(-ball.Speed.X, ball.Speed.Y);
                }
                if (ball.Position.Y + ball.Speed.Y + ball.Radius > dataAPI.Height || ball.Position.Y + ball.Speed.Y - ball.Radius < 0)
                {
                    ball.Speed = new Vector2(ball.Speed.X, -ball.Speed.Y);
                }
            }
        }

        private void CheckBallsCollision(Object s, DataEventArgs e)
        {
            BallApi ball = (BallApi)s;
            List<BallApi> collidingBalls = new List<BallApi>();
            Monitor.Enter(ball.getCommonLock());
            try
            {
                foreach (BallApi otherBall in dataAPI.GetAllBalls().ToArray())
            {
                double distance = Math.Sqrt(Math.Pow(ball.Position.X + ball.Speed.X - (otherBall.Position.X + otherBall.Speed.X), 2)
                                + Math.Pow(ball.Position.Y + ball.Speed.Y - (otherBall.Position.Y + otherBall.Speed.Y), 2));
                if (otherBall != ball && distance <= ball.Radius * 2)
                {
                    collidingBalls.Add(otherBall);
                }
            }


                foreach (BallApi otherBall in collidingBalls)
                {
                    float otherBallXSpeed = otherBall.Speed.X * (otherBall.Mass - ball.Mass) / (otherBall.Mass + ball.Mass)
                                           + ball.Mass * ball.Speed.X * 2f / (otherBall.Mass + ball.Mass);
                    float otherBallYSpeed = otherBall.Speed.Y * (otherBall.Mass - ball.Mass) / (otherBall.Mass + ball.Mass)
                                           + ball.Mass * ball.Speed.Y * 2f / (otherBall.Mass + ball.Mass);

                    float ballXSpeed = ball.Speed.X * (ball.Mass - otherBall.Mass) / (ball.Mass + ball.Mass)
                                      + otherBall.Mass * otherBall.Speed.X * 2f / (ball.Mass + otherBall.Mass);
                    float ballYSpeed = ball.Speed.Y * (ball.Mass - otherBall.Mass) / (ball.Mass + ball.Mass)
                                      + otherBall.Mass * otherBall.Speed.Y * 2f / (ball.Mass + otherBall.Mass);

                    otherBall.Speed = new Vector2(otherBallXSpeed, otherBallYSpeed);
                    ball.Speed = new Vector2(ballXSpeed, ballYSpeed);

                }
            }
            catch (SynchronizationLockException exception)
            {
                throw new Exception("Synchronization lock not working", exception);
            }
            finally
            {
                Monitor.Exit(ball.getCommonLock());
            }

        }



        public override void ClearBoard()
        {
            Balls.Clear();
            dataAPI.RemoveAllBalls();
        }



        public override List<LogicBallApi> GetAllBalls()
        {
            return Balls;
        }
    }
}
