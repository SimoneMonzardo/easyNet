using MongoDB.Bson.Serialization.Attributes;
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
        [BsonElement("panel_id")]
        public int? PanelId { get; set; }
        [BsonElement("panelName")]
        public string? PanelName { get; set; }
        [BsonElement("content")]
        public string? Content { get; set; }
        [BsonElement("buttons")]
        public Button[]? Buttons { get; set; }
    }
}
