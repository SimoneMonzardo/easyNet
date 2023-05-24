using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace easyNetAPI.Models
{
    public class UserBehavior
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        [BsonElement("user_id")]
        public string? UserId { get; set; }
        [BsonElement("administrator")]
        public bool Administrator { get; set; }
        [BsonElement("company")]
        public Company? Company { get; set; }
        [BsonElement("posts")]
        public Post[]? Posts { get; set; }
        [BsonElement("followed_users")]
        public string[]? FollowedUsers { get; set; }
        [BsonElement("followers_list")]
        public string[]? FollowedList { get; set; }
        [BsonElement("liked_posts")]
        public string[]? LikedPost { get; set; }
        [BsonElement("saved_posts")]
        public string[]? SavedPost { get; set; }
        [BsonElement("mentioned_posts")]
        public string[]? MentionedPost { get; set; }
    }
}
