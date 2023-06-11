using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Data
{
    internal class DataLogger : DataLoggerApi, IDisposable
    {
        private string _filePath;
        private Task _loggerTask;
        private ConcurrentQueue<JObject> _ballsQueue;
        private JArray _logArray;
        private Mutex _writeMutex = new Mutex();
        private bool _disposed = false;

        internal DataLogger()
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
            _filePath = Path.Combine(path, "DataBallsLog.json");
            _ballsQueue = new ConcurrentQueue<JObject>();

            if (File.Exists(_filePath))
            {
                try
                {
                    string input = File.ReadAllText(_filePath);
                    _logArray = JArray.Parse(input);
                }
                catch (JsonReaderException)
                {
                    _logArray = new JArray();
                }
            }
            else
            {
                _logArray = new JArray();
                FileStream file = File.Create(_filePath);
                file.Close();
            }

            _loggerTask = Task.Run(writeDataToLogFile);
        }

        public override void addBoardData(BoardApi board)
        {
            ClearLogFile();
            JObject logObject = JObject.FromObject(board);
            _logArray.Add(logObject);
            String data = JsonConvert.SerializeObject(_logArray, Newtonsoft.Json.Formatting.Indented);
            _writeMutex.WaitOne();
            try
            {
                File.WriteAllText(_filePath, data);
            }
            finally
            {
                _writeMutex.ReleaseMutex();
            }
        }
        public override void addBallToQueue(BallApi ball)
        {
            JObject logObject = JObject.FromObject(ball.Position);
            logObject["Time:"] = DateTime.Now.ToString("HH:mm:ss");
            logObject.Add("ID", ball.ID);

            _ballsQueue.Enqueue(logObject);
        }

        private async Task writeDataToLogFile()
        {
            while (!_disposed)
            {
                while (_ballsQueue.TryDequeue(out JObject ball))
                {
                    _logArray.Add(ball);
                }
                String data = JsonConvert.SerializeObject(_logArray, Newtonsoft.Json.Formatting.Indented);
                _writeMutex.WaitOne();
                try
                {
                    File.WriteAllText(_filePath, data);
                }
                finally
                {
                    _writeMutex.ReleaseMutex();
                }
                await Task.Delay(10);
            }
        }

        private void ClearLogFile()
        {
            _writeMutex.WaitOne();
            try
            {
                _logArray.Clear();
                File.WriteAllText(_filePath, string.Empty);
            }
            finally
            {
                _writeMutex.ReleaseMutex();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _writeMutex.Dispose();
                }
                _disposed = true;
            }
        }

        ~DataLogger()
        {
            Dispose(false);
        }
    }
}
