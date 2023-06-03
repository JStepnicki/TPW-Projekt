using System.Collections.Generic;


namespace Data
{
    public abstract class BoardApi
    {
        private Logger logger = new Logger();
        public abstract int Width { get; set; }
        public abstract int Height { get; set; }

        public abstract List<BallApi> GetAllBalls();
        public abstract void RemoveAllBalls();
        public abstract BallApi AddBall(float xPosition, float yPosition, int radius, float weight, float xSpeed = 0, float ySpeed = 0);

        public static BoardApi CreateApi(int boardWidth, int boardHeight)
        {
            return new Board(boardWidth, boardHeight);
        }

        public void LogBallData(BallApi ball)
        {
            logger.LogBallPosition(ball);
        }

    }
}
