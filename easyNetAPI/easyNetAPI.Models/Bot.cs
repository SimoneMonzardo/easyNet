using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Models
{
    public class Bot
    {
        public int BotId { get; set; }
        public string? Type { get; set; }
        public string? Platform { get; set; }
        [JsonProperty("Q&A")]
        public QA[]? QA { get; set; }
        public Panel[]? Panels { get; set; }
    }
}
