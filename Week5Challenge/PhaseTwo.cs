using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week5Challenge.Model;

namespace Week5Challenge
{
    public static class PhaseTwo
    {
        public static string SaveToFile(string input)
        {
            string path = @"C:\PerformanceCounter\" + DateTime.Now.Date.ToString("yyyy-MM-dd") + ".log";

            try
            {
                if (!File.Exists(path))
                {
                    File.Create(path).Dispose();
                }

                using (TextWriter tw = new StreamWriter(path, append: true))
                {
                    tw.WriteLine(input);
                }
            }
            catch (Exception)
            {
                return null;
            }

            return path;

        }

        public static async void PublishUsages(UsagesModel usages)
        {
            var _hub = new HubConnection("http://localhost:62908/");
            var _proxy = _hub.CreateHubProxy("UsagesHub");
            await _hub.Start();
            await _proxy.Invoke("Publish", JsonConvert.SerializeObject(usages));
        }
    }
}
