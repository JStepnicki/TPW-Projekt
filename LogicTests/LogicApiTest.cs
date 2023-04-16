using Logic;

namespace LogicTests
{
    [TestClass]
    public class LogicAPITests
    {
        [TestMethod]
        public void TestInitiateBoard()
        {
            int height = 100;
            int width = 200;
            int ballQuantity = 10;
            int ballRadius = 5;

            AbstractLogicAPI api = AbstractLogicAPI.CreateApi();
            api.InitiateBoard(height, width, ballQuantity, ballRadius);

            List<Ball> balls = api.GetBallsList();
            foreach (Ball ball in balls)
            {
                Assert.IsTrue(ball.XCord >= ball.Radius && ball.XCord <= width - ball.Radius);
                Assert.IsTrue(ball.YCord >= ball.Radius && ball.YCord <= height - ball.Radius);
            }
            Assert.AreEqual(ballQuantity, balls.Count);
            Assert.AreEqual(ballRadius, balls[0].Radius);
        }

        [TestMethod]
        public void TestCreateBalls()
        {
            AbstractLogicAPI api = AbstractLogicAPI.CreateApi();
            api.InitiateBoard(100, 200, 10, 5);
            api.CreateBalls();
            api.Enable();

            var ballsList = api.GetBallsList();
            Assert.AreEqual(10, ballsList.Count);

            api.Disable();
            ballsList = api.GetBallsList();

            Assert.AreEqual(0, ballsList.Count);
            foreach (var ball in ballsList)
            {
                Assert.IsTrue(ball.XCord != 0 || ball.YCord != 0);
            }
        }

        [TestMethod]
        public void TestEnableDisable()
        {
            AbstractLogicAPI api = AbstractLogicAPI.CreateApi();
            api.InitiateBoard(100, 200, 10, 5);

            api.Enable();
            Assert.IsTrue(api.IsEnabled());

            api.Disable();
            Assert.IsFalse(api.IsEnabled());
        }

        [TestMethod]
        public void TestBallMovement()
        {
            Ball ball = new Ball(50, 50, 10);

            int initialXCord = ball.XCord;
            int initialYCord = ball.YCord;

            ball.XMovement = 10;
            ball.YMovement = -5;

            ball.MakeMove();

            Assert.AreEqual(initialXCord + ball.XMovement, ball.XCord);
            Assert.AreEqual(initialYCord + ball.YMovement, ball.YCord);
        }
    }
}
