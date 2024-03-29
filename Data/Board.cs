﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;

namespace Data
{
    internal class Board : BoardApi
    {
        public override int Width { get; set; }
        public override int Height { get; set; }

        private List<BallApi> Balls = new List<BallApi>();
        private DataLoggerApi _logger = DataLoggerApi.CreateBallLoger();
        public Board(int width, int height)
        {
            Width = width;
            Height = height;
            _logger.saveBoardData(this);
        }

        public override BallApi AddBall(int id,float X, float Y, int radius, float Mass, float xSpeed = 0, float ySpeed = 0)
        {
            BallApi ball = BallApi.CreateBall(id,X, Y, radius, Mass, xSpeed, ySpeed,_logger);
            Balls.Add(ball);
            return ball;
        }

        public override List<BallApi> GetAllBalls()
        {
            return Balls;
        }
        public override void RemoveAllBalls()
        {
            foreach (BallApi ball in Balls) { ball.isRunning = false; }
            Balls.Clear();
        }

    }
}
