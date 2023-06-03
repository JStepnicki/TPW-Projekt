
namespace Data
{
    public abstract class BallLoggerApi
    {
        public abstract void addBallToQueue(BallApi ball);

        public static BallLoggerApi CreateBallLoger()
        {
            return new BallLogger();
        }
    }
}