using Data;
using Logic;
using System.Diagnostics;
using System.Numerics;
using System.Reflection.Emit;
using System.Xml.Schema;

namespace LogicApiTest
{
    [TestClass]
    public class LogicApiTest
    {
        [TestClass]
        internal class FakeDataBall : BallApi
        {
            private Vector2 _position { get; set; }
            private Vector2 _speed { get; set; }
            public override Boolean isRunning { get; set; }

            public override event EventHandler<DataEventArgs>? ChangedPosition;
            public override float Mass { get; set; }
            public override int Radius { get; set; }
            public override bool CollisionCheck { get; set; }
            Stopwatch stopwatch;
            private DataLoggerApi _logger;

            public override int ID { get; }




            internal FakeDataBall(int id, float X, float Y, int radius, float mass, float xSpeed, float ySpeed, DataLoggerApi logger)
            {
                ID = id;
                _position = new Vector2(X, Y);
                _speed = new Vector2(xSpeed, ySpeed);
                Mass = mass;
                Radius = radius;
                Task.Run(StartMovement);
                CollisionCheck = false;
                isRunning = true;
                stopwatch = new Stopwatch();
                this._logger = logger;
            }

            private async void StartMovement()
            {
                while (this.isRunning)
                {
                    float time = stopwatch.ElapsedMilliseconds / 10;
                    stopwatch.Restart();
                    stopwatch.Start();
                    Move(time);
                    _logger.addBallToQueue(this);
                    Vector2 tempSpeed = Speed;
                    int sleepTime = (int)(1 / Math.Abs(tempSpeed.X) + Math.Abs(tempSpeed.Y));
                    if (sleepTime < 10)
                    {
                        sleepTime = 10;
                    }
                    await Task.Delay(sleepTime);
                    stopwatch.Stop();
                }
            }


            private void Move(float time)
            {
                Vector2 tempPos = _position;
                Vector2 tempSpeed = Speed;
                tempPos = new Vector2(tempPos.X + tempSpeed.X * time, tempPos.Y + tempSpeed.Y * time);
                _position = tempPos;
                DataEventArgs args = new DataEventArgs(this);
                ChangedPosition?.Invoke(this, args);
            }



            public override Vector2 Position
            {
                get => _position;
            }



            public override Vector2 Speed
            {
                get => _speed;

                set
                {
                    if (_speed != value)
                    {
                        _speed = value;
                    }
                }
            }


        }


        [TestClass]
        internal class FakeDataAPI : BoardApi
        {
            public override int Width { get; set; }
            public override int Height { get; set; }

            private List<BallApi> Balls = new List<BallApi>();
            private DataLoggerApi _logger = DataLoggerApi.CreateBallLoger();
            public FakeDataAPI(int width, int height)
            {
                Width = width;
                Height = height;
                _logger.addBoardData(this);
            }

            public override BallApi AddBall(int id, float X, float Y, int radius, float Mass, float xSpeed = 0, float ySpeed = 0)
            {
                BallApi ball = BallApi.CreateBall(id, X, Y, radius, Mass, xSpeed, ySpeed, _logger);
                Balls.Add(ball);
                return ball;
            }

            public override List<BallApi> GetAllBalls()
            {
                return Balls;
            }
            public override void RemoveAllBalls()
            {
                foreach (BallApi ball in Balls) { ball.isRunning = false; }
                Balls.Clear();
            }

        }



        [TestMethod]
        public void ConstructorTest()
        {
            LogicBoardApi board1 = LogicBoardApi.CreateAPI(new FakeDataAPI(500, 500));
            Assert.IsNotNull(board1);
            board1.AddBalls(3, 5);
            Assert.AreEqual(board1.GetAllBalls().Count, 3);
            board1.AddBalls(3, 5);
            Assert.AreEqual(board1.GetAllBalls().Count, 6);
            board1.ClearBoard();
            Assert.AreEqual(board1.GetAllBalls().Count, 0);
            board1.ClearBoard();
            Assert.AreEqual(board1.GetAllBalls().Count, 0);
        }
    }





}


