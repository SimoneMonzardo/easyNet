using easyNetAPI.Models;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace easyNetAPI.Models
{
    public class Post
    {
        [BsonElement("post_id")]
        public int PostId { get; set; }
        [BsonElement("comments")]
        public Comment[]? Comments { get; set; }
        [BsonElement("user_id")]
        public string? UserId { get; set; }
        [BsonElement("username")]
        public string? Username { get; set; }
        [BsonElement("content")]
        public string? Content { get; set; }

        //UserId
        [BsonElement("likes")]
        public string[]? Likes { get; set; }

        //Keyword hashtag
        [BsonElement("hashtags")]
        public string[]? Hashtags { get; set; }

        //UserId mention
        [BsonElement("tags")]
        public string[]? Tags { get; set; }
    }
}
