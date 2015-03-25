using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThermalActivity;


namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            uint t = 1;
            uint n = 5;
            uint[] sunData = {5, 3, 1, 2, 0, 4, 1, 1, 3, 2, 2, 3, 2, 4, 3, 0, 2, 3, 3, 2, 1, 0, 2, 4, 3};

            Task.Run(async () =>
            {
                Sun sun = new Sun(sunData);

                await sun.EvaluateSunSurface();
                string result = sun.GetTopSolarScores(t);

                Assert.AreEqual(result, "(3,3 score:26)");
            });


        }

        [TestMethod]
        public void TestMethod2()
        {
            uint t = 3;
            uint n = 4;
            uint[] sunData = { 2, 3, 2, 1, 4, 4, 2, 0, 3, 4, 1, 1, 2, 3, 4, 4 };

            Task.Run(async () =>
            {
                Sun sun = new Sun(sunData);

                await sun.EvaluateSunSurface();
                string result = sun.GetTopSolarScores(t);

                Assert.AreEqual(result, "(1,2 score:27)(1,1 score:25)(2,2 score:23)");
            });
        }
    }
}
