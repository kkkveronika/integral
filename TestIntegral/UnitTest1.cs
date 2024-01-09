//Тест
namespace TestIntegral
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void integ()
        {
            var actual = find_integral.trapezoid_int(1, 1, 1, 0, 10, 1000);
            Assert.AreEqual(393.3334999999895, actual);

            actual = find_integral.parabola_int(1, 1, 1, 0, 10, 1000);
            Assert.AreEqual(393.33333333332723, actual);

            actual = find_integral.trapezoid_int(1, 2, 0, -5, 0, 1000);
            Assert.AreEqual(66.61668749999838, actual);

            actual = find_integral.parabola_int(1, 2, 0, -5, 0, 1000);
            Assert.AreEqual(66.63333333333242, actual);

            actual = find_integral.trapezoid_int(2, 3, 1, -10, -5, 1000);
            Assert.AreEqual(126.18337499999696, actual);

            actual = find_integral.parabola_int(2, 3, 1, -10, -5, 1000);
            Assert.AreEqual(126.06666666666493, actual);

            actual = find_integral.trapezoid_int(1, 1, 1, 0, 2, 100);
            Assert.AreEqual(6.666800000000002, actual);

            actual = find_integral.parabola_int(1, 1, 1, 0, 2, 100);
            Assert.AreEqual(6.666666666666669, actual);

            actual = find_integral.trapezoid_int(-2, 1, 1, 0, 5, 1000);
            Assert.AreEqual(65.83337499999773, actual);

            actual = find_integral.parabola_int(-2, 1, 1, 0, 5, 1000);
            Assert.AreEqual(65.83333333333198, actual);
        }
    }
}