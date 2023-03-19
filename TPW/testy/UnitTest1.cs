using TPW;
using static TPW.calculator;
namespace testy
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            calculator test = new calculator();
            Assert.AreEqual(test.add(4,5),9);
        }
    }
}