using System;
using MongoDB.Bson.Serialization.Attributes;

namespace easyNetAPI.Models.UpsertModels
{
	public class UpsertComment
	{
        public int PostId { get; set; }
        public int CommentId { get; set; }
        public string? Content { get; set; }
    }
}

