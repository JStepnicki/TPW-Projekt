
using Catel.Runtime.Serialization;
using System;

namespace Data
{
    public abstract class DataLoggerApi : IDisposable
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