using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Models
{
    public class QA
    {
        [BsonElement("intent")]
        public string? Intent { get; set; }
        [BsonElement("questions")]
        public string[] Questions { get; set; }
        [BsonElement("answer")]
        public string? Answer { get; set; }
    }
}
