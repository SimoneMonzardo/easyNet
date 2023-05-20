using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Models
{
    public class Panel
    {
        [JsonProperty("panel_id")]
        public int PanelId { get; set; }
        [JsonProperty("panelName")]
        public string? PanelName { get; set; }
        public string? Content { get; set; }
        public Button[]? Buttons { get; set; }
    }
}
