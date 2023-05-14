using Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Logic
{
    internal sealed class LogicBoard : LogicBoardApi
    {
        public int sizeX { get; set; }
        public int sizeY { get; set; }

        private int _BallRadius { get; set; }
        public List<LogicBallApi> Balls { get; set; }

        private Object _locker = new Object();

        public BoardApi dataAPI;



        public LogicBoard(int sizeX, int sizeY)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            Balls = new List<LogicBallApi>();
            dataAPI = BoardApi.CreateApi(sizeY, sizeX);
        }

        public override void AddBalls(int number, int radius)
        {
            _BallRadius = radius;
            for (int i = 0; i < number; i++)
            {
                Random random = new Random();
                int x = random.Next(radius, sizeX - radius);
                int y = random.Next(radius, sizeY - radius);
                int weight = random.Next(3, 3);

                int SpeedX;
                do
                {
                    SpeedX = random.Next(-3, 3);
                } while (SpeedX == 0);

                int SpeedY;
                do
                {
                    SpeedY = random.Next(-3, 3);
                } while (SpeedY == 0);

                BallApi dataBall = dataAPI.AddBall(x, y, _BallRadius, weight, SpeedX, SpeedY);
                LogicBall ball = new LogicBall(dataBall.xCordinate, dataBall.yCordinate);

                //dodajemy do eventu funkcje, ktore beda sie wywolywaly po wykonaniu Move(), bo wtedy jest PropertyChanged wywolywane
                dataBall.ChangedPosition += ball.UpdateBall;    //ball to nasz ball w logice, nie w data
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
                if (ball.xCordinate + ball.XSpeed + ball.Radius > dataAPI.Width || ball.xCordinate + ball.XSpeed - ball.Radius < 0)
                {
                    ball.XSpeed *= -1;
                }
                if (ball.yCordinate + ball.YSpeed + ball.Radius > dataAPI.Height || ball.yCordinate + ball.YSpeed - ball.Radius < 0)
                {
                    ball.YSpeed *= -1;
                }
            }
        }

        private void CheckBallsCollision(Object s, DataEventArgs e)
        {
            BallApi me = (BallApi)s;
            if (!me.CollisionCheck)
            {
                lock (_locker)
                {
                    foreach (BallApi ball in dataAPI.GetAllBalls())
                    {
                        if (ball != me)
                        {
                            if (Math.Sqrt(Math.Pow(ball.xCordinate - me.xCordinate, 2) + Math.Pow(ball.yCordinate - me.yCordinate, 2)) <= 2 * _BallRadius)
                            {
                                ballCollision(me, ball);
                            }
                        }
                    }
                }
            }
        }

        private void ballCollision(BallApi ball, BallApi otherBall)
        {
            if (Math.Sqrt(Math.Pow(ball.xCordinate + ball.XSpeed - otherBall.xCordinate - otherBall.XSpeed, 2) + Math.Pow(ball.yCordinate + ball.YSpeed - otherBall.yCordinate - otherBall.YSpeed, 2)) <= otherBall.Radius + ball.Radius)
            {
                double weight = 1d;

                double newXMovement = (2d * weight * ball.XSpeed) / (2d * weight);
                ball.XSpeed = (2d * weight * otherBall.XSpeed) / (2d * weight);
                otherBall.XSpeed = newXMovement;

                double newYMovement = (2 * weight * ball.YSpeed) / (2d * weight);
                ball.YSpeed = (2d * weight * otherBall.YSpeed) / (2d * weight);
                otherBall.YSpeed = newYMovement;

                ball.CollisionCheck = true;
                otherBall.CollisionCheck = true;
            }
        }

        public override void ClearBoard()
        {
            Balls.Clear();
        }



        public override List<LogicBallApi> GetAllBalls()
        {
            return Balls;
        }
    }
}
