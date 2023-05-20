using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Models
{
    public class Button
    {
        [JsonProperty("panel_id")]
        public int PanelId { get; set; }
        [JsonProperty("buttonName")]
        public string? ButtonName { get; set; }
    }
}
