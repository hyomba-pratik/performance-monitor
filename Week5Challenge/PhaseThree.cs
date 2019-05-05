using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Week5Challenge.Enum;
using Week5Challenge.Model;

namespace Week5Challenge
{
    public static class PhaseThree
    {
        public static bool ResourceLeaked(List<UsagesModel> ResourceUsages, ResourceTypeEnum type)
        {
            try
            {
                var last5minutesReport = ResourceUsages.Where(x => x.DateTime > DateTime.Now.AddMinutes(-5)).ToList();
                SendEmailNotification(last5minutesReport, type);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static async void SendEmailNotification(List<UsagesModel> last5minutesReport, ResourceTypeEnum type)
        {
            string message = "";
            foreach (var usages in last5minutesReport)
            {
                if (type == ResourceTypeEnum.CPU)
                {
                    message += $"{usages.DateTime}   CPU Usage   {usages.CPUUsages}%${Environment.NewLine}";
                }
                else
                {
                    message += $"{usages.DateTime}   Memory Usages   {usages.MemoryUsages}MB${Environment.NewLine}";
                }
            }

            MailMessage mail = new MailMessage("servermonitor@insightworkshop.com", "admin@insightworkshop.com");
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "smtp.gmail.com";
            if (type == ResourceTypeEnum.CPU)
            {
                mail.Subject = "CPU Usages Exceeded 90%";
            }
            else
            {
                mail.Subject = "Memory Usages Exceeded 85%";
            }
            mail.Body = message;
            client.SendAsync(mail, null);
        }
    }
}
