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
        [JsonProperty("comment_id")]
        public int CommentId { get; set; }
        [JsonProperty("user_id")]
        public string? UserId { get; set; }

        public string? Username { get; set; }
        public string? Content { get; set; }
        public string[]? Like { get; set; }
        public Reply[]? Replies { get; set; }
    }

}