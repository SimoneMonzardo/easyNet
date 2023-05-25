using MongoDB.Bson.Serialization.Attributes;
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
        [BsonElement("panel_id")]
        public int PanelId { get; set; }
        [BsonElement("buttonName")]
        public string? ButtonName { get; set; }
    }
}
