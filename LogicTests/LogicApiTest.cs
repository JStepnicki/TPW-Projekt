using Data;
using Logic;
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
            public override int Mass { get; set; }
            public override int Radius { get; set; }
            public override bool CollisionCheck { get; set; }
            public FakeDataBall(float X, float Y, int radius, int mass, int xSpeed, int ySpeed)
            {
                _position = new Vector2(X, Y);
                _speed = new Vector2(xSpeed, ySpeed);
                Mass = mass;
                Radius = radius;
                Task.Run(StartMovement);
                CollisionCheck = false;
                isRunning = true;
            }

            public async void StartMovement()
            {
                while (this.isRunning)
                {
                    lock (this)
                    {
                        Vector2 movedPos = new Vector2(Position.X + Speed.X, Position.Y + Speed.Y);
                        Position = movedPos;
                        DataEventArgs args = new DataEventArgs(this);
                        ChangedPosition?.Invoke(this, args);
                    }
                    CollisionCheck = false;
                    await Task.Delay(10);
                }
            }


            public override Vector2 Position
            {
                get => _position;
                set
                {
                    if (_position != value)
                    {
                        _position = value;
                    }
                }
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

            public FakeDataAPI(int width, int height)
            {
                Width = width;
                Height = height;
            }

            public override BallApi AddBall(float X, float Y, int radius, int Mass, int xSpeed = 0, int ySpeed = 0)
            {
                BallApi ball = BallApi.CreateBall(X, Y, radius, Mass, xSpeed, ySpeed);
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
            LogicBoardApi board = LogicBoardApi.CreateAPI(new FakeDataAPI(500, 500));
            Assert.IsNotNull(board);
        }

        [TestMethod]
        public void AddingBallsTest()
        {
            LogicBoardApi board = LogicBoardApi.CreateAPI(new FakeDataAPI(500, 500));
            board.AddBalls(3, 5);
            Assert.AreEqual(board.GetAllBalls().Count, 3);
        }
        [TestMethod]
        public void ClearingBoardTest()
        {
            LogicBoardApi board = LogicBoardApi.CreateAPI(new FakeDataAPI(500, 500));
            board.AddBalls(3, 5);
            Assert.AreEqual(board.GetAllBalls().Count, 3);

            board.ClearBoard();
            Assert.AreEqual(board.GetAllBalls().Count, 0);
        }
        [TestMethod]
        public void ClearingEmptyBoardTest()
        {
            LogicBoardApi board = LogicBoardApi.CreateAPI(new FakeDataAPI(500, 500));
            board.ClearBoard();
            Assert.AreEqual(board.GetAllBalls().Count, 0);
        }
    }





}


