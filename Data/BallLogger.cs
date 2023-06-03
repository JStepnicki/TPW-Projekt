using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Reflection;

namespace Data
{
    internal class BallLogger : BallLoggerApi
    {
        private string _filePath;
        private Task _logerTask;
        private ConcurrentQueue<JObject> _ballsQueue;
        private JArray _logArray;
        private Mutex _writeMutex = new Mutex();
        private Mutex _enterQueueMutex = new Mutex();

        public BallLogger()
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
            _filePath = Path.Combine(path,"DataBallsLog.json");
            _ballsQueue = new ConcurrentQueue<JObject>();

            if (File.Exists(_filePath))
            {
                try
                {
                    string input = File.ReadAllText(_filePath);
                    _logArray = JArray.Parse(input);
                    return;
                }
                catch (JsonReaderException)
                {
                    _logArray = new JArray();
                }
            }
            _logArray = new JArray();
            FileStream file = File.Create(_filePath);
            file.Close();

        }

        public override void addBallToQueue(BallApi ball)
        {
            _enterQueueMutex.WaitOne();
            try
            {
                JObject logObject = JObject.FromObject(ball);
                logObject["Time:"] = DateTime.Now.ToString("HH:mm:ss");

                _ballsQueue.Enqueue(logObject);
                if (_logerTask == null || _logerTask.IsCompleted)
                {
                    _logerTask = Task.Run(writeDataToLogFile);
                }
            }
            finally
            {
                _enterQueueMutex.ReleaseMutex();
            }
        }

        public void writeDataToLogFile()
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
        }

        ~BallLogger()
        {
            _writeMutex.WaitOne();
            _writeMutex.ReleaseMutex();
        }
    }
}
