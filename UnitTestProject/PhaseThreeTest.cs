using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Week5Challenge;
using Week5Challenge.Enum;
using Week5Challenge.Model;

namespace UnitTestProject
{
    [TestClass]
    public class PhaseThreeTest
    {
        [TestMethod]
        public void ResourceLeakedTest()
        {
            //arrange
            var resourceUsages = new List<UsagesModel>();
            for (int i = 0; i < 5; i++)
            {
                resourceUsages.Add(new UsagesModel
                {
                    CPUUsages = 40 + i,
                    DateTime = DateTime.Now.AddMinutes(-3),
                    MemoryUsages = 4500 + i,
                    MemoryUsagesinPercent = 60 + i
                });
            }

            //act
            var resultCPU = PhaseThree.ResourceLeaked(resourceUsages, ResourceTypeEnum.CPU);
            var resultMemory = PhaseThree.ResourceLeaked(resourceUsages, ResourceTypeEnum.Memory);

            //assert
            Assert.IsTrue(resultCPU);
            Assert.IsTrue(resultMemory);
        }

    }
}
