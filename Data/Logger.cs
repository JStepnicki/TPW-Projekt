using System;
using System.Collections.Concurrent;
using System.IO;
using System.Numerics;
using System.Reflection;
using System.Threading.Tasks;

namespace Data
{
    internal class Logger : IDisposable
    {
        private BlockingCollection<string> buffer = new BlockingCollection<string>();
        private Task fileWriter;
        private StreamWriter sw;
        static string projectDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        static string dataDirectory = Path.GetFullPath(Path.Combine(projectDirectory, "..", "..", "..", "..", "Data"));
        string logFilePath = Path.Combine(dataDirectory, "log.txt");

        public Logger()
        {
            startLogging();
        }

        public void startLogging()
        {
            if (fileWriter == null || fileWriter.IsCompleted)
            {
                fileWriter = Task.Run(() => WriteToFile());
            }
        }

        public void LogBallPosition(BallApi ball)
        {
            string time = DateTime.Now.ToString("h:mm:ss tt");
            string log = $"{time} Ball moved to position ({ball.Position.X}, {ball.Position.Y}) with speed ({ball.Speed.X}, {ball.Speed.Y})";
            buffer.Add(log);
        }

        private void WriteToFile()
        {
            sw = new StreamWriter(logFilePath, append: false);
            try
            {
                foreach (string log in buffer.GetConsumingEnumerable())
                {
                    sw.WriteLine(log);
                }
            }
            finally
            {
                Dispose();
            }
        }

        public void Dispose()
        {
            buffer.CompleteAdding();
            fileWriter.Wait();
            sw.Close();
            sw?.Dispose();
        }
    }
}
