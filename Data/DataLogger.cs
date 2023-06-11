using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using Catel.Runtime.Serialization;

namespace Data
{
    internal class DataLogger : DataLoggerApi,IDisposable
    {
        private string _filePath;
        private ConcurrentQueue<BallApi> _ballsQueue;
        private JArray _logArray = new JArray();
        private  int QueueSize = 50;
        private CancellationTokenSource StateChange = new CancellationTokenSource();
        private bool StopTask;

        internal DataLogger()
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
            _filePath = Path.Combine(path, "DataBallsLog.json");
            _ballsQueue = new ConcurrentQueue<BallApi>();

            using (FileStream LogFile = File.Create(_filePath))
            {
                LogFile.Close();
            }

            this.StopTask = false;
            Task.Run(writeDataToLogFile);
        }


        public override void addBallToQueue(BallApi ball)
        {
            if (_ballsQueue.Count < this.QueueSize)
            {
                _ballsQueue.Enqueue(ball);
                StateChange.Cancel();
            }
        }


        private async void writeDataToLogFile()
        {
            StringBuilder stringBuilder = new StringBuilder();
            while (!this.StopTask)
            {
                if (!_ballsQueue.IsEmpty)
                {
                    while (_ballsQueue.TryDequeue(out BallApi serilizedObject))
                    {
                        JObject jsonObject = JObject.FromObject(serilizedObject);
                        jsonObject["Time"] = DateTime.Now.ToString("HH:mm:ss");
                        _logArray.Add(jsonObject);
                    }

                    stringBuilder.Append(JsonConvert.SerializeObject(_logArray, Formatting.Indented));
                    _logArray.Clear();
                    await File.AppendAllTextAsync(_filePath, stringBuilder.ToString());
                    stringBuilder.Clear();
                }
                await Task.Delay(Timeout.Infinite, StateChange.Token).ContinueWith(_ => { });

                if (this.StateChange.IsCancellationRequested)
                {
                    this.StateChange = new CancellationTokenSource();
                }
            }
        }
        public override void saveBoardData(BoardApi board)
        {
            JObject logObject = JObject.FromObject(board);
            String data = JsonConvert.SerializeObject(logObject, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_filePath, data);
        }
        public override void Dispose()
        {
            this.StopTask = true;
        }

        ~DataLogger()
        {
            this.Dispose();
        }
    }
}
