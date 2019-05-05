using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week5Challenge.Model;

namespace Week5Challenge
{
    public static class PhaseOne
    {
        public static UsagesModel FetchUsages(PerformanceCounter CPUCounter, PerformanceCounter RAMCounter, decimal TotalRAM)
        {
            UsagesModel resourceUsages;
            try
            {
                var cpuUsages = Convert.ToDecimal(Math.Round(CPUCounter.NextValue(), 4));
                var memoryUsages = Convert.ToDecimal(RAMCounter.NextValue());
                var memoryInPercent = Math.Round((memoryUsages / TotalRAM) * 100, 4);

                resourceUsages = new UsagesModel
                {
                    CPUUsages = cpuUsages,
                    MemoryUsages = memoryUsages,
                    DateTime = DateTime.Now,
                    MemoryUsagesinPercent = memoryInPercent
                };
            }
            catch (Exception)
            {
                return null;
            }

            return resourceUsages;
        }
    }
}
