using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week5Challenge.Model
{
    public class UsagesModel
    {
        public DateTime DateTime { get; set; }
        public decimal CPUUsages { get; set; }
        public decimal MemoryUsages { get; set; }
        public decimal MemoryUsagesinPercent { get; set; }
    }
}
