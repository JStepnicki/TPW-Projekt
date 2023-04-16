using Logic;

namespace LogicTests
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            int height = 100;
            int width = 200;
            Board board = new Board(height, width);
            Assert.AreEqual(height, board.Height);
            Assert.AreEqual(width, board.Width);
        }

        [TestMethod]
        public void AddBallsToBoard()
        {
            Board board = new Board(200, 200);

            int expectedBallsCount = 5;
            int ballRadius = 3;


            board.FillBallList(expectedBallsCount, ballRadius);
            Assert.AreEqual(expectedBallsCount,board.Balls.Count);
            foreach (var ball in board.Balls)
            {
                Assert.AreEqual(ballRadius, ball.Radius);
            }
        }
    }
}
