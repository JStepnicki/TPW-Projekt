using SampleProgram;

namespace SampleTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Class1 test1 = new Class1();
            Assert.AreEqual(10, test1.add(6, 4));
            Assert.AreEqual(1, test1.subtract(8, 7));
        }

        [TestMethod]
        public void TestMethod2()
        {
            Class1 test2 = new Class1();
            int a = test2.add(3, 2);
            int b = test2.subtract(8, 2);
            Assert.IsFalse(a == b);
            Assert.IsTrue(test2.add(a, 1) == b);
        }
    }
}