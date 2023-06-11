
using Catel.Runtime.Serialization;

namespace Data
{
    public abstract class DataLoggerApi
    {
        public abstract void addBallToQueue(BallApi ball);
        public abstract void saveBoardData(BoardApi board);
        public abstract void Dispose();
        public static DataLoggerApi CreateBallLoger()
        {
            return new DataLogger();
        }
    }
}