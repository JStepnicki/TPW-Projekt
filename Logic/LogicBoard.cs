﻿using Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Logic
{
    internal sealed class LogicApi : LogicAbstractAPI
    {
        public int sizeX { get; set; }
        public int sizeY { get; set; }

        private int _BallRadius { get; set; }
        public List<LogicBallApi> Balls { get; set; }

        private Object _locker = new Object();

        public DataApi dataAPI;



        public LogicApi(int sizeX, int sizeY)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            Balls = new List<LogicBallApi>();
            dataAPI = DataApi.CreateApi(sizeY, sizeX);
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
        /*public void ApplyTempSpeed(IDataBall ball)
        {
            ball.YSpeed = ball.TempYSpeed;
            ball.XSpeed = ball.TempXSpeed;
        }*/

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



        /*
        public void CollideWithBall(IDataBall me, IDataBall collider)
        {
            //Te dwie zmienne sa tymczasowe, poki nei dodamy masy do Balla
            double ourMass = 1;
            double otherMass = 1;

            double ourSpeed = Math.Sqrt(me.XSpeed * me.XSpeed + me.YSpeed * me.YSpeed);
            double otherSpeed = Math.Sqrt(collider.XSpeed * collider.XSpeed + collider.YSpeed * collider.YSpeed);

            // Mozliwe ze zle uzywam arcus tangens, juz nie pamietam za bardzo trygonometrii XD
            //TUTAJ MOZLIWE ZE JEST BLAD W TYM WZORZE
            double contactAngle = Math.Atan(Math.Abs((me.PosY - collider.PosY )/ (me.PosX - collider.PosX)));

            // same as before
            double ourMovementAngle = Math.Atan(me.YSpeed / me.XSpeed);
            double otherMovementAngle = Math.Atan(collider.YSpeed / collider.XSpeed);


            // numerator_SPEEDX = (ourSpeed*cos(ourMovementAngle-contactAngle)(ourMass - otheramss) + 2*otherMass*otherSpeed*cos(otherMovementAngle-contactAngle))*cos(contactAngle)
            double SpeedXNumerator = (ourSpeed * Math.Cos(ourMovementAngle - contactAngle) * (ourMass - otherMass) + 2 * otherMass * otherSpeed * Math.Cos(otherMovementAngle - contactAngle) * Math.Cos(contactAngle));
            double SpeedXDenominator = ourMass + otherMass;
            double addToSpeedX = ourSpeed * Math.Sin(ourMovementAngle - contactAngle) * Math.Cos(contactAngle + Math.PI / 2f);


            double SpeedYNumerator = (ourSpeed * Math.Cos(ourMovementAngle - contactAngle) * (ourMass - otherMass) + 2 * otherMass * otherSpeed * Math.Cos(otherMovementAngle - contactAngle) * Math.Sin(contactAngle));
            double SpeedYDenominator = SpeedXDenominator;
            double addToSpeedY = ourSpeed * Math.Sin(ourMovementAngle - contactAngle) * Math.Sin(contactAngle + Math.PI / 2f);

            me.TempXSpeed = (SpeedXNumerator / SpeedXDenominator + addToSpeedX);
            me.TempYSpeed = (SpeedYNumerator / SpeedYDenominator + addToSpeedY);
            // numerator_SPEEDY = numerator_SPEEDX/cos(contactAngle)*sin(contactAngle) // to put it simply, the numerator is the same, except it's multiplied by sin instead of cos
            // denominator_SPEEDY = denomiator_SPEEDX;
            // addToSpeedY = ourSpeed*sin(ourMovementAngle - contactAngle)*sin(contactAngle + PI/2)
            //contactAngle is the angle of a line connecting the centers of the balls
        }
        */


        // tutaj jest problem, bo nie usuwamy instancji kulek (tzn. taski dalej sie wykonuja w tle, ale nie ma ich narysowanych na planszy)
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
