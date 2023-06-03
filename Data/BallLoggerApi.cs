
namespace Data
{
    public abstract class BallLoggerApi
    {
        public abstract void addBallToQueue(BallApi ball);

        public abstract void addBoardData(BoardApi board);

        public static BallLoggerApi CreateBallLoger()
        {
            return new BallLogger();
        }
    }
}