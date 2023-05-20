using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Models
{
    public class QA
    {
        public string? Intent { get; set; }
        public string[] Questions { get; set; }
        public string? Answer { get; set; }
    }
}
