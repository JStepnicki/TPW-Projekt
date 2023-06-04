
namespace Data
{
    public abstract class DataLoggerApi
    {
        public abstract void addBallToQueue(BallApi ball);

        public abstract void addBoardData(BoardApi board);

        public static DataLoggerApi CreateBallLoger()
        {
            return new DataLogger();
        }
    }
}