using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Models
{
    public class UserBehavior
    {
        [JsonProperty("buttonName")]
        public string? UserId { get; set; }
        public bool Administrator { get; set; }
        public string Description { get; set; }
        public Company? Company { get; set; }
        public List<Post>? Posts { get; set; }
        [JsonProperty("followed_users")]
        public string[]? FollowedUsers { get; set; }
        [JsonProperty("followers_list")]
        public string[]? FollowedList { get; set; }
        [JsonProperty("liked_posts")]
        public string[]? LikedPost { get; set; }
        [JsonProperty("saved_posts")]
        public string[]? SavedPost { get; set; }
        [JsonProperty("mentioned_posts")]
        public string[]? MentionedPost { get; set; }
    }
}
