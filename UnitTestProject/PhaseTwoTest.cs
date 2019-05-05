using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Week5Challenge;
using Week5Challenge.Model;

namespace UnitTestProject
{
    [TestClass]
    public class PhaseTwoTest
    {
        [TestMethod]
        public void SaveToFileTest()
        {
            //arrange
            UsagesModel usages = new UsagesModel
            {
                CPUUsages = 40,
                DateTime = DateTime.Now,
                MemoryUsages = 4500,
                MemoryUsagesinPercent = 60
            };

            string input = $"{usages.DateTime}   CPU Usage   {usages.CPUUsages}%    Memory Usages   {usages.MemoryUsages}MB";

            string expectedOutput = @"C:\PerformanceCounter\" + DateTime.Now.Date.ToString("yyyy-MM-dd") + ".log";
           
            //act
            var result = PhaseTwo.SaveToFile(input);

            //assert
            Assert.IsTrue(result == expectedOutput);
        }
    }
}
