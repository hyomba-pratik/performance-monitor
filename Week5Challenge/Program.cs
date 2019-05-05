using Microsoft.AspNet.SignalR.Client;
using Microsoft.Owin.Hosting;
using Microsoft.VisualBasic.Devices;
using Newtonsoft.Json;
using Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Week5Challenge.Enum;
using Week5Challenge.Model;

namespace Week5Challenge
{
    public static class Program
    {
        static PerformanceCounter CPUCounter;
        static PerformanceCounter RAMCounter;
        static readonly decimal TotalRAM;
        public static List<UsagesModel> ResourceUsagesList;


        static Program()
        {
            CPUCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            RAMCounter = new PerformanceCounter("Memory", "Available MBytes");
            TotalRAM = Convert.ToDecimal((new ComputerInfo().TotalPhysicalMemory / (Math.Pow(1024, 2))) + 0.5);
            ResourceUsagesList = new List<UsagesModel>();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Fetching system CPU and Memory usages:");
            SetInterval(() =>
            {
                UsagesModel resourceUse = PhaseOne.FetchUsages(CPUCounter, RAMCounter, TotalRAM);

                ResourceUsagesList.Add(resourceUse);

                string outputText = $"{resourceUse.DateTime}   CPU Usage   {resourceUse.CPUUsages}%    Memory Usages   {resourceUse.MemoryUsages}MB";

                PhaseTwo.SaveToFile(outputText);

                PhaseTwo.PublishUsages(resourceUse);

                if (resourceUse.CPUUsages > 90)
                {
                    PhaseThree.ResourceLeaked(ResourceUsagesList, ResourceTypeEnum.CPU);
                }

                if (resourceUse.MemoryUsagesinPercent > 85)
                {
                    PhaseThree.ResourceLeaked(ResourceUsagesList, ResourceTypeEnum.Memory);
                }


                Console.WriteLine(outputText);

            }, TimeSpan.FromSeconds(10));

            Console.ReadLine();
        }

        static async void SetInterval(Action action, TimeSpan timeout)
        {
            action();
            await Task.Delay(timeout).ConfigureAwait(false);
            SetInterval(action, timeout);
        }

    }
}
