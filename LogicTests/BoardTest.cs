using Logic;

namespace LogicTests
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            Board board = new Board(100, 200);
            Assert.AreEqual(100, board.BoardWidth);
            Assert.AreEqual(200, board.BoardHeight);
        }

    }
}
