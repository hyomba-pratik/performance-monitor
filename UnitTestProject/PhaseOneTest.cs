using System;
using System.Diagnostics;
using Microsoft.VisualBasic.Devices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Week5Challenge;

namespace UnitTestProject
{
    [TestClass]
    public class PhaseOneTest
    {
        [TestMethod]
        public void FetchUsagesTest()
        {

            //arrange
            PerformanceCounter CPUCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            PerformanceCounter RAMCounter = new PerformanceCounter("Memory", "Available MBytes");
            decimal TotalRAM = Convert.ToDecimal((new ComputerInfo().TotalPhysicalMemory / (Math.Pow(1024, 2))) + 0.5);

            //act
            var usages = PhaseOne.FetchUsages(CPUCounter, RAMCounter, TotalRAM);


            //assert
            Assert.IsNotNull(usages);
        }
    }
}
