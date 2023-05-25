using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace easyNetAPI.Models
{
    public class UserBehavior
    {
        [BsonId]
        //[BsonIgnore]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string _id { get; set; }
        [BsonElement("user_id")]
        public string? UserId { get; set; }
        [BsonElement("administrator")]
        public bool? Administrator { get; set; }
        [BsonElement("description")]
        public string? Description { get; set; }
        [BsonElement("company")]
        public Company? Company { get; set; }
        [BsonElement("posts")]
        public List<Post>? Posts { get; set; }
        [BsonElement("followed_users")]
        public List<string>? FollowedUsers { get; set; }
        [BsonElement("followers_list")]
        public List<string>? FollowedList { get; set; }

        [BsonElement("liked_posts")]
        public List<int>? LikedPost { get; set; }
        [BsonElement("saved_posts")]
        public List<int>? SavedPost { get; set; }
        [BsonElement("mentioned_posts")]
        public List<int>? MentionedPost { get; set; }
    }
}
