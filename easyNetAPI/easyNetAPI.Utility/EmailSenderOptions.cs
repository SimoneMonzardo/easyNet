using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Utility
{
    public class EmailSenderOptions
    {
        public string EmailFrom { get; set; } = string.Empty;
        public string EmailSenderName { get; set; } = string.Empty;
        public string SmtpHost { get; set; } = string.Empty;
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; } = string.Empty;
        public string SmtpPass { get; set; } = string.Empty;
        public string GenerateMessageIdFrom { get; set; } = string.Empty;

    }
}
