using System.Drawing;
using Data;
namespace DataTests
{
    [TestClass]
    public class DataApiTest
    {
        [TestClass]
        public class DataAPITest
        {

            [TestMethod]
            public void dataAPIBallPOsitionTest()
            {
                BoardApi dataAPI = BoardApi.CreateApi(600,400);
                dataAPI.AddBall(1,10,10,12,10);
                Assert.IsTrue(dataAPI.GetAllBalls().First().Position.X == 10);
                Assert.IsTrue(dataAPI.GetAllBalls().First().Position.Y == 10);
            }

            [TestMethod]
            public void DataAPIBallMovementTest()
            {
                BoardApi dataAPI = BoardApi.CreateApi(600, 400);
                dataAPI.AddBall(1,10, 10, 12, 10,1,1);
                Assert.IsTrue(dataAPI.GetAllBalls().First().Speed.Y == 1);
                Assert.IsTrue(dataAPI.GetAllBalls().First().Speed.X == 1);
            }

            [TestMethod]
            public void dataAPIBallsMovingTest()
            {
                BoardApi dataAPI = BoardApi.CreateApi(600, 400);
                dataAPI.AddBall(1, 10, 10, 12, 10,12,12);
                BallApi ball = dataAPI.GetAllBalls().First();
                Assert.IsTrue(ball.Speed.X == 12);
                Assert.IsTrue(ball.Speed.Y == 12);

                float prevX = ball.Position.X;
                float prevY = ball.Position.Y;

                Thread.Sleep(20);

                Assert.AreNotEqual(prevX, ball.Position.X);
                Assert.AreNotEqual(prevY, ball.Position.Y);

            }


            [TestMethod]
            public void dataAPICreateBallsTest()
            {
                BoardApi dataAPI = BoardApi.CreateApi(600, 400);
                for (int i = 0; i < 10; i++)
                {
                    dataAPI.AddBall(10, 10, 12, 10, 1, 1);
                }
                Assert.IsTrue(10 == dataAPI.GetAllBalls().Count);
            }

        }
    }
}