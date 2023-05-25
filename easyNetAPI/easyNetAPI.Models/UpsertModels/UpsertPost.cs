using System;
using MongoDB.Bson.Serialization.Attributes;

namespace easyNetAPI.Models.UpsertModels
{
	public class UpsertPost
	{
        public int PostId { get; set; }
        public string? Content { get; set; }
    }
}

