using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public abstract class BoardApi
    {
        public abstract int Width { get; set; }
        public abstract int Height { get; set; }

        public abstract List<BallApi> GetAllBalls();
        public abstract void RemoveAllBalls();
        public abstract BallApi AddBall(int id,float xPosition, float yPosition, int radius, float weight, float xSpeed = 0, float ySpeed = 0);

        public static BoardApi CreateApi(int boardWidth, int boardHeight)
        {
            return new Board(boardWidth, boardHeight);
        }

    }
}
