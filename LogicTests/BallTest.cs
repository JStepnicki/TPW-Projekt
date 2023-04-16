using Logic;

namespace LogicTests
{
    [TestClass]
    public class BallTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            Ball ball = new Ball(5, 2, 3);
            Assert.AreEqual(ball.XCord, 5);
            Assert.AreEqual(ball.YCord, 2);
            Assert.AreEqual(ball.Radius, 3);
        }

        [TestMethod]
        public void TestMoveBall()
        {
            Ball ball = new Ball(1, 1, 3);
            ball.XMovement = 3;
            ball.MakeMove();
            Assert.AreEqual(ball.XCord, 4);
            ball.YMovement = 1;
            ball.MakeMove();
            Assert.AreEqual(ball.YCord, 2);
            Assert.AreEqual(ball.XCord, 7);
        }
    }
}