using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Models
{
    public class Comment
    {
        [BsonElement("comment_id")]
        public int CommentId { get; set; }
        [BsonElement("user_id")]
        public string? UserId { get; set; }
        [BsonElement("username")]
        public string? Username { get; set; }
        [BsonElement("content")]
        public string? Content { get; set; }
        [BsonElement("like")]
        public string[]? Like { get; set; }
        [BsonElement("replies")]
        public Reply[]? Replies { get; set; }
    }
}