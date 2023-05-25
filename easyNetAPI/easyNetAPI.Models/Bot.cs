using MongoDB.Bson.Serialization.Attributes;
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
        [BsonElement("bot_id")]
        public int BotId { get; set; }
        [BsonElement("type")]
        public string? Type { get; set; }
        [BsonElement("platform")]
        public string? Platform { get; set; }
        [BsonElement("Q&A")]
        public List<QA>? QA { get; set; }
        [BsonElement("panels")]
        public List<Panel>? Panels { get; set; }
    }
}
