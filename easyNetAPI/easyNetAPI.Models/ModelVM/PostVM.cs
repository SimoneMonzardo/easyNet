using System;
using MongoDB.Bson.Serialization.Attributes;

namespace easyNetAPI.Models.ModelVM
{
	public class PostVM
	{
        public int PostId { get; set; }
        public List<Comment>? Comments { get; set; }
        public string? Username { get; set; }
        public string? Content { get; set; }
        public DateTime? DataDiCreazione { get; set; }
        public List<string>? Likes { get; set; }
        public List<string>? Hashtags { get; set; }
        public List<string>? Tags { get; set; }
        public string? ImgUrl { get; set; }
    }
}