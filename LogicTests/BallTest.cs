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
            Assert.AreEqual(ball.Xpos, 5);
            Assert.AreEqual(ball.Ypos, 2);
            Assert.AreEqual(ball.Radius, 3);
            Assert.AreEqual(ball.Mass, 10);
        }

        [TestMethod]
        public void TestMoveBall()
        {
            Ball ball = new Ball(1, 1, 3);
            ball.SpeedX = 3;
            ball.MoveBall();
            Assert.AreEqual(ball.Xpos, 4);
            ball.SpeedY = 1;
            ball.MoveBall();
            Assert.AreEqual(ball.Ypos, 2);
            Assert.AreEqual(ball.Xpos, 7);
        }

        [TestMethod]
        public void TestColission()
        {
            Ball ball1 = new Ball(645, 70, 3);
            Ball ball2 = new Ball(80, 415, 3);
            ball1.SpeedX = 1;
            ball2.SpeedX = 2;
            ball1.SpeedY = 2;
            ball2.SpeedY = 5;
            Assert.IsTrue(ball1.CheckColission(650, 420));
            Assert.IsFalse(ball2.CheckColission(650, 420));
        }
    }
}