using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public abstract class DataApi
    {
        public abstract int Width { get; set; }
        public abstract int Height { get; set; }

        public abstract List<BallApi> GetAllBalls();
        public abstract void RemoveAllBalls();
        public abstract BallApi AddBall(int xPosition, int yPosition, int radius, int weight, int xSpeed = 0, int ySpeed = 0);

        public static DataApi CreateApi(int boardWidth, int boardHeight)
        {
            return new Board(boardWidth, boardHeight);
        }

    }
}
