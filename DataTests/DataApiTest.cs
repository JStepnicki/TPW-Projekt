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
            public void BoardTest()
            {
                BoardApi dataAPI = BoardApi.CreateApi(600, 400);
                Assert.IsFalse(dataAPI.Equals(null));
                Assert.IsTrue(dataAPI.Width==600);
                Assert.IsTrue(dataAPI.Height == 400);
                dataAPI.AddBall(1, 10, 10, 12, 7, 1, 1);
                Assert.IsTrue(dataAPI.GetAllBalls().First().ID == 1);
                Assert.IsTrue(dataAPI.GetAllBalls().First().Position.X == 10);
                Assert.IsTrue(dataAPI.GetAllBalls().First().Position.X == 10);
                Assert.IsTrue(dataAPI.GetAllBalls().First().Radius == 12);
                Assert.IsTrue(dataAPI.GetAllBalls().First().Mass==7);
                Assert.IsTrue(dataAPI.GetAllBalls().First().Speed.Y == 1);
                Assert.IsTrue(dataAPI.GetAllBalls().First().Speed.X == 1);
                for (int i = 0; i < 10; i++)
                {
                    dataAPI.AddBall(i, 10, 12, 10, 1, 1);
                }
                Assert.IsTrue(11 == dataAPI.GetAllBalls().Count);
            }

        }
    }
}